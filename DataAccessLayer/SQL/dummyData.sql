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
	),
		(
		'S256',
		'Monitor Samsung',
		'Neftlix en pantalla ergonomica',
		'215000',
		'19500',
		'2',
		'1'
	),
	(
		'P50 Pro',
		'Huawei Pro',
		'Momentos únicos, capturas reales. Especial para ver los videos del profe Maxi.',
		'150000',
		'95000',
		'10',
		'4'
	),
		(
		'MR-40',
		'Motorola Gris Mate',
		'Descubrí la pantalla plegable.Al plegarse, adopta un tamaño pequeño y compacto. ',
		'1299999',
		'950000',
		'2',
		'5'
	);

GO

------------------------
-- PRODUCT CATEGORIES --
------------------------

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
	(7,1),
	(8,2),
    (9,2),
    (10,1);


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
	),
	('https://arrichetta.com.ar/wp-content/uploads/2021/01/LF27T350FHLCZB-1',
		'8'
	),
	(
		'https://arrichetta.com.ar/wp-content/uploads/2021/01/LF27T350FH_3.png',
		'8'
	),
	(
		'https://clevercel.mx/cdn/shop/files/huawei-p30-lite_06ed1a77-5959-4add-8239-fabacde4fd77_700x.png?v=1713365233',
		'9'
	),
	(
		'https://armoto.vtexassets.com/arquivos/ids/165522-800-800?v=638439565882870000&width=800&height=800&aspect=true',
		'10'
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

INSERT INTO Provinces (ProvinceName, CountryId)
VALUES
    ('Buenos Aires', 1), -- ID 1
    ('Catamarca', 1), -- ID 2
    ('Chaco', 1), -- ID 3
    ('Chubut', 1), -- ID 4
    ('Córdoba', 1), -- ID 5
    ('Corrientes', 1), -- ID 6
    ('Entre Ríos', 1), -- ID 7
    ('Formosa', 1), -- ID 8
    ('Jujuy', 1), -- ID 9
    ('La Pampa', 1), -- ID 10
    ('La Rioja', 1), -- ID 11
    ('Mendoza', 1), -- ID 12
    ('Misiones', 1), -- ID 13
    ('Neuquén', 1), -- ID 14
    ('Río Negro', 1), -- ID 15
    ('Salta', 1), -- ID 16
    ('San Juan', 1), -- ID 17
    ('San Luis', 1), -- ID 18
    ('Santa Cruz', 1), -- ID 19
    ('Santa Fe', 1), -- ID 20
    ('Santiago del Estero', 1), -- ID 21
    ('Tierra del Fuego', 1), -- ID 22
    ('Tucumán', 1); -- ID 23

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
	('Villa Carlos Paz', '5152', '5'),
	('San Carlos de Bariloche', '8400', '15');

GO

---------------
-- ADDRESSES --
---------------

INSERT INTO
	Addresses
	(StreetName, StreetNumber, Flat, Details, CityId)
VALUES
	('Piedra Buena', '389', '2C', 'En frente de las vías', '2'),
	('9 de Julio', '1290', '2C', 'No anda el timbre', '1'),
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
	('Ana', 'Bertello', '35147428', NULL, 'ana@outlook.com', NULL, 1),
	('Facundo', 'Masino', NULL, '15548026', 'joaqfm@gmail.com', NULL, 2),
	('Maximiliano', 'Malvicino', NULL, NULL, 'maxi@gmail.com', NULL, 3),
	('Carlos', 'Paz', NULL, NULL, 'carlos@hotmail.com', NULL, NULL),
	('Juan', 'Berlinguieri', NULL, NULL, 'berlinguieri@hotmail.com', NULL, NULL);

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
	('Transportista'),
	('Atención al público');

GO

-----------
-- USERS --
-----------

INSERT INTO
	Users
	(Username, UserPassword, RoleId, PersonId)
VALUES
	('Ani77aa', 'ani', '1', '1'),
	('FacuMasino', 'facu', '1', '2'),
	('mrmalvicino', 'maxi', '1', '3');

GO

-------------------
-- ORDERSTATUSES --
-------------------

INSERT INTO
	OrderStatuses
	(OrderStatusName, Transition, RoleId)
VALUES
	('Pago en proceso', 'Marcar como abonado', 4), -- ID 1 Hardcoded
	('Envío pendiente', 'Marcar como enviado', 4), -- ID 2 Hardcoded
	('Pedido enviado', 'Marcar como entregado', 3), -- ID 3 Hardcoded
	('Pedido entregado', 'Marcar como recibido', 2), -- ID 4 Hardcoded
	('Orden completada', 'Devolver pedido', 2), -- ID 5 Hardcoded
	('Pago y retiro pendientes', 'Marcar como abonado y retirado', 4), -- ID 6 Hardcoded
	('Retiro pendiente', 'Marcar como retirado', 4), -- ID 7 Hardcoded
	('Devolución pendiente', 'Marcar como devuelto', 4), -- ID 8 Hardcoded
	('Orden cancelada', 'Sin acción disponible', 1), -- ID 9 Hardcoded
	('Envío y pago pendientes', 'Marcar como abonado y entregado', 3); -- ID 10 Hardcoded

GO

-------------------
-- PAYMENT TYPES --
-------------------

INSERT INTO
	PaymentTypes
	(PaymentTypeName)
VALUES
	('Efectivo'),
	('Mercado Pago'),
	('Transferencia bancaria');

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
	('Pago en efectivo y retiro'),
	('Pago en efectivo y envío');

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
	(3, 9, 3),
	(4, 10, 0),
	(4, 4, 1),
	(4, 5, 2),
	(4, 8, 3),
	(4, 9, 4);

GO

------------
-- ORDERS --
------------

INSERT INTO
	Orders
	(CreationDate, DeliveryDate, DeliveryAddressId, OrderStatusId, PersonId, DistributionChannelId, PaymentTypeId)
VALUES
	('2024-02-20', NULL, 4, 2, 2, 1, 2),
	('2024-03-30', NULL, NULL, 7, 3, 2, 3),
	('2024-07-02', NULL, NULL, 5, 4, 3, 1),
	('2024-07-02', NULL, 5, 2, 5, 4, 1);

GO

--------------------
-- ORDER PRODUCTS --
--------------------

INSERT INTO
	OrderProducts
	(OrderId, ProductId, Quantity)
VALUES
	(1, 1, 1),
	(1, 2, 2),
	(2, 3, 3),
	(2, 4, 4),
	(2, 5, 5),
	(3, 6, 1),
	(4, 4, 2);

GO