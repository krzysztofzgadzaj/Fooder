SET IDENTITY_INSERT [dbo].[Applications] ON
INSERT INTO [dbo].[Applications] ([Id], [Name], [Description]) VALUES (666666, 'Fooder', 'Application for reviewing pet feed');
SET IDENTITY_INSERT [dbo].[Applications] OFF
SET IDENTITY_INSERT [dbo].[Scopes] ON
INSERT INTO [dbo].[Scopes] ([Id], [Name], [ApplicationId]) VALUES (666666, 'Fooder', 666666);
SET IDENTITY_INSERT [dbo].[Scopes] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT INTO [dbo].[Roles] ([Id], [Name], [Code], [Description], [ApplicationId]) VALUES (666666, 'Administrator', 'Fooder.Admin', 'Administrator foodera', 666666);
INSERT INTO [dbo].[Roles] ([Id], [Name], [Code], [Description], [ApplicationId]) VALUES (666667, 'Ekspert', 'Fooder.Expert', 'Expert foodera', 666666);
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Permissions] ON
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Description], [ApplicationId]) VALUES (666666, 'Fooder_Add_Feed', 'To uprawnienie pozwala na dodawanie karmy', 666666);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Description], [ApplicationId]) VALUES (666667, 'Fooder_Add_Review', 'To uprawnienie pozwala na dodawanie recenzji', 666666);
INSERT INTO [dbo].[Permissions]([Id], [Code], [Description], [ApplicationId]) VALUES (666668, 'Fooder_Add_Brand', 'To uprawnienie pozwala na dodawanie brandu', 666666);
SET IDENTITY_INSERT [dbo].[Permissions] OFF
INSERT INTO [dbo].[RoleScopeAssignment] VALUES (666666, 666666);
INSERT INTO [dbo].[RoleScopeAssignment] VALUES (666667, 666666);
INSERT INTO [dbo].[PermissionRoleAssignment] VALUES (666666, 666666);
INSERT INTO [dbo].[PermissionRoleAssignment] VALUES (666667, 666666);
INSERT INTO [dbo].[PermissionRoleAssignment] VALUES (666667, 666667);
INSERT INTO [dbo].[PermissionRoleAssignment] VALUES (666668, 666666);