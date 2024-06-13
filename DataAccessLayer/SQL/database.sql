CREATE
DATABASE ecommerce
COLLATE Latin1_General_100_CI_AS_SC_UTF8;

GO
USE ecommerce 
GO

------------
-- BRANDS --
------------

CREATE TABLE Brands
(
	BrandId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	BrandName varchar(30) UNIQUE NOT NULL
) 
GO

----------------
-- CATEGORIES --
----------------

CREATE TABLE Categories
(
	CategoryId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CategoryName varchar(30) UNIQUE NOT NULL
) 
GO

--------------
-- PRODUCTS --
--------------

CREATE TABLE Products
(
	ProductId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	Code varchar(50) NOT NULL UNIQUE,
	ProductName varchar(50) NOT NULL,
	ProductDescription varchar(300) NULL,
	Price money NOT NULL,
	Cost money NOT NULL,
	Stock int check(0 <= Stock) NOT NULL,
	BrandId int FOREIGN KEY REFERENCES Brands (BrandId) NULL
)
GO

------------------------
-- PRODUCT CATEGORIES --
------------------------

CREATE TABLE ProductCategories
(
	ProductId int FOREIGN KEY REFERENCES Products(ProductId) NOT NULL,
	CategoryId int FOREIGN KEY REFERENCES Categories(CategoryId) NOT NULL,
	PRIMARY KEY (ProductId, CategoryId)
)
GO

------------
-- IMAGES --
------------

CREATE TABLE Images
(
	ImageId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	ImageUrl varchar(300) UNIQUE NOT NULL,
	ProductId int FOREIGN KEY REFERENCES Products (ProductId) NOT NULL
) 
GO

---------------
-- COUNTRIES --
---------------

CREATE TABLE Countries
(
	CountryId tinyint PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CountryName varchar(30) UNIQUE NOT NULL
) 
GO

---------------
-- PROVINCES --
---------------

CREATE TABLE Provinces
(
	ProvinceId smallint PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	ProvinceName varchar(30) NOT NULL,
	CountryId tinyint FOREIGN KEY REFERENCES Countries (CountryId) NOT NULL,
	CONSTRAINT UC_Province UNIQUE (ProvinceName, CountryId)
) 
GO

------------
-- CITIES --
------------

CREATE TABLE Cities
(
	CityId smallint PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CityName varchar(30) NOT NULL,
	ZipCode varchar(30) NULL,
	ProvinceId smallint FOREIGN KEY REFERENCES Provinces (ProvinceId) NOT NULL,
	CONSTRAINT UC_City UNIQUE (CityName, ProvinceId)
) 
GO

--------------
-- ADRESSES --
--------------

CREATE TABLE Adresses
(
	AdressId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	StreetName varchar(30) NOT NULL,
	StreetNumber varchar(10) NOT NULL,
	Flat varchar(30) NULL,
	Details varchar(300) NULL,
	CityId smallint FOREIGN KEY REFERENCES Cities (CityId) NOT NULL,
	CONSTRAINT UC_Adress UNIQUE (StreetName, StreetNumber, CityId)
) 
GO

------------
-- PEOPLE --
------------

CREATE TABLE People
(
	PersonId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	PersonStatus bit NOT NULL DEFAULT (1),
	PersonFirstName varchar(30) NOT NULL,
	PersonLastName varchar(30) NOT NULL,
	TaxCode varchar(30) NULL,
	Phone varchar(30) NULL,
	Email varchar(30) NULL,
	Birth date NULL,
	AdressId int FOREIGN KEY REFERENCES Adresses (AdressId) NULL,
) 
GO

-----------
-- ROLES --
-----------

CREATE TABLE Roles
(
	RoleId tinyint PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	RoleName varchar(50) NOT NULL
) 
GO

-----------
-- USERS --
-----------

CREATE TABLE Users
(
	UserId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	UserName varchar(30) NOT NULL,
	UserPassword varchar(30) NOT NULL,
	RoleId tinyint FOREIGN KEY REFERENCES Roles (RoleId) NOT NULL,
	PersonId int FOREIGN KEY REFERENCES People (PersonId) NOT NULL
) 
GO

-------------------
-- ORDERSTATUSES --
-------------------

CREATE TABLE OrderStatuses
(
	OrderStatusId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	OrderStatusName varchar(30) NOT NULL
) 
GO

------------
-- ORDERS --
------------

CREATE TABLE Orders
(
	OrderId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	OrderDate datetime DEFAULT (getdate()) NOT NULL,
	AdressId int FOREIGN KEY REFERENCES Adresses (AdressId) NULL,
	OrderStatusId int FOREIGN KEY REFERENCES OrderStatuses (OrderStatusId) NOT NULL,
	UserId int FOREIGN KEY REFERENCES Users (UserId) NOT NULL
) 
GO

--------------------
-- PRODUCT ORDERS --
--------------------

CREATE TABLE ProductOrders
(
	OrderId int FOREIGN KEY REFERENCES Orders (OrderId) NOT NULL,
	ProductId int FOREIGN KEY REFERENCES Products (ProductId) NOT NULL,
	Amount int DEFAULT (1) NOT NULL,
	PRIMARY KEY (OrderId, ProductId)
) 
GO