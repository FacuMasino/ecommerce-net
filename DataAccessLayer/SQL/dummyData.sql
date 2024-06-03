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
	('Audio');

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
	BrandId,
	CategoryId
	)
VALUES
	(
		'S01',
		'Galaxy S10',
		'El Samsung Galaxy S10 es una línea de teléfonos inteligentes Android de gama alta fabricados por Samsung. La línea incluye los modelos S10, S10+, S10e y S10 5G',
		'69.99',
		'1',
		'1'
	),
	(
		'M03',
		'Moto G Play 7ma Gen',
		'Momentos únicos, capturas reales. Capturá tus mejores momentos y revivilos cuando quieras con la cámara trasera de 13 Mpx.',
		'228576',
		'5',
		'1'
	),
	(
		'S99',
		'Play Station 5',
		'Experimenta una velocidad sorprendente con una SSD de velocidad ultrarrápida, una inmersión más profunda con soporte para respuesta háptica, gatillos adaptables y audio 3D',
		'1350000',
		'3',
		'3'
	),
	(
		'S56',
		'X80J Curvo 3D 4k',
		'Disfruta de tu contenido favorito de Google TV en 4K HDR gracias al color, el contraste y la claridad de alta calidad de BRAVIA.',
		'1899999',
		'3',
		'2'
	),
	(
		'A23',
		'Apple TV',
		'Con este dispositivo podrás acceder a diversas aplicaciones para disfrutar de todas las herramientas y funcionalidades multimedia que ofrece. ¡Entretenimiento asegurado!',
		'269990',
		'2',
		'2'
	);

GO

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
		'https://ik.imagekit.io/tpce16/products/motogplay7magen_1024x1024.png?updatedAt=1717373361655',
		'2'
	),
	(
		'https://ik.imagekit.io/tpce16/products/Playstation-5.webp?updatedAt=1717373430693',
		'3'
	),
	(
		'https://ik.imagekit.io/tpce16/products/Sonyx80j_4S._SL1500_1024x.webp?updatedAt=1717373650982',
		'4'
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

--------------
-- ADRESSES --
--------------

INSERT INTO
	Adresses
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
	PersonFirstName,
	PersonLastName,
	TaxCode,
	Phone,
	Email,
	Birth,
	AdressId
	)
VALUES
	('Ana', 'Bertello', NULL, NULL, NULL, NULL, 1),
	('Facundo', 'Masino', NULL, NULL, NULL, NULL, 2),
	(
		'Maximiliano',
		'Malvicino',
		NULL,
		NULL,
		NULL,
		NULL,
		3
	);

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
	(UserName, UserPassword, RoleId, PersonId)
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

GO

------------
-- ORDERS --
------------

INSERT INTO
	Orders
	(OrderDate, AdressId, OrderStatusId, UserId)
VALUES
	('2024-02-20', '4', '1', '2'),
	('2024-03-30', NULL, '1', '3');

GO

-----------------------
-- ORDERS X PRODUCTS --
-----------------------

INSERT INTO
	OrdersXProducts
	(OrderId, ProductId, Amount)
VALUES
	('1', '1', '1'),
	('1', '2', '2'),
	('2', '3', '3'),
	('2', '4', '4'),
	('2', '5', '5');

GO