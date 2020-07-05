-- Create Database
create database DB_AS_2 on primary
(
	name = 'DB_AS_2',
	filename = 'D:\Semester\192\Database System\Assignment\Database\DB_AS_2.mdf',
	size = 10240KB,
	maxsize = unlimited,
	filegrowth = 1024KB
)
log on
(
	name = 'DB_AS_2_log',
	filename = 'D:\Semester\192\Database System\Assignment\Database\DB_AS_2.ldf',
	size = 1024KB,
	maxsize = 2048KB,
	filegrowth = 10%
);


-- Create table
create table Customer
(
	ID int  primary key not null,
	username varchar(100) not null,
	fullname varchar(100) not null,
	phone_number varchar(12),
	email varchar(30),
	user_pass varchar(30),
	basket_owned int not null
)

create table Delivery
(
	delivery_name varchar(100) primary key,
	country_code varchar(100) not null,
	region varchar(100),
	city varchar(100),
	phone varchar(12)
)

create table Delivery_Bill
(
	ID int  primary key not null,
	delivery_date datetime not null,
	given_to varchar(100) not null,
	part_of int not null
)

create table Sell_Bill
(
	ID int  primary key not null,
	summary int,
	paid_date datetime,
	paid_for int not null
)

create table Basket
(
	ID int  primary key not null,
	customer_ID int not null,
	item_count int,
	order_date datetime null,
	total real
)

create table Comment
(
	ID int  primary key not null,
	content varchar(100),
	rating real,
	created_date datetime,
	created_by int not null,
	related_to int not null
)

create table Items
(
	ID int  primary key not null,
	item_descripion varchar(100),
	item_name varchar(100),

	is_pc varchar(1) default 'F',
		pc_setting varchar(100),
		pc_manufacturer varchar(20),

	is_laptop varchar(1) default 'F',
		laptop_setting varchar(100),
		laptop_manufacturer varchar(20),

	is_phone varchar(1) default 'F',
		phone_core varchar(20),
		phone_feature varchar(100),

	is_accessory varchar(1) default 'F',

	average_rating real default 5;
)

create table Item_Image
(
	item_ID int  not null,
	img_path varchar(20) not null,

	primary key (item_ID, img_path)
)

create table Goods
(
	in_item int not null,
	ID int not null,
	sell_price real not null,
	supplied_by varchar(30),
	contained_in int null,
	item_status varchar(10) default 'available',

	primary key (in_item, ID)
)

create table Supplier
(
	supplier_name varchar(30) not null primary key,
	phone_number varchar(12),
	email varchar(30)
)

-- Change datetime format
set dateformat dmy;

-- Create Foregin Key
alter table Basket
add constraint fk_basket_owned_by_customer 
foreign key(customer_ID) references Customer(ID);

alter table Comment
add constraint fk_comment_created_by_customer
foreign key(created_by) references Customer(ID);

alter table Comment
add constraint fk_comment_realated_to_product
foreign key(related_to) references Items(ID);

alter table Delivery_Bill
add constraint fk_bill_given_to_delivery
foreign key(given_to) references Delivery(delivery_name);

alter table Goods
add constraint fx_goods_in_items
foreign key(in_item) references Items(ID);

alter table Goods
add constraint fx_goods_supplied_by_supplier
foreign key(supplied_by) references Supplier(supplier_name);

alter table Goods
add constraint fx_goods_contained_in_basket
foreign key(contained_in) references Basket(ID);

alter table Item_Image
add constraint fk_image_of_item
foreign key(item_ID) references Items(ID);

alter table Delivery_Bill
add constraint fk_delivery_bill_part_of_sell_bill
foreign key(part_of) references Sell_Bill(ID);

alter table Sell_Bill
add constraint fk_sell_bill_paid_for_basket
foreign key(paid_for) references Basket(ID);
