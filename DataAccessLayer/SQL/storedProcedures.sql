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
		select ProductId, Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId, Active
		from Products where Active = 1
	end
	else 
	begin
		select ProductId, Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId, Active
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
	set Active = 0
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
		select C.CategoryId, C.CategoryName
		from Categories C
		where C.Active = 1
		return
	end

	if (@OnlyActive = 0 and @ProductId = 0) 
	begin
		select C.CategoryId, C.CategoryName
		from Categories C
		return
	end

	if (@OnlyActive = 1 and 0 < @ProductId)
	begin
		select C.CategoryId, C.CategoryName
		from Categories C
		inner join ProductCategories PC on C.CategoryId = PC.CategoryId
		where C.Active = 1 and PC.ProductId = @ProductId
		return
	end
	-- if (@OnlyActive = 0 and 0 < @ProductId)
	begin
		select C.CategoryId, C.CategoryName
		from Categories C
		inner join ProductCategories PC on C.CategoryId = PC.CategoryId
		where PC.ProductId = @ProductId
	end
end

go

create or alter procedure SP_Delete_Category_Logically(
	@CategoryId int
)
as
begin
	update Categories
	set Active = 0
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

create or alter procedure SP_Delete_Brand_Logically(
	@BrandId int
)
as
begin
	update Brands
	set Active = 0
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