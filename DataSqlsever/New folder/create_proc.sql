create proc Create_customer
(
	@username varchar(100),
	@fullname varchar(100),
	@phone_number varchar(12),
	@email varchar(30),
	@user_pass varchar(30)
)
as
begin
	if @username is null or @user_pass is null
	begin
		raiserror ('Username và password không được để trống', 16, 1);
		return;
	end

	if (@email is not null) and (@email not like '%@%')
	begin
		raiserror ('Email không hợp lệ', 16, 1);
		return;
	end

	declare @next_cus_id int = -1;
	select @next_cus_id = max(id) from Customer;
	
	if @next_cus_id is null
		set @next_cus_id = 0;
	else
		set @next_cus_id = @next_cus_id + 1;

	insert into Customer(ID, username, fullname, phone_number, email, user_pass, basket_owned)
	values(@next_cus_id, @username, @fullname, @phone_number, @email, @user_pass, 1);

	declare @next_basket_id int = -1;
	select @next_basket_id = max(id) from Basket;
	
	if @next_basket_id is null
		set @next_basket_id = 0;
	else
		set @next_basket_id = @next_basket_id + 1;

	insert into Basket (ID, customer_ID, item_count, total)
	values(@next_basket_id, @next_cus_id, 0, 0);
end;


create proc Update_customer
(
	@customer_id int,
	@new_phone varchar(12),
	@new_email varchar(30),
	@new_password varchar(30)
)
as
begin
	if @customer_id not in (select ID from Customer)
	begin
		raiserror ('ID khách hàng không đúng', 16, 5);
		return;
	end

	if @new_email not like '%@%'
	begin
		raiserror ('Email mới không hợp lệ', 16, 6);
		return;
	end

	if @new_email is null
		select @new_email = email from Customer where ID = @customer_id;

	if @new_phone is null
		select @new_phone = phone_number from Customer where ID = @customer_id;

	if @new_password is null
		select @new_password = user_pass from Customer where ID = @customer_id;

	update Customer
	set email = @new_email, phone_number = @new_phone, user_pass = @new_password
	where ID = @customer_id;

end;


create proc Paid
(
	@basket_id int,
	@delivery int,
	@delivery_date DateTime
)
as
begin
	if @basket_id not in (select ID from Basket)
	begin
		raiserror ('Giỏ hàng không hợp lệ', 16, 6);
		return;
	end

	if @delivery not in (select ID from Delivery)
	begin
		raiserror ('Mã bên giao hàng không hợp lệ', 16, 7);
		return;
	end

	update Basket
	set order_date = GETDATE()
	where ID = @basket_id;

	declare @next_basket_id int = -1;
	select @next_basket_id = max(ID) from Basket;

	if @next_basket_id is null
		set @next_basket_id = 0;
	else
		set @next_basket_id = @next_basket_id + 1;

	declare @customer_id int = -1;
	select @customer_id = customer_ID from Basket where ID = @basket_id;

	insert into Basket (ID, customer_ID)
	values(@next_basket_id, @customer_id);

	update Customer
	set basket_owned = basket_owned + 1
	where ID = @customer_id;

	update Goods
	set item_status = 'sold'
	where contained_in = @basket_id;

	declare @total_cost real = -1;
	select @total_cost = sum(sell_price) from Goods where contained_in = @basket_id;

	declare @next_sell_bill_id int = -1;
	select @next_sell_bill_id = max(ID) from Sell_Bill;

	if @next_sell_bill_id is null
		set @next_sell_bill_id = 0;
	else
		set @next_sell_bill_id = @next_sell_bill_id + 1;

	insert into Sell_Bill
	values (@next_sell_bill_id, @total_cost, GETDATE(), @basket_id);

	declare @next_deli_bill_id int = -1;
	select @next_deli_bill_id = max(ID) from Delivery_Bill;

	if @next_deli_bill_id is null
		set @next_deli_bill_id = 0;
	else
		set @next_deli_bill_id = @next_deli_bill_id + 1;

	insert into Delivery_Bill
	values (@next_deli_bill_id, @delivery_date, @delivery, @next_sell_bill_id);
end;

create proc Create_item_pc
(
	@item_name varchar(100),
	@setting varchar(100),
	@manufacturer varchar(20)
)
as
begin
	declare @next_id int = -1;
	select @next_id = max(ID) from Items;

	if @next_id is null
		set @next_id = 0;
	else
		set @next_id = @next_id + 1;

	insert into Items (ID, item_name, is_pc, is_laptop, is_accessory, pc_setting, pc_manufacturer)
	values (@next_id, @item_name, 'T', 'F', 'F', @setting, @manufacturer);
end;

create proc Create_item_laptop
(
	@item_name varchar(100),
	@setting varchar(100),
	@manufacturer varchar(20)
)
as
begin
	declare @next_id int = -1;
	select @next_id = max(ID) from Items;

	if @next_id is null
		set @next_id = 0;
	else
		set @next_id = @next_id + 1;

	insert into Items (ID, item_name, is_laptop, is_pc, is_accessory, laptop_setting, laptop_manufacturer)
	values (@next_id, @item_name, 'T', 'F', 'F', @setting, @manufacturer);
end;

create proc Create_item_phone
(
	@item_name varchar(100),
	@core varchar(100),
	@feature varchar(20)
)
as
begin
	declare @next_id int = -1;
	select @next_id = max(ID) from Items;

	if @next_id is null
		set @next_id = 0;
	else
		set @next_id = @next_id + 1;

	insert into Items (ID, item_name, is_laptop, is_pc, is_accessory, phone_core, phone_feature)
	values (@next_id, @item_name, 'T', 'F', 'F', @core, @feature);
end;

create proc Create_item_accessory
(
	@item_name varchar(100)
)
as
begin
	declare @next_id int = -1;
	select @next_id = max(ID) from Items;

	if @next_id is null
		set @next_id = 0;
	else
		set @next_id = @next_id + 1;

	insert into Items (ID, item_name, is_accessory, is_pc, is_laptop)
	values (@next_id, @item_name, 'T', 'F', 'F');
end;

create proc Create_delivery
(
	@name varchar(100),
	@country_code varchar(100),
	@region varchar(100),
	@city varchar(100),
	@phone varchar(12)
)
as
begin
	if @name in (select delivery_name from Delivery)
	begin
		raiserror('Tên bên giao hàng đã tồn tại', 16, 10);
		return;
	end

	insert into Delivery
	values (@name, @country_code, @region, @city, @phone);
end;

create proc Create_comment
(
	@from int,
	@rating real,
	@content varchar(1024),
	@realted_to int
)
as
begin
	if @from not in (select ID from Customer)
	begin
		raiserror ('Mã khách hàng không phù hợp', 16, 8);
		return;
	end

	if @realted_to not in (select ID from Items)
	begin
		raiserror ('Mã món hàng không hợp lệ', 16, 9);
		return;
	end

	if @rating not between 0 and 5
	begin
		raiserror ('Số rating không hợp lệ, chỉ nhận số trong khoảng 0 - 5', 16, 10);
		return;
	end

	declare @next_id int = -1;
	select @next_id = max(ID) from Comment;

	if (@next_id is null)
		set @next_id = 0;
	else
		set @next_id = @next_id + 1;

	insert into Comment
	values (@next_id, @content, @rating, GETDATE(), @from, @realted_to);
end;


create proc Create_supplier
(
	@name varchar(100),
	@phone varchar(12),
	@email varchar(100)
)
as
begin
	if @name in (select supplier_name from Supplier)
	begin
		raiserror('Tên nhà cung cấp đã tồn tại', 16, 10);
		return;
	end
	
	insert into Supplier
	values (@name, @phone, @email);
end;

create proc Create_goods
(
	@item int,
	@sell_price real,
	@supplied_by varchar(30)
)
as
begin
	if @item not in (select ID from Items)
	begin
		raiserror('Mã hàng hóa không hợp lệ', 16, 1);
		return;
	end

	if @supplied_by not in (select supplier_name from Supplier)
	begin
		raiserror('Nhà cung cấp không tồn tại', 16, 2);
		return;
	end

	if @sell_price is null or @sell_price <= 0
	begin
		raiserror('Mệnh giá không hợp lệ', 16, 3);
		return;
	end

	declare @next_id int = -1;
	select @next_id = max(ID) from Goods;

	if @next_id is null
		set @next_id = 0;
	else
		set @next_id = @next_id + 1;

	insert into Goods
	values(@item, @next_id, @sell_price, @supplied_by, null, 'available');
end;