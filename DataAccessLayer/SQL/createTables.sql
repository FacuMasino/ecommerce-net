USE ecommerce_net_db

GO

------------
-- BRANDS --
------------

CREATE TABLE Brands
(
	BrandId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	IsActive bit default(1) NOT NULL,
	BrandName varchar(30) UNIQUE NOT NULL
) 
GO

----------------
-- CATEGORIES --
----------------

CREATE TABLE Categories
(
	CategoryId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	IsActive bit default(1) NOT NULL,
	CategoryName varchar(30) UNIQUE NOT NULL
) 
GO

--------------
-- PRODUCTS --
--------------

CREATE TABLE Products
(
	ProductId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	IsActive bit default(1) NOT NULL,
	Code varchar(50) NOT NULL UNIQUE,
	ProductName varchar(50) NOT NULL,
	ProductDescription varchar(300) NULL,
	Price money NOT NULL,
	Cost money NOT NULL,
	Stock int check(0 <= Stock) NOT NULL,
	BrandId int FOREIGN KEY REFERENCES Brands(BrandId) NULL,
	TotalVisits int default 0
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

-----------------------
-- FEATURED PRODUCTS --
-----------------------

CREATE TABLE FeaturedProducts
(
    FeaturedProductId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    ProductId int FOREIGN KEY REFERENCES Products(ProductId) NOT NULL UNIQUE,
    DisplayOrder int NOT NULL,
	ShowAsNew bit NOT NULL DEFAULT 0
)
GO

------------
-- IMAGES --
------------

CREATE TABLE Images
(
	ImageId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	ImageUrl varchar(300) NOT NULL,
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

---------------
-- ADDRESSES --
---------------

CREATE TABLE Addresses
(
	AddressId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	StreetName varchar(30) NOT NULL,
	StreetNumber varchar(10) NOT NULL,
	Flat varchar(30) NULL,
	Details varchar(300) NULL,
	CityId smallint FOREIGN KEY REFERENCES Cities (CityId) NOT NULL,
	CONSTRAINT UC_Address UNIQUE (StreetName, StreetNumber, CityId)
) 
GO

------------
-- PEOPLE --
------------

CREATE TABLE People
(
	PersonId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	IsActive bit NOT NULL DEFAULT (1),
	FirstName varchar(30) NOT NULL,
	LastName varchar(30) NOT NULL,
	TaxCode varchar(30) NULL,
	Phone varchar(30) NULL,
	Email varchar(30) UNIQUE NOT NULL,
	Birth date NULL,
	AddressId int FOREIGN KEY REFERENCES Addresses (AddressId) NULL,
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
	Username varchar(30) NULL,
	UserPassword varchar(30) NOT NULL,
	PersonId int FOREIGN KEY REFERENCES People (PersonId) NOT NULL
) 
GO

----------------
-- USER ROLES --
----------------

CREATE TABLE UserRoles
(
	UserId int FOREIGN KEY REFERENCES Users (UserId) NOT NULL,
	RoleId tinyint FOREIGN KEY REFERENCES Roles (RoleId) NOT NULL,
	PRIMARY KEY (UserId, RoleId)
) 
GO

-------------------
-- ORDERSTATUSES --
-------------------

CREATE TABLE OrderStatuses
(
	OrderStatusId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	OrderStatusName varchar(30) NOT NULL,
	TransitionText varchar(50) NOT NULL,
	AcceptedText varchar(50) NOT NULL,
	RoleId tinyint FOREIGN KEY REFERENCES Roles (RoleId) NOT NULL
) 
GO

-------------------
-- PAYMENT TYPES --
-------------------

CREATE TABLE PaymentTypes
(
	PaymentTypeId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	PaymentTypeName varchar(30) NOT NULL
)
GO

---------------------------
-- DISTRIBUTION CHANNELS --
---------------------------

CREATE TABLE DistributionChannels
(
	DistributionChannelId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	DistributionChannelName varchar(50) NOT NULL
)
GO

---------------------
-- CHANNELSTATUSES --
---------------------

CREATE TABLE ChannelStatuses
(
	DistributionChannelId int FOREIGN KEY REFERENCES DistributionChannels(DistributionChannelId) NOT NULL,
	OrderStatusId int FOREIGN KEY REFERENCES OrderStatuses (OrderStatusId) NOT NULL,
	OrderStatusIndex int NOT NULL,
	PRIMARY KEY (DistributionChannelId, OrderStatusId)
)
GO

------------
-- ORDERS --
------------

CREATE TABLE Orders
(
	OrderId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CreationDate datetime DEFAULT (getdate()) NOT NULL,
	DeliveryDate datetime NULL,
	DeliveryAddressId int FOREIGN KEY REFERENCES Addresses (AddressId) NULL,
	OrderStatusId int FOREIGN KEY REFERENCES OrderStatuses (OrderStatusId) NOT NULL,
	PersonId int FOREIGN KEY REFERENCES People (PersonId) NOT NULL,
	DistributionChannelId int FOREIGN KEY REFERENCES DistributionChannels (DistributionChannelId) NOT NULL,
	PaymentTypeId int FOREIGN KEY REFERENCES PaymentTypes (PaymentTypeId) NOT NULL
)
GO

--------------------
-- ORDER PRODUCTS --
--------------------

CREATE TABLE OrderProducts
(
	OrderId int FOREIGN KEY REFERENCES Orders (OrderId) NOT NULL,
	ProductId int FOREIGN KEY REFERENCES Products (ProductId) NOT NULL,
	Quantity int DEFAULT (1) NOT NULL,
	PRIMARY KEY (OrderId, ProductId)
) 
GO