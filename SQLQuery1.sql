DECLARE @CategoryIdentifier AS INT = 1
SELECT ProductID
      ,ProductName
      ,SupplierID
      ,CategoryID
      ,QuantityPerUnit
      ,UnitPrice
      ,UnitsInStock
      ,UnitsOnOrder
      ,ReorderLevel
      ,Discontinued
      ,Discontinued
  FROM NorthWindAzure1.dbo.Products
