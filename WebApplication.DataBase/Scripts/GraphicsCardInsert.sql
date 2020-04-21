
INSERT INTO [dbo].[GraphicsCardMemoryType]
           ([MemoryType])
     VALUES
           (N'GDDR3'),
           (N'GDDR5'),
           (N'GDDR6'),
           (N'HBM2'),
           (N'HBM3')
GO

DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'Видеокарта NVidia GeForce GT210'
			   , 200 +(RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/VideoCards/vc1.jpg'
			   , SYSDATETIME()
			   ,10
			   ,1),
			   (NEWID()
			   ,'Видеокарта AMD GT 710'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/VideoCards/vc2.jpg'
			   , SYSDATETIME()
			   ,11
			   ,1),
			   (NEWID()
			   ,'Видеокарта ASUS GT'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/VideoCards/vc3.jpg'
			   , SYSDATETIME()
			   ,12
			   ,1),
			   (NEWID()
			   ,'Видеокарта Zotac GT 710'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/VideoCards/vc4.jpg'
			   , SYSDATETIME()
			   ,13
			   ,1),
			   (NEWID()
			   ,'Видеокарта Gainward R5 230 '
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/VideoCards/vc5.jpg'
			   , SYSDATETIME()
			   ,14
			   ,1),
			   (NEWID()
			   ,'Видеокарта AMD GeForce 210'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/VideoCards/vc6.jpg'
			   , SYSDATETIME()
			   ,11
			   ,1),
			   (NEWID()
			   ,'Видеокарта NVidia GeForce GT710'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/VideoCards/vc7.jpg'
			   , SYSDATETIME()
			   ,10
			   ,1)
        SET @number = @number - 5
    END;
	GO


DELETE FROM [WebApplication].[dbo].[GraphicsCard]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 1

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER,
@totalMemory INT = 2
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @totalMemory = @totalMemory * 2;
	INSERT INTO [WebApplication].[dbo].[GraphicsCard]
           ([Id], [GpuFrequency], [GpuTurboFrequency], [StreamProcessorsNumber], [VideoMemory], [MemoryFrequency], [MemoryBusWidth], [DirectXSupport],
		   [RecommendedPowerSupply], [FansNumber], [GraphicsCardMemoryTypeId], [InterfaceId])
		 VALUES
			(@productId, ROUND((500 + RAND()* 1500),2),ROUND((500 + RAND()* 2000),2), ROUND(500 + (RAND()* 2000),0), @totalMemory,ROUND((500 + RAND()* 3000),2),192,12,450,2,ROUND((1 + RAND() * 4),0),3)
	IF( @totalMemory >= 16)
		SET @totalMemory = 2
	FETCH NEXT FROM Product_Cursor INTO @productId
END
