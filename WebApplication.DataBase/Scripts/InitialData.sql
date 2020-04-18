﻿DELETE FROM [WebApplication].[dbo].[Category]
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

DELETE FROM [WebApplication].[dbo].[Ram]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product]

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
