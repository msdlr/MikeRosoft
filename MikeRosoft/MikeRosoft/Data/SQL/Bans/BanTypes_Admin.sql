/* Ban types */

SET IDENTITY_INSERT [dbo].[BanTypes] ON
INSERT INTO [dbo].[BanTypes] ([TypeID], [TypeName], [DefaultDuration]) VALUES (1, N'Griefing', N'12:00:00')
SET IDENTITY_INSERT [dbo].[BanTypes] OFF

SET IDENTITY_INSERT [dbo].[BanTypes] ON
INSERT INTO [dbo].[BanTypes] ([TypeID], [TypeName], [DefaultDuration]) VALUES (2, N'Inappropiate comment', N'23:59:59')
SET IDENTITY_INSERT [dbo].[BanTypes] OFF


SET IDENTITY_INSERT [dbo].[BanTypes] ON
INSERT INTO [dbo].[BanTypes] ([TypeID], [TypeName], [DefaultDuration]) VALUES (3, N'Unpaid orders', N'23:59:59')
SET IDENTITY_INSERT [dbo].[BanTypes] OFF


SET IDENTITY_INSERT [dbo].[BanTypes] ON
INSERT INTO [dbo].[BanTypes] ([TypeID], [TypeName], [DefaultDuration]) VALUES (4, N'Fraudulent information', N'23:59:59')
SET IDENTITY_INSERT [dbo].[BanTypes] OFF


/* Admin */
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Name], [FirstSurname], [SecondSurname], [DNI], [contractStarting], [contractEnding], [Street], [City], [Province], [Country]) VALUES (N'79df0829-1bb5-4972-8cb6-9d23b9aa59aa', N'ms@uclm.es', N'MS@UCLM.ES', N'ms@uclm.es', N'MS@UCLM.ES', 1, N'AQAAAAEAACcQAAAAELZt4jiv4PPZF2NWQrU8BPvXjYEBcOo6F62I1KlgHZ5+lybw6MyZixxsrlLuqvuboQ==', N'U7GFJM6MLSTI3LVVZ2CEUJZWQQKGDPWH', N'2401b374-4654-4618-a028-571d49fec2fb', NULL, 0, 0, NULL, 1, 0, N'Admin', N'Miguel', N'Sanchez', N'De la Rosa', N'21345234U', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', NULL, NULL, NULL, NULL)