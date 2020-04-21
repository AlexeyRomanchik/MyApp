DELETE FROM [WebApplication].[dbo].[Category]
GO

INSERT INTO [WebApplication].[dbo].[Category] ([Name])
VALUES
	( N'Видеокарты'),
	( N'Оперативная память'),
	( N'Процессоры'),
	( N'Жесткие диски'),
	( N'Материнские платы'),
	( N'Блоки питания')

DELETE FROM [WebApplication].[dbo].[Manufacturer]
GO

INSERT INTO [WebApplication].[dbo].[Manufacturer]
           ([Name])
     VALUES
           ('ASUS'),
		   ('AMD'),
		   ('NVIDIA'),
		   ('MSI'),
		   ('EVGA'),
		   ('Zotac'),
		   ('Palit'),
		   ('PowerColor')
GO

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

DELETE FROM [WebApplication].[dbo].[Product]
GO

DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'Оперативная память XyperX HX42'
			   , 200 +(RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Ram/ram1.jpg'
			   , SYSDATETIME()
			   ,1
			   ,2),
			   (NEWID()
			   ,'Оперативная память XyperX HX42'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Ram/ram2.jpg'
			   , SYSDATETIME()
			   ,2
			   ,2),
			   (NEWID()
			   ,'Оперативная память XyperX HX42'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Ram/ram3.jpg'
			   , SYSDATETIME()
			   ,3
			   ,2),
			   (NEWID()
			   ,'Оперативная память XyperX HX42'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Ram/ram4.jpg'
			   , SYSDATETIME()
			   ,4
			   ,2),
			   (NEWID()
			   ,'Оперативная память XyperX HX42'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Ram/ram5.jpg'
			   , SYSDATETIME()
			   ,5
			   ,2),
			   (NEWID()
			   ,'Оперативная память XyperX HX42'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Ram/ram6.jpg'
			   , SYSDATETIME()
			   ,6
			   ,2),
			   (NEWID()
			   ,'Оперативная память XyperX HX42'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Ram/ram7.jpg'
			   , SYSDATETIME()
			   ,7
			   ,2)
        SET @number = @number - 5
    END;
	GO

DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'ОБлок питания Aerocool'
			   , 200 +(RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/PowerSupply/pw1.jpg'
			   , SYSDATETIME()
			   ,1
			   ,6),
			   (NEWID()
			   ,'Блок питания Chieftec 600W'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/PowerSupply/pw2.jpg'
			   , SYSDATETIME()
			   ,2
			   ,6),
			   (NEWID()
			   ,'Блок питания GeIL'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/PowerSupply/pw3.jpg'
			   , SYSDATETIME()
			   ,3
			   ,6),
			   (NEWID()
			   ,'Блок питания Xilence RedWing 500W'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/PowerSupply/pw4.jpg'
			   , SYSDATETIME()
			   ,4
			   ,6),
			   (NEWID()
			   ,'Блок питания be quiet! System Power 9 500W'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/PowerSupply/pw5.jpg'
			   , SYSDATETIME()
			   ,5
			   ,6),
			   (NEWID()
			   ,'Блок питания AFOX 1000W (AFPS-1000A1)'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/PowerSupply/pw6.jpg'
			   , SYSDATETIME()
			   ,6
			   ,6)
        SET @number = @number - 5
    END;
	GO

DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'Видеокарта AFOX GeForce GT210'
			   , 200 +ROUND((RAND()* 1000),0)
			   ,100 + ROUND((RAND()* 1000),0)
			   ,'/productsImages/VideoCards/vc1.jpg'
			   , SYSDATETIME()
			   ,1
			   ,1),
			   (NEWID()
			   ,'Видеокарта MSI GeForce GT 710'
			   , 200 + ROUND((RAND()* 1000),0)
			   ,100 + ROUND((RAND()* 1000),0)
			   ,'/productsImages/VideoCards/vc2.jpg'
			   , SYSDATETIME()
			   ,2
			   ,1),
			   (NEWID()
			   ,'Видеокарта Gigabyte GeForce GT'
			   , 200 + ROUND((RAND()* 1000),0)
			   ,100 + ROUND((RAND()* 1000),0)
			   ,'/productsImages/VideoCards/vc3.jpg'
			   , SYSDATETIME()
			   ,3
			   ,1),
			   (NEWID()
			   ,'Видеокарта Inno3D GeForce GT 710'
			   , 200 + ROUND((RAND()* 1000),0)
			   ,100 + ROUND((RAND()* 1000),0)
			   ,'/productsImages/VideoCards/vc4.jpg'
			   , SYSDATETIME()
			   ,4
			   ,1),
			   (NEWID()
			   ,'Видеокарта Sapphire R5 230 '
			   , 200 + ROUND((RAND()* 1000),0)
			   ,100 + ROUND((RAND()* 1000),0)
			   ,'/productsImages/VideoCards/vc5.jpg'
			   , SYSDATETIME()
			   ,5
			   ,1),
			   (NEWID()
			   ,'Видеокарта Gigabyte GeForce 210'
			   , 200 + ROUND((RAND()* 1000),0)
			   ,100 + ROUND((RAND()* 1000),0)
			   ,'/productsImages/VideoCards/vc6.jpg'
			   , SYSDATETIME()
			   ,6
			   ,1),
			   (NEWID()
			   ,'Видеокарта Colorful GeForce GT710'
			   , 200 + ROUND((RAND()* 1000),0)
			   ,100 + ROUND((RAND()* 1000),0)
			   ,'/productsImages/VideoCards/vc7.jpg'
			   , SYSDATETIME()
			   ,6
			   ,1)
        SET @number = @number - 5
    END;
	GO

DELETE FROM [WebApplication].[dbo].[Ram]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 2

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO [WebApplication].[dbo].[Ram]
           ([Id], [RamMemoryTypeId], [TotalMemory], [Frequency], [ContactsNumber], [Throughput], [SupplyVoltage])
		 VALUES
			(@productId, 1, 4, 1900, 224, 2500, 1.5)
	FETCH NEXT FROM Product_Cursor INTO @productId
END

CLOSE Product_Cursor
DEALLOCATE Product_Cursor
GO


DELETE FROM [WebApplication].[dbo].[PowerSupply]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 6

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO [WebApplication].[dbo].[PowerSupply]
           ([Id], [Power], [Standard], [NumberIndividualLines], [MaxLineCurrent], [PowerFactorCorrection], [ActiveEfficiency],[FanSize],[FansNumber])
		 VALUES
			(@productId, 600, N'ATX12V 2.4', 12, 12, 1.5, 86, 120, 1)
	FETCH NEXT FROM Product_Cursor INTO @productId
END

CLOSE Product_Cursor
DEALLOCATE Product_Cursor
GO


DELETE FROM [WebApplication].[dbo].[GraphicsCard]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 1

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO [WebApplication].[dbo].[GraphicsCard]
           ([Id], [GpuFrequency], [GpuTurboFrequency], [StreamProcessorsNumber], [VideoMemory], [MemoryFrequency], [MemoryBusWidth],[DirectXSupport],[RecommendedPowerSupply],[FansNumber])
		 VALUES
			(@productId, 1500, 1860, 1408, 6, 3500, 192, 12, 450, 2)
	FETCH NEXT FROM Product_Cursor INTO @productId
END

CLOSE Product_Cursor
DEALLOCATE Product_Cursor
GO