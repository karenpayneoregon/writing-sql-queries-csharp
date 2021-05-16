/*
 The following three variables would be parameters for your
 SqlClient Commands.
*/

-- suppress rows affected
SET NOCOUNT ON 


DECLARE @Id AS INT = 2;
DECLARE @OrderAmount AS INT = 2;
DECLARE @UpdateInStock AS INT = 0;

SELECT UnitsInStock FROM dbo.Products WHERE ProductID = 2
SELECT COUNT(ProductID) FROM dbo.Products WHERE ProductID = 2
--UPDATE dbo.Products SET UnitsInStock = 20 WHERE ProductID = 2;


BEGIN TRANSACTION
	DECLARE @InStock AS INT = (SELECT UnitsInStock FROM dbo.Products WHERE ProductID = @Id);

	PRINT 'Current stock ' + CAST(@InStock AS VARCHAR(5));

	--- determine if the order can be placed
	IF @InStock > @OrderAmount
		BEGIN	
			SET @UpdateInStock = @InStock - @OrderAmount;

			PRINT 'Updated amount ' + CAST(@UpdateInStock AS VARCHAR(5));

			UPDATE dbo.Products SET UnitsInStock = @UpdateInStock WHERE ProductID = @Id;

			SET @InStock =  (SELECT  UnitsInStock FROM dbo.Products WHERE ProductID = @Id);

			PRINT 'Current stock after update ' + CAST(@InStock AS VARCHAR(5));

		END
	ELSE PRINT 'Not enough stock to place order!!!'


ROLLBACK TRANSACTION

SET @InStock = (SELECT  UnitsInStock FROM dbo.Products WHERE ProductID = @Id);
PRINT 'Current stock after operations ' + CAST(@InStock AS VARCHAR(5));

