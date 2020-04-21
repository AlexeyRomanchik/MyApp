DECLARE @number INT
SET @number = 5000;

WHILE @number > 0
    BEGIN
        INSERT INTO [WebApplication].[dbo].[Product]
           ([Id], [Name], [Price], [QuantityInStock], [ImageUrl], [DateAdded], [ManufacturerId], [CategoryId])
		 VALUES
			   (NEWID()
			   ,'ОБлок питания Aerocool'
			   , 200 +ROUND((RAND()* 1000),0)
			   , 200 +ROUND((RAND()* 1000),0)
			   ,'/productsImages/PowerSupply/pw1.jpg'
			   , SYSDATETIME()
			   ,15
			   ,6),
			   (NEWID()
			   ,'Блок питания Chieftec 600W'
			   ,  200 +ROUND((RAND()* 1000),0)
			   , 200 +ROUND((RAND()* 1000),0)
			   ,'/productsImages/PowerSupply/pw2.jpg'
			   , SYSDATETIME()
			   ,16
			   ,6),
			   (NEWID()
			   ,'Блок питания GeIL'
			   ,  200 +ROUND((RAND()* 1000),0)
			   , 200 +ROUND((RAND()* 1000),0)
			   ,'/productsImages/PowerSupply/pw3.jpg'
			   , SYSDATETIME()
			   ,17
			   ,6),
			   (NEWID()
			   ,'Блок питания Xilence RedWing 500W'
			   , 200 + (RAND(@number) * 1000)
			   , 200 +ROUND((RAND()* 1000),0)
			   ,'/productsImages/PowerSupply/pw4.jpg'
			   , SYSDATETIME()
			   ,18
			   ,6),
			   (NEWID()
			   ,'Блок питания be quiet! System Power 9 500W'
			   ,  200 +ROUND((RAND()* 1000),0)
			   , 200 +ROUND((RAND()* 1000),0)
			   ,'/productsImages/PowerSupply/pw5.jpg'
			   , SYSDATETIME()
			   ,19
			   ,6),
			   (NEWID()
			   ,'Блок питания AFOX 1000W (AFPS-1000A1)'
			   ,  200 +ROUND((RAND()* 1000),0)
			   , 200 +ROUND((RAND()* 1000),0)
			   ,'/productsImages/PowerSupply/pw6.jpg'
			   , SYSDATETIME()
			   ,20
			   ,6)
        SET @number = @number - 5
    END;
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
			(@productId, ROUND((500 + RAND()* 600),0), N'ATX12V 2.4', ROUND((500 + RAND()* 20),0), 12, 1.5, 86, 120, 1)
	FETCH NEXT FROM Product_Cursor INTO @productId
END

CLOSE Product_Cursor
DEALLOCATE Product_Cursor
GO
