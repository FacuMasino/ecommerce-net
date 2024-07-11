use ecommerce
go

-----------
-- STATS --
-----------

create or alter view VW_TopSellingProducts
as
select top 100
    p.ProductId,
    p.ProductName,
    p.Code,
    p.Price,
	p.IsActive,
	p.BrandId,
    IsNull(SUM(op.Quantity),0) AS TotalQuantitySold
FROM 
    Products p
    INNER JOIN OrderProducts op ON p.ProductId = op.ProductId
    INNER JOIN Orders o ON op.OrderId = o.OrderId
WHERE 
    o.OrderStatusId = 5 -- ordenes completadas
GROUP BY 
    p.ProductId, p.ProductName, p.Code, p.Price, p.BrandId, p.IsActive
ORDER BY 
    TotalQuantitySold DESC;

go

create or alter view VW_TopVisitedProducts
as
select top 100
	*
from Products
Where
	IsActive = 1
And
	TotalVisits > 0
Order By TotalVisits DESC

go