DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'Материнская плата Gigabyte B450M'
			   , 200 +(RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb1.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5),
			   (NEWID()
			   ,'Материнская плата Gigabyte B450M S2H (rev. 1.0)'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb2.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5),
			   (NEWID()
			   ,'Материнская плата ASUS Prime B450M-K'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb3.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5),
			   (NEWID()
			   ,'Материнская плата ASUS ROG Strix B450-F Gaming'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb4.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5),
			   (NEWID()
			   ,'Материнская плата MSI Z390-A Pro'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb5.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5),
			   (NEWID()
			   ,'Материнская плата MSI MPG Z390 Gaming Plus'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb6.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5),
			   (NEWID()
			   ,'Материнская плата Gigabyte B450M Gaming  '
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb7.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5),
			   (NEWID()
			   ,'Материнская плата MSI B450M-A Pro Max'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Motherboard/mb8.jpg'
			   , SYSDATETIME()
			   ,12
			   ,5)
        SET @number = @number - 1
    END;
	GO

DELETE FROM [WebApplication].[dbo].[Motherboard]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 5

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO [WebApplication].[dbo].[Motherboard]
           ([Id], [ChipSet], [FormFactor], [RamMemoryTypeId], [MemorySlotsNumber], [SocketTypeId])
		 VALUES
			(@productId,N'AMD B450', N'mATX',1, ROUND(1 + (RAND()* 3),2), 1)
	
	INSERT INTO [dbo].[MotherboardInterface]
	([MotherboardId], [InterfaceId],[Quantity])
     VALUES
           (@productId,24, 1),
		   (@productId,20,2),
		   (@productId,21, 1),
		   (@productId,22, 3),
		   (@productId,23, 1),
		   (@productId,10, 1),
		   (@productId,11, 3),
		   (@productId,12, 1),
		   (@productId,14, 2),
		   (@productId,15, 1),
		   (@productId,19, 1),
		   (@productId,18, 4),
		   (@productId,9, 2),
		   (@productId,7, 1),
		   (@productId,1, 1),
		   (@productId,3, 2),
		   (@productId,2, 2),
		   (@productId,5, 2)
         
	FETCH NEXT FROM Product_Cursor INTO @productId
END
