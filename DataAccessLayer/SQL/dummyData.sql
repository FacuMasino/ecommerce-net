use ecommerce go

------------
-- BRANDS --
------------

insert into
	Brands (BrandName)
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

insert into
	Categories (CategoryName)
values
	('Celulares'),
	('Televisores'),
	('Media'),
	('Audio');

go

--------------
-- PRODUCTS --
--------------

insert into
	Products (
		Code,
		ProductName,
		ProductDescription,
		Price,
		BrandId,
		CategoryId
	)
values
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

go

------------
-- IMAGES --
------------

insert into
	Images (ImageUrl, ProductId)
values
	(
		'https://images.samsung.com/is/image/samsung/co-galaxy-s10-sm-g970-sm-g970fzyjcoo-frontcanaryyellow-thumb-149016542',
		'1'
	),
	(
		'https://i.blogs.es/9da288/moto-g7-/1366_2000.jpg',
		'2'
	);

go

---------------
-- COUNTRIES --
---------------

insert into
	Countries (CountryName)
values
	('Argentina'),
	('Estados Unidos'),
	('España'),
	('Brasil');

go

---------------
-- PROVINCES --
---------------

insert into
	Provinces (ProvinceName, CountryId)
values
	('Buenos Aires', '1'),
	('Córdoba', '1'),
	('Río Negro', '1');

go

------------
-- CITIES --
------------

insert into
	Cities (CityName, ZipCode, ProvinceId)
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

insert into
	Adresses (StreetName, StreetNumber, Flat, Details, CityId)
values
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

go

------------
-- PEOPLE --
------------

insert into
	People (
		PersonFirstName,
		PersonLastName,
		TaxCode,
		Phone,
		Email,
		Birth,
		AdressId
	)
values
	('Ana', 'Bertello', null, null, null, null, 1),
	('Facundo', 'Masino', null, null, null, null, 2),
	(
		'Maximiliano',
		'Malvicino',
		null,
		null,
		null,
		null,
		3
	);

go

-----------
-- ROLES --
-----------

insert into
	Roles (RoleName)
values
	('Admin'),
	('Cliente'),
	('Visitante');

go

-----------
-- USERS --
-----------

insert into
	Users (UserName, UserPassword, RoleId, PersonId)
values
	('Ani77aa', 'restrepo', '1', '1'),
	('FacuMasino', 'donpablo', '2', '2'),
	('mrmalvicino', 'elchili', '2', '3');

go

-------------------
-- ORDERSTATUSES --
-------------------

insert into
	OrderStatuses (OrderStatusName)
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

insert into
	Orders (OrderDate, AdressId, OrderStatusId, UserId)
values
	('2024-02-20', '4', '1', '2'),
	('2024-03-30', null, '1', '3');

go

-----------------------
-- ORDERS X PRODUCTS --
-----------------------

insert into
	OrdersXProducts (OrderId, ProductId, Amount)
values
	('1', '1', '1'),
	('1', '2', '2'),
	('2', '3', '3'),
	('2', '4', '4'),
	('2', '5', '5');

go