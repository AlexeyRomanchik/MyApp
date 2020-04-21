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
			   ,'Жесткий диск WD Caviar Blue 1TB'
			   , 200 +(RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Hdd/hdd1.jpg'
			   , SYSDATETIME()
			   ,24
			   ,4),
			   (NEWID()
			   ,'Жесткий диск Seagate Barracuda 2TB ST2000DM008'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Hdd/hdd2.jpg'
			   , SYSDATETIME()
			   ,25
			   ,4),
			   (NEWID()
			   ,'Жесткий диск Seagate BarraCuda 1TB'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Hdd/hdd3.jpg'
			   , SYSDATETIME()
			   ,24
			   ,4),
			   (NEWID()
			   ,'Жесткий диск WD Blue 500GB [WD5000AZLX]'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Hdd/hdd4.jpg'
			   , SYSDATETIME()
			   ,25
			   ,4),
			   (NEWID()
			   ,'Жесткий диск WD Blue 2TB (WD20EZRZ)'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Hdd/hdd5.jpg'
			   , SYSDATETIME()
			   ,24
			   ,4),
			   (NEWID()
			   ,'Жесткий диск WD Blue 1TB [WD10SPZX]'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Hdd/hdd6.jpg'
			   , SYSDATETIME()
			   ,25
			   ,4)
        SET @number = @number - 5
    END;
	GO


DELETE FROM [WebApplication].[dbo].[Hdd]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 4

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO [WebApplication].[dbo].[Hdd]
           ([Id], [Volume], [FormFactor], [SpindleSpeed], [Buffer], [SectorSize], [SequentialReadSpeed], [SequentialWriteSpeed],
		   [PowerConsumptionForReadWrite], [PowerConsumptionStandby], [NoiseReadingWriting], [NoiseStandby],[InterfaceId])
		 VALUES
			(@productId, 1000,N'2.5"', ROUND(2000 + (RAND()* 5000),0),32, 32,ROUND((32 + RAND()* 200),0), ROUND((32 + RAND()* 200),0) ,ROUND((3 + RAND() * 4),2) ,  ROUND((RAND() * 4),2),24,23,4)
	FETCH NEXT FROM Product_Cursor INTO @productId
END

