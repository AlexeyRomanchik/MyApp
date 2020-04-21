DELETE FROM [WebApplication].[dbo].[RamMemoryType]
GO

INSERT INTO [WebApplication].[dbo].[RamMemoryType]
           ([Type])
     VALUES
           ('DDR3'),
		   ('DDR2'),
		   ('DDR4'),
		   ('SIMM'),
		   ('DIMM')
GO

DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'Оперативная память CORSAIR' 
			   , ROUND((RAND()* 1000),0)
			   , ROUND((RAND()* 1000),0)
			   ,'/productsImages/Ram/ram1.jpg'
			   , SYSDATETIME()
			   ,1
			   ,2),
			   (NEWID()
			   ,'Оперативная память ADATA' 
			   , ROUND((RAND()* 1000),0)
			   , ROUND((RAND()* 1000),0)
			   ,'/productsImages/Ram/ram2.jpg'
			   , SYSDATETIME()
			   ,2
			   ,2),
			   (NEWID()
			   ,'Оперативная память CRUCIAL'
			   , ROUND((RAND()* 1000),0)
			   , ROUND((RAND()* 1000),0)
			   ,'/productsImages/Ram/ram3.jpg'
			   , SYSDATETIME()
			   ,3
			   ,2),
			   (NEWID()
			   ,'Оперативная память HYPERX' 
			   , ROUND((RAND()* 1000),0)
			   , ROUND((RAND()* 1000),0)
			   ,'/productsImages/Ram/ram4.jpg'
			   , SYSDATETIME()
			   ,4
			   ,2),
			   (NEWID()
			   ,'Оперативная память SAMSUNG' 
			   , ROUND((RAND()* 1000),0)
			   , ROUND((RAND()* 1000),0)
			   ,'/productsImages/Ram/ram5.jpg'
			   , SYSDATETIME()
			   ,5
			   ,2),
			   (NEWID()
			   ,'Оперативная память AMD' 
			   , ROUND((RAND()* 1000),0)
			   , ROUND((RAND()* 1000),0)
			   ,'/productsImages/Ram/ram6.jpg'
			   , SYSDATETIME()
			   ,6
			   ,2),
			   (NEWID()
			   ,'Оперативная память SKILL' 
			   , ROUND((RAND()* 1000),0)
			   , ROUND((RAND()* 1000),0)
			   ,'/productsImages/Ram/ram7.jpg'
			   , SYSDATETIME()
			   ,7
			   ,2)
        SET @number = @number - 1
    END;
GO


DELETE FROM [WebApplication].[dbo].[Ram]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 2

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER,
@totalMemory INT = 2
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @totalMemory = @totalMemory * 2;
	INSERT INTO [WebApplication].[dbo].[Ram]
           ([Id], [RamMemoryTypeId], [TotalMemory], [Frequency], [ContactsNumber], [Throughput], [SupplyVoltage])
		 VALUES
			(@productId, ROUND(1 + (RAND()* 4),0),@totalMemory, ROUND(500 + (RAND()* 3000),0), 224, ROUND(500 + (RAND()* 3000),0),Rand() * 3)
	IF( @totalMemory >= 32)
		SET @totalMemory = 2
	FETCH NEXT FROM Product_Cursor INTO @productId
END

CLOSE Product_Cursor
DEALLOCATE Product_Cursor
GO
    