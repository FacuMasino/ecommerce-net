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
	Code varchar(50) not null,
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

---------------
-- COUNTRIES --
---------------

create table Countries(
	CountryId tinyint primary key identity(1,1) not null,
	CountryName varchar(30) unique not null
)
go

insert into Countries
(CountryName)
values
('Argentina'),
('Estados Unidos'),
('España'),
('Brasil');
go

---------------
-- PROVINCES --
---------------

create table Provinces(
	ProvinceId smallint primary key identity(1,1) not null,
	ProvinceName varchar(30) not null,
	CountryId tinyint foreign key references Countries(CountryId) not null,
	constraint UC_Province unique (ProvinceName, CountryId)
)
go

insert into Provinces
(ProvinceName, CountryId)
values
('Buenos Aires', '1'),
('Córdoba', '1'),
('Río Negro', '1');
go

------------
-- CITIES --
------------

create table Cities(
	CityId smallint primary key identity(1,1) not null,
	CityName varchar(30) not null,
	ZipCode varchar(30) null,
	ProvinceId smallint foreign key references Provinces(ProvinceId) not null,
	constraint UC_City unique (CityName, ProvinceId)
)
go

insert into Cities
(CityName, ZipCode, ProvinceId)
values
('CABA', '1000', '1'),
('Villa Adelina', '1607', '1'),
('General Pacheco', '1617', '1'),
('San Fernando', '1646', '1'),
('Tigre', '1648', '1'),
('Don Torcuato', '1617', '1'),
('Villa Carlos Paz', '5152', '2'),
('San Carlos de Bariloche', '8400', '3');
go

--------------
-- ADRESSES --
--------------

create table Adresses(
	AdressId int primary key identity(1,1) not null,
	StreetName varchar(30) not null,
	StreetNumber varchar(10) not null,
	Flat varchar(30) null,
	Details varchar(300) null,
	CityId smallint foreign key references Cities(CityId) not null,
	constraint UC_Adress unique (StreetName, StreetNumber, CityId)
)
go

insert into Adresses
(StreetName, StreetNumber, Flat, Details, CityId)
values
('Piedra Buena', '389', '2C', 'En frente de las vías', '2'),
('9 de Julio', '1290', '2C', 'No anda el timbre', '1'),
('Córdoba', '2345', '9B', 'Puerta roja', '1'),
('Perón', '345', '', '', '4'),
('Cazón', '768', '', '', '5'),
('Santa Fé', '1290', '', '', '3');
go

------------
-- PEOPLE --
------------

create table People(
	PersonId int primary key identity(1,1) not null,
	PersonStatus bit not null default(1),
	PersonFirstName varchar(30) not null,
	PersonLastName varchar(30) not null,
	TaxCode varchar(30) null,
	Phone varchar(30) null,
	Email varchar(30) null,
	Birth date null,
	AdressId int foreign key references Adresses(AdressId) null,
)
go

insert into People
(PersonFirstName, PersonLastName, TaxCode, Phone, Email, Birth, AdressId)
values
('Ana', 'Bertello', null, null, null, null, 1),
('Facundo', 'Masino', null, null, null, null, 2),
('Maximiliano', 'Malvicino', null, null, null, null, 3);
go

-----------
-- ROLES --
-----------

create table Roles(
	RoleId tinyint primary key identity(1,1) not null,
	RoleName varchar(50) not null
)
go

insert into Roles
(RoleName)
values
('Admin'),
('Customer');
go

-----------
-- USERS --
-----------

create table Users(
	UserId int primary key identity(1,1) not null,
	UserName varchar(30) not null,
	UserPassword varchar(30) not null,
	RoleId tinyint foreign key references Roles(RoleId) not null,
	PersonId int foreign key references People(PersonId) not null
)
go

insert into Users
(UserName, UserPassword, RoleId, PersonId)
values
('Ani77aa', 'restrepo', '1', '1'),
('FacuMasino', 'donpablo', '2', '2'),
('mrmalvicino', 'elchili', '2', '3');
go

-------------------
-- ORDERSTATUSES --
-------------------

create table OrderStatuses(
	OrderStatusId int primary key identity(1,1) not null,
	OrderStatusName varchar(30) not null
)
go

insert into OrderStatuses
(OrderStatusName)
values
('Orden generada'),
('Pago pendiente'),
('Procesando pago'),
('Envío pendiente'),
('Pedido enviado'),
('Pedido entregado'),
('Orden completada'),
('Devolución pendiente'),
('Compra devuelta'),
('Compra cancelada'),
('Pago y retiro pendiente'),
('Retiro pendiente');
go

------------
-- ORDERS --
------------

create table Orders(
	OrderId int primary key identity(1,1) not null,
	OrderDate datetime default(getdate()) not null,
	AdressId int foreign key references Adresses(AdressId) null,
	OrderStatusId int foreign key references OrderStatuses(OrderStatusId) not null,
	UserId int foreign key references Users(UserId) not null
)
go

insert into Orders
(OrderDate, AdressId, OrderStatusId, UserId)
values
('2024-02-20', '4', '1', '2'),
('2024-03-30', null, '1', '3');
go

-----------------------
-- ORDERS X PRODUCTS --
-----------------------

create table OrdersXProducts(
	OrderId int foreign key references Orders(OrderId) not null,
	ProductId int foreign key references Products(ProductId) not null,
	Amount int default(1) not null
)
go

insert into OrdersXProducts
(OrderId, ProductId, Amount)
values
('1', '1', '1'),
('1', '2', '2'),
('2', '3', '3'),
('2', '4', '4'),
('2', '5', '5');
go