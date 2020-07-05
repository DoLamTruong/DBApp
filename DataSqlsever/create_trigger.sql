create trigger Compute_total_basket
on Goods
after update
as
begin
	declare @table table(id int primary key, contained_in int, total real, item_count int);
	update Basket set item_count = 0, total = 0 where ID in (select contained_in from inserted) or ID in (select contained_in from deleted);

	insert into @table
	select (ROW_NUMBER() over (order by contained_in)) as num, contained_in, sum(sell_price) as total_cost, count(contained_in) as item_count
	from Goods
	where contained_in is not null and (ID in (select contained_in from inserted) or ID in (select contained_in from deleted))
	group by contained_in;

	declare @count int = 1;
	declare @length int = -1;

	select @length = count(*) from @table;

	while @count <= @length
	begin
		declare @item int;
		declare @total real;
		declare @item_count int;

		select @item = contained_in, @total = total, @item_count = item_count from @table where id = @count;
		update Basket
		set Basket.total = @total, Basket.item_count = @item_count
		where ID = @item;

		set @count = @count + 1;
	end
end;


create trigger Item_on_comment
on Comment
after insert
as
begin
	declare @table table (num int primary key, related_to int, rating real);

	insert into @table
	select row_number() over (order by related_to), related_to, rating
	from inserted;

	declare @count int = 1;
	declare @length int = -1;
	select @length = count(*) from @table;

	while @count <= @length
	begin
		declare @related_to int = 0;
		declare @rating real = 0;

		select @related_to = related_to, @rating = rating from @table;
		update Items
		set average_rating = 0.5 * average_rating + 0.5 * @rating
		where ID = @related_to;

		set @count = @count + 1;
	end
end;