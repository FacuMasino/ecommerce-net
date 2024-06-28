use ecommerce

go

-------------------
-- LIST PRODUCTS --
-------------------

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

--------------------
-- DELETE PRODUCT --
--------------------

create or alter procedure SP_Delete_Product(
	@ProductId int
)
as
begin
	update products
	set Active = 0
	where ProductId = @ProductId;
end

go

------------------------------
-- COUNT CATEGORY RELATIONS --
------------------------------

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


---------------------------
-- COUNT BRAND RELATIONS --
---------------------------

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

-------------------------------
-- DELETE CATEGORY LOGICALLY --
-------------------------------

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

----------------------------
-- DELETE BRAND LOGICALLY --
----------------------------

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