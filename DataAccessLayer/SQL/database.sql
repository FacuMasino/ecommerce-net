create
database ecommerce
collate Latin1_General_100_CI_AS_SC_UTF8;

go use ecommerce go
------------
-- BRANDS --
------------
create table
	Brands (
		BrandId int primary key identity(1, 1) not null,
		BrandName varchar(30) unique not null
	) go
	----------------
	-- CATEGORIES --
	----------------
create table
	Categories (
		CategoryId int primary key identity(1, 1) not null,
		CategoryName varchar(30) unique not null
	) go
	--------------
	-- PRODUCTS --
	--------------
create table
	Products (
		ProductId int primary key identity(1, 1) not null,
		Code varchar(50) not null,
		ProductName varchar(50) not null,
		ProductDescription varchar(150) null,
		Price decimal(15, 2) not null,
		BrandId int foreign key references Brands (BrandId) null,
		CategoryId int foreign key references Categories (CategoryId) null
	) go
	------------
	-- IMAGES --
	------------
create table
	Images (
		ImageId int primary key identity(1, 1) not null,
		ImageUrl varchar(300) unique not null,
		ProductId int foreign key references Products (ProductId) not null
	) go
	---------------
	-- COUNTRIES --
	---------------
create table
	Countries (
		CountryId tinyint primary key identity(1, 1) not null,
		CountryName varchar(30) unique not null
	) go
	---------------
	-- PROVINCES --
	---------------
create table
	Provinces (
		ProvinceId smallint primary key identity(1, 1) not null,
		ProvinceName varchar(30) not null,
		CountryId tinyint foreign key references Countries (CountryId) not null,
		constraint UC_Province unique (ProvinceName, CountryId)
	) go
	------------
	-- CITIES --
	------------
create table
	Cities (
		CityId smallint primary key identity(1, 1) not null,
		CityName varchar(30) not null,
		ZipCode varchar(30) null,
		ProvinceId smallint foreign key references Provinces (ProvinceId) not null,
		constraint UC_City unique (CityName, ProvinceId)
	) go
	--------------
	-- ADRESSES --
	--------------
create table
	Adresses (
		AdressId int primary key identity(1, 1) not null,
		StreetName varchar(30) not null,
		StreetNumber varchar(10) not null,
		Flat varchar(30) null,
		Details varchar(300) null,
		CityId smallint foreign key references Cities (CityId) not null,
		constraint UC_Adress unique (StreetName, StreetNumber, CityId)
	) go
	------------
	-- PEOPLE --
	------------
create table
	People (
		PersonId int primary key identity(1, 1) not null,
		PersonStatus bit not null default (1),
		PersonFirstName varchar(30) not null,
		PersonLastName varchar(30) not null,
		TaxCode varchar(30) null,
		Phone varchar(30) null,
		Email varchar(30) null,
		Birth date null,
		AdressId int foreign key references Adresses (AdressId) null,
	) go
	-----------
	-- ROLES --
	-----------
create table
	Roles (
		RoleId tinyint primary key identity(1, 1) not null,
		RoleName varchar(50) not null
	) go
	-----------
	-- USERS --
	-----------
create table
	Users (
		UserId int primary key identity(1, 1) not null,
		UserName varchar(30) not null,
		UserPassword varchar(30) not null,
		RoleId tinyint foreign key references Roles (RoleId) not null,
		PersonId int foreign key references People (PersonId) not null
	) go
	-------------------
	-- ORDERSTATUSES --
	-------------------
create table
	OrderStatuses (
		OrderStatusId int primary key identity(1, 1) not null,
		OrderStatusName varchar(30) not null
	) go
	------------
	-- ORDERS --
	------------
create table
	Orders (
		OrderId int primary key identity(1, 1) not null,
		OrderDate datetime default (getdate()) not null,
		AdressId int foreign key references Adresses (AdressId) null,
		OrderStatusId int foreign key references OrderStatuses (OrderStatusId) not null,
		UserId int foreign key references Users (UserId) not null
	) go
	-----------------------
	-- ORDERS X PRODUCTS --
	-----------------------
create table
	OrdersXProducts (
		OrderId int foreign key references Orders (OrderId) not null,
		ProductId int foreign key references Products (ProductId) not null,
		Amount int default (1) not null
	) go