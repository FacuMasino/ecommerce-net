use ecommerce

go

--------------
-- PRODUCTS --
--------------

create or alter procedure SP_List_Products(
	@OnlyActive bit,
	@OrderId int
)
as
begin
	if (@OnlyActive = 1 and @OrderId = 0)
	begin
		select P.ProductId, P.IsActive, P.Code, P.ProductName, P.ProductDescription, P.Price, P.Cost, P.Stock, P.BrandId
		from Products P
		where P.IsActive = 1
	end
	else if (@OnlyActive = 0 and @OrderId = 0)
	begin
		select P.ProductId, P.IsActive, P.Code, P.ProductName, P.ProductDescription, P.Price, P.Cost, P.Stock, P.BrandId
		from Products P
	end
	else if (@OnlyActive = 1 and @OrderId = 1)
	begin
		select P.ProductId, P.IsActive, P.Code, P.ProductName, P.ProductDescription, P.Price, P.Cost, P.Stock, P.BrandId, OP.Quantity
		from Products P
		inner join OrderProducts OP on OP.ProductId = P.ProductId
		where P.IsActive = 1 and OP.OrderId = @OrderId
	end
	--else if (@OnlyActive = 0 and @OrderId = 1)
	begin
		select P.ProductId, P.IsActive, P.Code, P.ProductName, P.ProductDescription, P.Price, P.Cost, P.Stock, P.BrandId, OP.Quantity
		from Products P
		inner join OrderProducts OP on OP.ProductId = P.ProductId
		where OP.OrderId = @OrderId
	end
end

go

create or alter procedure SP_List_Featured_Products
as
begin
	Select * from FeaturedProducts
end

go

create or alter procedure SP_Delete_Product_Logically(
	@ProductId int
)
as
begin
	update products
	set IsActive = 0
	where ProductId = @ProductId;
end

go

create or alter procedure SP_Add_Visit (
	@ProductId int
)
as
begin
	Update products
		set TotalVisits += 1
	where ProductId = @ProductId
end

go

----------------
-- CATEGORIES --
----------------

create or alter procedure SP_List_Categories(
	@OnlyActive bit,
	@ProductId int
)
as
begin
	if (@OnlyActive = 1 and @ProductId = 0)
	begin
		select C.CategoryId, C.IsActive, C.CategoryName
		from Categories C
		where C.IsActive = 1
		return
	end

	if (@OnlyActive = 0 and @ProductId = 0) 
	begin
		select C.CategoryId, C.IsActive, C.CategoryName
		from Categories C
		return
	end

	if (@OnlyActive = 1 and 0 < @ProductId)
	begin
		select C.CategoryId, C.IsActive, C.CategoryName
		from Categories C
		inner join ProductCategories PC on C.CategoryId = PC.CategoryId
		where C.IsActive = 1 and PC.ProductId = @ProductId
		return
	end

	-- if (@OnlyActive = 0 and 0 < @ProductId)
	begin
		select C.CategoryId, C.IsActive, C.CategoryName
		from Categories C
		inner join ProductCategories PC on C.CategoryId = PC.CategoryId
		where PC.ProductId = @ProductId
	end
end

go

create or alter procedure SP_Edit_Category(
	@CategoryId int,
	@IsActive bit,
	@CategoryName varchar(30)
)
as
begin
	update Categories set
	IsActive = @IsActive, CategoryName = @CategoryName
	where CategoryId = @CategoryId
end

go

create or alter procedure SP_Delete_Category_Logically(
	@CategoryId int
)
as
begin
	update Categories set
	IsActive = 0
	where CategoryId = @CategoryId;
end

go

create or alter procedure SP_Count_Category_Relations(
	@CategoryId int
)
as
begin
	select count (CategoryId)
	from ProductCategories
	where CategoryId = @CategoryId;
end

go

------------
-- BRANDS --
------------

create or alter procedure SP_List_Brands(
	@OnlyActive bit
)
as
begin
	if (@OnlyActive = 1)
	begin
		select B.BrandId, B.IsActive, B.BrandName
		from Brands B
		where B.IsActive = 1
		return
	end

	--if (@OnlyActive = 0) 
	begin
		select B.BrandId, B.IsActive, B.BrandName
		from Brands B
	end
end

go

create or alter procedure SP_Edit_Brand(
	@BrandId int,
	@IsActive bit,
	@BrandName varchar(30)
)
as
begin
	update Brands set
	IsActive = @IsActive, BrandName = @BrandName
	where BrandId = @BrandId
end

go

create or alter procedure SP_Delete_Brand_Logically(
	@BrandId int
)
as
begin
	update Brands
	set IsActive = 0
	where BrandId = @BrandId;
end

go

create or alter procedure SP_Count_Brand_Relations(
	@BrandId int
)
as
begin
	select count (BrandId)
	from Products
	where BrandId = @BrandId;
end

go

------------
-- ORDERS --
------------

create or alter procedure SP_List_Orders(
	@PersonId int
)
as
begin
	if (0 < @PersonId)
	begin
		select OrderId, CreationDate, DeliveryDate, DeliveryAddressId, OrderStatusId, PersonId, DistributionChannelId, PaymentTypeId
		from Orders
		where PersonId = @PersonId
	end
	else
	begin
		select OrderId, CreationDate, DeliveryDate, DeliveryAddressId, OrderStatusId, PersonId, DistributionChannelId, PaymentTypeId
		from Orders
	end
end

go

create or alter procedure SP_Read_Order(
	@OrderId int
)
as
begin
	select CreationDate, DeliveryDate, DeliveryAddressId, OrderStatusId, PersonId, DistributionChannelId, PaymentTypeId
	from Orders
	where OrderId = @OrderId
end

go

create or alter procedure SP_Add_Order_Confirmation(
	@DeliveryAddressId int,
	@OrderStatusId int,
	@PersonId int,
	@DistributionChannelId int,
	@PaymentTypeId int
)
as
begin
	insert into Orders
	(DeliveryAddressId, OrderStatusId, PersonId, DistributionChannelId, PaymentTypeId)
	output inserted.OrderId
	values
	(@DeliveryAddressId, @OrderStatusId, @PersonId, @DistributionChannelId, @PaymentTypeId)
end

go

create or alter procedure SP_Add_Product_To_Order(
	@OrderId int,
	@ProductId int,
	@Quantity int
)
as
begin
	insert into OrderProducts
	(OrderId, ProductId, Quantity)
	values
	(@OrderId, @ProductId, @Quantity)
end

go

create or alter procedure SP_List_Order_Statuses(
	@DistributionChannelId int
)
as
begin
	select S.OrderStatusId, S.OrderStatusName
	from OrderStatuses S
	inner join ChannelStatuses CS on CS.OrderStatusId = S.OrderStatusId
	inner join DistributionChannels C on C.DistributionChannelId = CS.DistributionChannelId
	where C. DistributionChannelId = @DistributionChannelId
	order by CS.OrderStatusIndex
end

go

-----------
-- USERS --
-----------

create or alter procedure SP_Add_User(
	@Username varchar(30) = NULL,
	@UserPassword varchar(30),
	@RoleId int,
    @PersonId int
)
as
begin
	insert into users(Username, UserPassword, RoleId, PersonId)
	output inserted.UserId
	values (@Username, @UserPassword, @RoleId, @PersonId)
end
go

create or alter procedure SP_Get_Person_Id(
	@Email varchar(30)
)
as
begin
	select PersonId
	from People
	where Email = @Email
end

go

create or alter procedure SP_Get_User_Id_By_Person(
	@PersonId int
)
as
begin
	select isNull(U.UserId, 0) as UserId
	from Users U
	right join People P on P.PersonId = U.PersonId
	where P.PersonId = @PersonId
end

go

create or alter procedure SP_Get_User_Id_By_Credentials(
	@Email varchar(30),
	@Password varchar (30)
)
as
begin
	select isNull(U.UserId, 0) as UserId
	from Users U
	right join People P on P.PersonId = U.PersonId
	where P.Email = @Email and U.UserPassword = @Password
end

go

create or alter procedure SP_Read_Person(
	@PersonId int
)
as
begin
	select IsActive, FirstName, LastName, TaxCode, Phone, Email, Birth, AddressId
	from People
	where PersonId = @PersonId
end

go

create or alter procedure SP_Read_User(
	@UserId int
)
as
begin
	select Username, UserPassword, RoleId, PersonId
	from Users
	where UserId = @UserId
end

go

create or alter procedure SP_Add_Person(
	@IsActive bit,
	@FirstName varchar(30),
	@LastName varchar(30),
	@TaxCode varchar(30) = NULL,
	@Phone varchar(30) = NULL,
	@Email varchar(30),
	@Birth datetime,
	@AddressId int
)
as
begin
	insert into People
	(IsActive, FirstName, LastName, TaxCode, Phone, Email, Birth, AddressId)
	output inserted.PersonId
	values
	(@IsActive, @FirstName, @LastName, @TaxCode, @Phone, @Email, @Birth, @AddressId)
end

go

create or alter procedure SP_Delete_Person_Logically(
	@PersonId int
)
as
begin
	update People
	set IsActive = 0
	where PersonId = @PersonId;
end

go

-----------
-- STATS --
-----------

create or alter procedure SP_Count_Finished_Orders
as
begin
	Select count(OrderId)
		from Orders
	where OrderStatusId = 5
end
go

create or alter procedure SP_Count_Active_Products 
as
begin
	Select count(ProductId)
		from Products 
	where IsActive = 1 
end
go

create or alter procedure SP_Count_Sold_Products 
as
begin
	Select IsNull(SUM(OP.Quantity),0)
		from OrderProducts OP
		inner join Orders O
		on O.OrderId = OP.OrderId
	where O.OrderStatusId = 5 
end
go

create or alter procedure SP_Count_Shipped_Products 
as
begin
	Select IsNull(SUM(OP.Quantity),0)
		from OrderProducts OP
		inner join Orders O
		on O.OrderId = OP.OrderId
	where 
		O.OrderStatusId = 5
		And O.DistributionChannelId in (1,4)
end
go