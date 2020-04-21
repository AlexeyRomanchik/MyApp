INSERT INTO [dbo].[SocketType]
           ([Type])
     VALUES
           (N'Inter'),
		   (N'Amd')
GO


DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'Процессор AMD A6-9500'
			   , 200 +(RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Cpu/cpu1.jpg'
			   , SYSDATETIME()
			   ,6
			   ,3),
			   (NEWID()
			   ,'Процессор NVidia Athlon X4 950'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Cpu/cpu2.jpg'
			   , SYSDATETIME()
			   ,10
			   ,3),
			   (NEWID()
			   ,'Процессор Intel 1200'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Cpu/cpu3.jpg'
			   , SYSDATETIME()
			   ,21
			   ,3),
			   (NEWID()
			   ,'Процессор Qualcomm Ryzen 5 1600'
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Cpu/cpu4.jpg'
			   , SYSDATETIME()
			   ,22
			   ,3),
			   (NEWID()
			   ,'Процессор Apple 3 2200G '
			   , 200 + (RAND(@number) * 1000)
			   ,100 + (RAND(@number) * 1000)
			   ,'/productsImages/Cpu/cpu5.jpg'
			   , SYSDATETIME()
			   ,23
			   ,3)
        SET @number = @number - 1
    END;
	GO


DELETE FROM [WebApplication].[dbo].[Cpu]
GO

DECLARE Product_Cursor CURSOR
FOR
  SELECT [Id] FROM [WebApplication].[dbo].[Product] WHERE [CategoryId] = 3

OPEN Product_Cursor 
DECLARE  @productId UNIQUEIDENTIFIER,
@totalMemory INT = 2
FETCH NEXT FROM Product_Cursor INTO @productId
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @totalMemory = @totalMemory * 2;
	INSERT INTO [WebApplication].[dbo].[Cpu]
           ([Id], [Socket], [NumberCores], [MaximumNumberThreads], [BaseClock], [MaximumFrequency], [L2Cache], [L3Cache],
		   [MemorySupport], [NumberMemoryChannels], [MaxMemoryFrequency], [Tdp],[SocketTypeId])
		 VALUES
			(@productId,N'AM4', ROUND((1 + RAND()* 15),0),ROUND((1 + RAND()* 15),0), ROUND(500 + (RAND()* 4),2), ROUND(500 + (RAND()* 4),2),3,16,N'DD3',2,ROUND((1 + RAND()* 15),0),ROUND((20 + RAND()* 65),0),ROUND((1 + RAND()* 1),0))
	IF( @totalMemory >= 16)
		SET @totalMemory = 2
	FETCH NEXT FROM Product_Cursor INTO @productId
END

