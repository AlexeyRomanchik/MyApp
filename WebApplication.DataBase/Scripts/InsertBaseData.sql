
INSERT INTO [WebApplication].[dbo].[Category] ([Name])
VALUES
	( N'Видеокарты'),
	( N'Оперативная память'),
	( N'Процессоры'),
	( N'Жесткие диски'),
	( N'Материнские платы'),
	( N'Блоки питания')
GO

INSERT INTO [dbo].[Manufacturer]
           ([Name])
     VALUES
           (N'CORSAIR'),
		   (N'ADATA'),
		   (N'CRUCIAL'),
		   (N'HYPERX'),
		   (N'SAMSUNG'),
		   (N'AMD'),
		   (N'SKILL'),
		   (N'TRANSCEND'),
		   (N'KINGSTON'),
		   (N'NVidia'),
		   (N'Palit'),
		   (N'ASUS'),
		   (N'Zotac'),
		   (N'Gainward'),
		   (N'Super Flower'),
		   (N'Sea Sonic'),
		   (N'Chieftec'),
		   (N'High Power'),
		   (N'Corsair'),
		   (N'AeroCool'),
		   (N'Intel'),
		   (N'Qualcomm'),
		   (N'Apple'),
		   (N'TOSHIBA'),
		   (N'HITACHI'),
		   (N'SEAGATE'),
		   (N'Gigabyte'),
		   (N'ASRock'),
		   (N'BIOSTAR')
GO


INSERT INTO [dbo].[ManufacturerCategory]
           ([CategoryId], [ManufacturerId])
     VALUES
           (2, 1),
           (2, 2),
		   (2, 3),
		   (2, 4),
		   (2, 5),
		   (2, 6),
		   (2, 7),
		   (2, 8),
		   (2, 9),
		   (1, 10),
		   (1, 11),
		   (1, 12),
		   (1, 13),
		   (1, 14),
		   (6, 15),
		   (6, 16),
		   (6, 17),
		   (6, 18),
		   (6, 19),
		   (6, 20),
		   (3, 21),
		   (3, 22),
		   (3, 23),
		   (3, 6),
		   (3, 10),
		   (4, 5),
		   (4, 24),
		   (4, 25),
		   (4, 26),
		   (5, 27),
		   (5, 28),
		   (5, 29)
GO

INSERT INTO [dbo].[Interface]
           ([Name])
     VALUES
           (N'PCIe 4.0'),
		   (N'PCIe 5.0'),
		   (N'PCIe 6.0'),
		   (N'SATA-300'),
		   (N'SATA-600'),
		   (N'USB 2.0'),
		   (N'USB 3.0'),
		   (N'eSATA'),
		   (N'eSATA'),
		   (N'eSATA'),
		   (N'SATA 2.0'),
		   (N'SATA 3.0'),
		   (N'mSATA'),
		   (N'PS/2'),
		   (N'PS/2'),
		   (N'IEEE1284 (LPT)'),
		   (N'SAS'),
		   (N'DVI'),
		   (N'HDMI'),
		   (N'VGA (D-Sub)'),
		   (N'Wi-Fi '),
		   (N'Bluetooth'),
		   (N'DisplayPort'),
		   (N'Ethernet')
GO
