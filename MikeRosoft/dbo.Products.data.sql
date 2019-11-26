SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([id], [title], [description], [precio], [stock], [rate], [Brandid], [brand_string]) VALUES (4, N'Memoria RAM', N'8GB', 130, 3, 8, 1, N'Kingston')
INSERT INTO [dbo].[Products] ([id], [title], [description], [precio], [stock], [rate], [Brandid], [brand_string]) VALUES (5, N'Memoria RAM', N'16GB', 180, 0, 9, 1, N'Kingston')
INSERT INTO [dbo].[Products] ([id], [title], [description], [precio], [stock], [rate], [Brandid], [brand_string]) VALUES (6, N'SSD', N'250GB', 150, 8, 7, 2, N'Samsung')
SET IDENTITY_INSERT [dbo].[Products] OFF
