create database ecommerce
collate Latin1_General_100_CI_AS_SC_UTF8;
go

use ecommerce
go

------------
-- BRANDS --
------------

create table Brands(
	BrandId int primary key identity(1,1) not null,
	BrandName varchar(30) unique not null
)
go

insert into Brands
(BrandName)
values
('Samsung'),
('Apple'),
('Sony'),
('Huawei'),
('Motorola');
go

----------------
-- CATEGORIES --
----------------

create table Categories(
	CategoryId int primary key identity(1,1) not null,
	CategoryName varchar(30) unique not null
)
go

insert into Categories
(CategoryName)
values
('Celulares'),
('Televisores'),
('Media'),
('Audio');
go

--------------
-- PRODUCTS --
--------------

create table Products(
	ProductId int primary key identity(1,1) not null,
	Code varchar(50) null,
	ProductName varchar(50) not null,
	ProductDescription varchar(150) null,
	Price decimal(15,2) not null,
	BrandId int foreign key references Brands(BrandId) null,
	CategoryId int foreign key references Categories(CategoryId) null
)
go

insert into Products
(Code, ProductName, ProductDescription, Price, BrandId, CategoryId)
values
('S01', 'Galaxy S10', 'Una canoa cara', '69.99', '1', '1'),
('M03', 'Moto G Play 7ma Gen', 'Ya siete de estos?', '15699', '5', '1'),
('S99', 'Play 4', 'Ya no se cuantas versiones hay', '35000', '3', '3'),
('S56', 'Bravia 55', 'Alta tele', '49500', '3', '2'),
('A23', 'Apple TV', 'lindo loro', '7850', '2', '2');
go

------------
-- IMAGES --
------------

create table Images(
	ImageId int primary key identity(1,1) not null,
	ImageUrl varchar(300) unique not null,
	ProductId int foreign key references Products(ProductId) not null
)
go

insert into Images
(ImageUrl, ProductId)
values
('https://images.samsung.com/is/image/samsung/co-galaxy-s10-sm-g970-sm-g970fzyjcoo-frontcanaryyellow-thumb-149016542', '1'),
('https://i.blogs.es/9da288/moto-g7-/1366_2000.jpg', '2');
go