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

----------------------------------------
-- COUNT PRODUCT CATEGORIES RELATIONS --
----------------------------------------

create or alter procedure SP_Count_PC_Relations(
	@CategoryId int
)
as
begin
	select count (CategoryId)
	from ProductCategories
	where CategoryId = @CategoryId;
end

go


----------------------------------------
-- COUNT PRODUCT CATEGORIES RELATIONS --
----------------------------------------

create or alter procedure SP_Count_B_Relations(
	@BrandId int
)
as
begin
	select count (BrandId)
	from Products
	where BrandId = @BrandId;
end

go