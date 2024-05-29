use ecommerce
go

-------------------
-- LIST PRODUCTS --
-------------------

create or alter procedure SP_List_Products as
begin
	select ProductId, Code, ProductName, ProductDescription, Price, BrandId, CategoryId
	from Products
end
go

/* Test */

exec SP_List_Products