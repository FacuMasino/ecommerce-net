use ecommerce

go

-------------------
-- LIST PRODUCTS --
-------------------

create or alter procedure SP_List_Products (@OnlyActive bit) as
begin
	if @OnlyActive = 1 BEGIN
		select ProductId, Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId, Active
		from Products where Active = 1
	END
	ELSE 
	BEGIN
		select ProductId, Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId, Active
		from Products
	END
end
go

create or alter procedure SP_Delete_Product (@ProductId int) as
begin
	update products
	set Active = 0
	where ProductId = @ProductId
end
go

/* Test */

exec SP_List_Products 0

go