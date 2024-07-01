use ecommerce

go

--------------
-- PRODUCTS --
--------------

create or alter procedure SP_List_Products(
	@OnlyActive bit
)
as
begin
	if @OnlyActive = 1
	begin
		select ProductId, IsActive, Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId
		from Products where IsActive = 1
	end
	else 
	begin
		select ProductId, IsActive, Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId
		from Products
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