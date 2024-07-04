USE ecommerce 
GO

------------
-- BRANDS --
------------

INSERT INTO
	Brands
	(BrandName)
VALUES
	('Samsung'),
	('Apple'),
	('Sony'),
	('Huawei'),
	('Motorola');

GO

----------------
-- CATEGORIES --
----------------

INSERT INTO
	Categories
	(CategoryName)
VALUES
	('Celulares'),
	('Televisores'),
	('Media'),
	('Audio'),
	('Entretenimiento');

GO

--------------
-- PRODUCTS --
--------------

INSERT INTO
	Products
	(
	Code,
	ProductName,
	ProductDescription,
	Price,
	Cost,
	Stock,
	BrandId
	)
VALUES
	(
		'S01',
		'Galaxy S10',
		'El Samsung Galaxy S10 es una línea de teléfonos inteligentes Android de gama alta fabricados por Samsung. La línea incluye los modelos S10, S10+, S10e y S10 5G',
		'399999.99',
		'255630.95',
		'5',
		'1'
	),
	(
		'M03',
		'Moto G Play 7ma Gen',
		'Momentos únicos, capturas reales. Capturá tus mejores momentos y revivilos cuando quieras con la cámara trasera de 13 Mpx.',
		'228576',
		'130200',
		'10',
		'5'
	),
	(
		'S99',
		'Play Station 5',
		'Experimenta una velocidad sorprendente con una SSD de velocidad ultrarrápida, una inmersión más profunda con soporte para respuesta háptica, gatillos adaptables y audio 3D',
		'1350000',
		'895300',
		'15',
		'3'
	),
	(
		'S56',
		'X80J Curvo 3D 4k',
		'Disfruta de tu contenido favorito de Google TV en 4K HDR gracias al color, el contraste y la claridad de alta calidad de BRAVIA.',
		'1899999',
		'1230000',
		'20',
		'3'
	),
	(
		'A23',
		'Apple TV',
		'Con este dispositivo podrás acceder a diversas aplicaciones para disfrutar de todas las herramientas y funcionalidades multimedia que ofrece. ¡Entretenimiento asegurado!',
		'269990',
		'215000',
		'0',
		'2'
	),	
	(
		'APL-VP-1',
		'Vision Pro',
		'Apple Vision Pro combina a la perfección el contenido digital con tu espacio físico.
		 Para que pueda trabajar, mirar, revivir recuerdos y conectarse de maneras nunca antes posibles.
		 La era de la computación espacial ya está aquí.',
		'5600000',
		'4050000',
		'4',
		'2'
	),
	(
		'APL-IPRO-15',
		'iPhone 15 Pro',
		'Forjado en titanio y equipado con el revolucionario chip A17 Pro, un Botón de Acción personalizable 
		y el sistema de cámaras Pro más versátil. Tiene un diseño resistente y ligero, con titanio de calidad 
		aeroespacial y parte posterior de vidrio mate texturizado.',
		'1450000',
		'995000',
		'8',
		'2'
	);

GO

---------------------------
-- CATEGORIES X PRODUCTS --
---------------------------

INSERT INTO
	ProductCategories
	(ProductId, CategoryId)
VALUES
	(1,1),
	(2,1),
	(3,3),
	(4,2),
	(5,2),
	(2,3),
	(3,5),
	(6,5),
	(6,3),
	(7,1);

GO

-----------------------
-- FEATURED PRODUCTS --
-----------------------
INSERT INTO
	FeaturedProducts
	(ProductId, DisplayOrder, ShowAsNew)
VALUES
	(3,0,0),
	(1,1,0),
	(6,2,0)

------------
-- IMAGES --
------------

INSERT INTO
	Images
	(ImageUrl, ProductId)
VALUES
	(
		'https://ik.imagekit.io/tpce16/products/S10-01-1024x1024.png?updatedAt=1717372633354',
		'1'
	),
	(
		'https://ik.imagekit.io/tpce16/products/prod-171-c1c8ff3e46f669939c15718532203779-1024-1024.jpg?updatedAt=1717374050896',
		'1'
	),
	(
		'https://ik.imagekit.io/tpce16/products/motogplay7magen_1024x1024.png?updatedAt=1717373361655',
		'2'
	),
	(
		'https://ik.imagekit.io/tpce16/products/723755131859-001-1400Wx1400H.jpeg?updatedAt=1717374542249',
		'2'
	),
	(
		'https://ik.imagekit.io/tpce16/products/Playstation-5.webp?updatedAt=1717373430693',
		'3'
	),
	(
		'https://ik.imagekit.io/tpce16/products/783661-11-729d40384c5a2ec86916826086826570-1024-1024.jpg?updatedAt=1717374648104',
		'3'
	),
	(
		'https://ik.imagekit.io/tpce16/products/Sonyx80j_4S._SL1500_1024x.webp?updatedAt=1717373650982',
		'4'
	),
	(
		'https://ik.imagekit.io/tpce16/products/Apple-Vision-Pro-1024x1024.png?updatedAt=1718281729142',
		'6'
	),
	(
		'https://ik.imagekit.io/tpce16/products/06techfix-top-fzqj-mobileMasterAt3x-1024x1024.jpg?updatedAt=1718281754644',
		'6'
	),
	(
		'https://ik.imagekit.io/tpce16/products/iphone-15-pro-a-2f70988805588cc27816964316066050-1024-1024.png?updatedAt=1718288634185',
		'7'
	),
	(
		'https://ik.imagekit.io/tpce16/products/IPHONE15PRO_WHITE_1024x1024.png_v=1710128219?updatedAt=1718288712790',
		'7'
	);

GO

---------------
-- COUNTRIES --
---------------

INSERT INTO
	Countries
	(CountryName)
VALUES
	('Argentina'),
	('Estados Unidos'),
	('España'),
	('Brasil');

GO

---------------
-- PROVINCES --
---------------

INSERT INTO
	Provinces
	(ProvinceName, CountryId)
VALUES
	('Buenos Aires', '1'),
	('Córdoba', '1'),
	('Río Negro', '1');

GO

------------
-- CITIES --
------------

INSERT INTO
	Cities
	(CityName, ZipCode, ProvinceId)
VALUES
	('CABA', '1000', '1'),
	('Villa Adelina', '1607', '1'),
	('General Pacheco', '1617', '1'),
	('San Fernando', '1646', '1'),
	('Tigre', '1648', '1'),
	('Don Torcuato', '1617', '1'),
	('Villa Carlos Paz', '5152', '2'),
	('San Carlos de Bariloche', '8400', '3');

GO

---------------
-- ADDRESSES --
---------------

INSERT INTO
	Addresses
	(StreetName, StreetNumber, Flat, Details, CityId)
VALUES
	(
		'Piedra Buena',
		'389',
		'2C',
		'En frente de las vías',
		'2'
	),
	(
		'9 de Julio',
		'1290',
		'2C',
		'No anda el timbre',
		'1'
	),
	('Córdoba', '2345', '9B', 'Puerta roja', '1'),
	('Perón', '345', '', '', '4'),
	('Cazón', '768', '', '', '5'),
	('Santa Fé', '1290', '', '', '3');

GO

------------
-- PEOPLE --
------------

INSERT INTO
	People
	(
	FirstName,
	LastName,
	TaxCode,
	Phone,
	Email,
	Birth,
	AddressId
	)
VALUES
	('Ana', 'Bertello', NULL, NULL, NULL, NULL, 1),
	('Facundo', 'Masino', NULL, NULL, NULL, NULL, 2),
	('Maximiliano', 'Malvicino', NULL, NULL, NULL, NULL, 3),
	('Carlos', 'Paz', NULL, NULL, NULL, NULL, NULL);

GO

-----------
-- ROLES --
-----------

INSERT INTO
	Roles
	(RoleName)
VALUES
	('Admin'),
	('Cliente'),
	('Visitante');

GO

-----------
-- USERS --
-----------

INSERT INTO
	Users
	(Username, UserPassword, RoleId, PersonId)
VALUES
	('Ani77aa', 'restrepo', '1', '1'),
	('FacuMasino', 'donpablo', '2', '2'),
	('mrmalvicino', 'elchili', '2', '3');

GO

-------------------
-- ORDERSTATUSES --
-------------------

INSERT INTO
	OrderStatuses
	(OrderStatusName)
VALUES
	('Pago en proceso'), -- ID 1 Hardcoded
	('Envío pendiente'), -- ID 2 Hardcoded
	('Pedido enviado'), -- ID 3 Hardcoded
	('Pedido entregado'), -- ID 4 Hardcoded
	('Orden completada'), -- ID 5 Hardcoded
	('Pago y retiro pendientes'), -- ID 6 Hardcoded
	('Retiro pendiente'), -- ID 7 Hardcoded
	('Devolución pendiente'), -- ID 8 Hardcoded
	('Orden cancelada'); -- ID 9 Hardcoded

GO

---------------------------
-- DISTRIBUTION CHANNELS --
---------------------------

INSERT INTO
	DistributionChannels
	(DistributionChannelName)
VALUES
	('Pago virtual y envío'),
	('Pago virtual y retiro'),
	('Pago personal y retiro');

GO

---------------------
-- CHANNELSTATUSES --
---------------------

INSERT INTO
	ChannelStatuses
	(DistributionChannelId, OrderStatusId, OrderStatusIndex) -- OrderStatusIndex Hardcoded
VALUES
	(1, 1, 0),
	(1, 2, 1),
	(1, 3, 2),
	(1, 4, 3),
	(1, 5, 4),
	(1, 8, 5),
	(1, 9, 6),
	(2, 1, 0),
	(2, 7, 1),
	(2, 5, 2),
	(2, 8, 3),
	(2, 9, 4),
	(3, 6, 0),
	(3, 5, 1),
	(3, 8, 2),
	(3, 9, 3);

GO

------------
-- ORDERS --
------------

INSERT INTO
	Orders
	(CreationDate, DeliveryDate, DeliveryAddressId, OrderStatusId, PersonId, DistributionChannelId)
VALUES
	('2024-02-20', NULL, 4, 2, 2, 1),
	('2024-03-30', NULL, NULL, 7, 3, 2),
	('2024-07-02', NULL, NULL, 5, 4, 3);

GO

-----------------------
-- ORDERS X PRODUCTS --
-----------------------

INSERT INTO
	OrderProducts
	(OrderId, ProductId, Amount)
VALUES
	(1, 1, 1),
	(1, 2, 2),
	(2, 3, 3),
	(2, 4, 4),
	(2, 5, 5),
	(3, 6, 1);

GO