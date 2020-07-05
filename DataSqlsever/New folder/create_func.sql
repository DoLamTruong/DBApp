create function Get_customer_history
(
	@customer_id int,
	@start_date datetime,
	@end_date datetime
)
returns @re
table
(
	item_count int,
	order_date datetime,
	paid_date datetime,
	delivery_by varchar(100),
	delivery_date datetime
)
as
begin
	insert into @re
	select item_count, total, order_date, paid_date, delivery_name, delivery_date
	from
	(
		( Basket join Sell_Bill on Basket.ID = Sell_Bill.ID)
		join (Delivery_Bill join Delivery on given_to = delivery_name)
		on part_of = Sell_Bill.ID
	)
	where customer_ID = @customer_id;

	return;
end;

create function Get_suppliers_by_item_name
(
	@item_name varchar(100)
)
returns @re
table
(
	item_name varchar(100),
	supplier varchar(100),
	sell_price real,
	items_status varchar(10)
)
as
begin
	insert into @re
		select item_name, supplier_name, sell_price, item_status
		from
		(
			(select distinct supplier_name, sell_price, item_status, in_item
			    from (Supplier join Goods on supplied_by = supplier_name)) as E
			join Items
			on Items.ID = E.in_item
		)
		where item_name = @item_name and item_status = 'available';

	return;
end;