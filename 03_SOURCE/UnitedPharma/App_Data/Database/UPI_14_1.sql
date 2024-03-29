USE [UPI]
GO
/****** Object:  Table [dbo].[SupervisorPosition]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupervisorPosition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PositionName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SupervisorPosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SupervisorPosition] ON
INSERT [dbo].[SupervisorPosition] ([Id], [PositionName]) VALUES (1, N'CEO')
INSERT [dbo].[SupervisorPosition] ([Id], [PositionName]) VALUES (2, N'General Manager')
INSERT [dbo].[SupervisorPosition] ([Id], [PositionName]) VALUES (3, N'Sales Manager')
SET IDENTITY_INSERT [dbo].[SupervisorPosition] OFF
/****** Object:  Table [dbo].[SmsType]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Syntax] [nvarchar](max) NULL,
 CONSTRAINT [PK_SmsType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SmsType] ON
INSERT [dbo].[SmsType] ([Id], [Name], [Syntax]) VALUES (1, N'SMS', N'SMS [SMS_Code] [Content]')
INSERT [dbo].[SmsType] ([Id], [Name], [Syntax]) VALUES (2, N'REQ', N'REQ [Sms_Code] [Content]')
INSERT [dbo].[SmsType] ([Id], [Name], [Syntax]) VALUES (3, N'FED', N'FED [Sms_code] [Content]')
SET IDENTITY_INSERT [dbo].[SmsType] OFF
/****** Object:  Table [dbo].[Section]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Section](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Section] ON
INSERT [dbo].[Section] ([Id], [SectionName]) VALUES (1, N'South Section')
INSERT [dbo].[Section] ([Id], [SectionName]) VALUES (2, N'North Section')
SET IDENTITY_INSERT [dbo].[Section] OFF
/****** Object:  Table [dbo].[SchedulePromotion]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SchedulePromotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](255) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[SMSContent] [ntext] NULL,
	[WebContent] [ntext] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AdministratorId] [int] NULL,
	[IsApprove] [bit] NULL,
	[PhoneNumbers] [nvarchar](max) NULL,
 CONSTRAINT [PK_SchedulePromotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SchedulePromotion] ON
INSERT [dbo].[SchedulePromotion] ([Id], [UpiCode], [Title], [SMSContent], [WebContent], [StartDate], [EndDate], [AdministratorId], [IsApprove], [PhoneNumbers]) VALUES (6, N'test', N'test title', N'sms content', N'web content', CAST(0x00009FCB00000000 AS DateTime), CAST(0x00009FE900000000 AS DateTime), 1, 0, N'0962345636,01223456783')
INSERT [dbo].[SchedulePromotion] ([Id], [UpiCode], [Title], [SMSContent], [WebContent], [StartDate], [EndDate], [AdministratorId], [IsApprove], [PhoneNumbers]) VALUES (7, N'222', N'nguoicungkho', N'Virtual scrolling for RadGrid', N'Virtual scrolling for RadGrid can be attained by ajaxifying the control via RadAjaxManager', CAST(0x00009FF800000000 AS DateTime), CAST(0x0000A01500000000 AS DateTime), 7, 1, N'0962345636')
SET IDENTITY_INSERT [dbo].[SchedulePromotion] OFF
/****** Object:  Table [dbo].[Groups]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](max) NOT NULL,
	[GroupName] [nvarchar](max) NOT NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON
INSERT [dbo].[Groups] ([Id], [UpiCode], [GroupName], [Description]) VALUES (1, N'GRP011', N'Group 11', N'This is group 1')
INSERT [dbo].[Groups] ([Id], [UpiCode], [GroupName], [Description]) VALUES (2, N'GRP02', N'Group 2', N'This is group 2')
SET IDENTITY_INSERT [dbo].[Groups] OFF
/****** Object:  Table [dbo].[Function]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Function](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FunctionName] [nvarchar](max) NOT NULL,
	[ParentFunctionId] [int] NULL,
	[Action] [nvarchar](max) NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Function] ON
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (1, N'Permission', NULL, NULL)
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (2, N'Promotion&SMS', NULL, NULL)
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (3, N'Geographic', NULL, NULL)
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (4, N'Supervisor Position', 1, N'SupervisorPositionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (5, N'Customer Supervisor', 1, N'CustomerSupervisorManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (6, N'Customer Type', 1, N'CustomerTypeManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (7, N'Channels', 1, N'ChannelManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (8, N'Customers', 1, N'CustomerManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (9, N'Customer Log', 1, N'CustomerLogManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (10, N'Roles', 1, N'RoleManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (11, N'Dashboard ', 1, N'DashboardManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (12, N'Salesmen', 1, N'SalesmanManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (13, N'Administrator', 1, N'AdministratorManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (14, N'Functions', 1, N'FunctionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (15, N'Assign Function', 1, N'AssignFunctionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (16, N'Promotion', 2, N'PromotionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (17, N'SMS Type', 2, N'SmsTypeManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (18, N'Sms Quota', 2, N'SmsQuota.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (19, N'Groups', 3, N'GroupManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (20, N'Regions', 3, N'RegionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (21, N'Areas', 3, N'AreaManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (22, N'Locals', 3, N'LocalManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (23, N'Sections', 3, N'SectionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (24, N'Provinces', 3, N'ProvinceManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (25, N'Districts', 3, N'DistrictManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (26, N'Salesmen - Group', 1, N'SalesmenGroupManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (27, N'Salesmen - Region', 1, N'SalesmenRegionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (28, N'Salesmen - Area', 1, N'SalesmenAreaManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (29, N'Salesmen - Local', 1, N'SalesmenLocalManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (30, N'Schedule Promotion', 2, N'SchedulePromotionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (31, N'Allow Approve', 1, N'AllowApprove.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (32, N'Detail Schedule Promotion', NULL, N'DetailSchedulePromotionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (33, N'Customer detail', NULL, N'CustomerDetail.aspx')
SET IDENTITY_INSERT [dbo].[Function] OFF
/****** Object:  Table [dbo].[Channel]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Channel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](max) NOT NULL,
	[ChannelName] [nvarchar](255) NOT NULL,
	[ParentChannelId] [int] NULL,
 CONSTRAINT [PK_Channel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Channel] ON
INSERT [dbo].[Channel] ([Id], [UpiCode], [ChannelName], [ParentChannelId]) VALUES (1, N'CHN01', N'Channel 1', NULL)
INSERT [dbo].[Channel] ([Id], [UpiCode], [ChannelName], [ParentChannelId]) VALUES (2, N'CHN02', N'Channel 2', NULL)
INSERT [dbo].[Channel] ([Id], [UpiCode], [ChannelName], [ParentChannelId]) VALUES (3, N'CHN03', N'Channel 3', NULL)
SET IDENTITY_INSERT [dbo].[Channel] OFF
/****** Object:  Table [dbo].[Dashboard]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dashboard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Content] [ntext] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[ReceiverPhoneNumber] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dashborad] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Dashboard] ON
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (1, N'Number 1', N'This is Number 1', CAST(0x00009FBB00713FA7 AS DateTime), CAST(0x00009FBB00713FA7 AS DateTime), N'0914343432', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (2, N'Number 2', N'aaaa', CAST(0x00009FBB00714D49 AS DateTime), CAST(0x00009FBB00714D49 AS DateTime), N'0914343432', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (3, N'For Ronaldo', N'This is inform from Admin4b', CAST(0x00009FBB0071D33D AS DateTime), CAST(0x00009FBB007206A1 AS DateTime), N'0909432575', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (4, N'', N'', CAST(0x00009FD80121EE69 AS DateTime), CAST(0x00009FD80121EE69 AS DateTime), N'01224356421', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (5, N'', N'', CAST(0x00009FD80121EEA9 AS DateTime), CAST(0x00009FD80121EEA9 AS DateTime), N'0914343432', 1)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (6, N'', N'', CAST(0x00009FD80121EEAB AS DateTime), CAST(0x00009FD80121EEAB AS DateTime), N'0987435325', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (7, N'', N'', CAST(0x00009FD80121EEAB AS DateTime), CAST(0x00009FD80121EEAB AS DateTime), N'0909432575', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (8, N'', N'', CAST(0x00009FD80121EEAD AS DateTime), CAST(0x00009FD80121EEAD AS DateTime), N'0987542764', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (9, N'', N'', CAST(0x00009FD80121EEAE AS DateTime), CAST(0x00009FD80121EEAE AS DateTime), N'0951234567', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (10, N'', N'', CAST(0x00009FD80121EEAE AS DateTime), CAST(0x00009FD80121EEAE AS DateTime), N'0967348328', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (11, N'', N'', CAST(0x00009FD80121EEAF AS DateTime), CAST(0x00009FD80121EEAF AS DateTime), N'0483928764', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (12, N'', N'', CAST(0x00009FD80121EEB3 AS DateTime), CAST(0x00009FD80121EEB3 AS DateTime), N'0907325465', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (13, N'', N'', CAST(0x00009FD80121EEB4 AS DateTime), CAST(0x00009FD80121EEB4 AS DateTime), N'0912345324', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (14, N'', N'', CAST(0x00009FD80121EEB5 AS DateTime), CAST(0x00009FD80121EEB5 AS DateTime), N'0986473843', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (15, N'test', N'test.....', CAST(0x00009FD80134D534 AS DateTime), CAST(0x00009FD80134D535 AS DateTime), N'01224356421', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (16, N'test', N'test.....', CAST(0x00009FD80134D53A AS DateTime), CAST(0x00009FD80134D53A AS DateTime), N'0909432575', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (17, N'test', N'test.....', CAST(0x00009FD80134D53B AS DateTime), CAST(0x00009FD80134D53B AS DateTime), N'0987435325', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (18, N'test', N'test.....', CAST(0x00009FD80134D53B AS DateTime), CAST(0x00009FD80134D53B AS DateTime), N'0987542764', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (19, N'test', N'test.....', CAST(0x00009FD80134D53C AS DateTime), CAST(0x00009FD80134D53C AS DateTime), N'0907325465', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (20, N'test', N'test.....', CAST(0x00009FD80134D53C AS DateTime), CAST(0x00009FD80134D53C AS DateTime), N'0912345324', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (21, N'test', N'test.....', CAST(0x00009FD80134D541 AS DateTime), CAST(0x00009FD80134D541 AS DateTime), N'0909313336', 0)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [ReceiverPhoneNumber], [IsDeleted]) VALUES (22, N'test', N'test.....', CAST(0x00009FD80134D542 AS DateTime), CAST(0x00009FD80134D542 AS DateTime), N'0909838826', 0)
SET IDENTITY_INSERT [dbo].[Dashboard] OFF
/****** Object:  Table [dbo].[CustomerType]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](max) NOT NULL,
	[TypeName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerType] ON
INSERT [dbo].[CustomerType] ([Id], [UpiCode], [TypeName]) VALUES (1, N'CT01', N'Pharmacy')
INSERT [dbo].[CustomerType] ([Id], [UpiCode], [TypeName]) VALUES (2, N'CT02', N'Hospital')
INSERT [dbo].[CustomerType] ([Id], [UpiCode], [TypeName]) VALUES (3, N'CT03', N'Company')
SET IDENTITY_INSERT [dbo].[CustomerType] OFF
/****** Object:  Table [dbo].[Administrator]    Script Date: 01/14/2012 19:13:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Administrator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](max) NOT NULL,
	[Fullname] [nvarchar](max) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[AllowApprove] [bit] NULL,
 CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Administrator] ON
INSERT [dbo].[Administrator] ([Id], [UpiCode], [Fullname], [Phone], [Password], [AllowApprove]) VALUES (1, N'ADM01', N'Administrator', N'0909313336', N'123', 1)
INSERT [dbo].[Administrator] ([Id], [UpiCode], [Fullname], [Phone], [Password], [AllowApprove]) VALUES (5, N'ADM02', N'Nguyễn Thanh Sơn', N'0909838826', N'123', 0)
INSERT [dbo].[Administrator] ([Id], [UpiCode], [Fullname], [Phone], [Password], [AllowApprove]) VALUES (7, N'ADM03', N'Dương Đăng Quốc', N'0905253311', N'123', 0)
INSERT [dbo].[Administrator] ([Id], [UpiCode], [Fullname], [Phone], [Password], [AllowApprove]) VALUES (8, N'ADM04', N'Lê Hoàng Bảo Khánh', N'0903356422', N'123', 0)
SET IDENTITY_INSERT [dbo].[Administrator] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (1, N'POS')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (2, N'POC')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (3, N'TDS')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (4, N'ROM')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (5, N'SUP')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (6, N'REP')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Region]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Region](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](255) NOT NULL,
	[RegionName] [nvarchar](max) NOT NULL,
	[Description] [ntext] NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Region] ON
INSERT [dbo].[Region] ([Id], [UpiCode], [RegionName], [Description], [GroupId]) VALUES (1, N'REG01', N'Region 1', N'This is region 1', 1)
INSERT [dbo].[Region] ([Id], [UpiCode], [RegionName], [Description], [GroupId]) VALUES (2, N'REG02', N'Region 2', N'This is region 2', 1)
INSERT [dbo].[Region] ([Id], [UpiCode], [RegionName], [Description], [GroupId]) VALUES (3, N'REG03', N'Region 3', N'This is region 3', 2)
SET IDENTITY_INSERT [dbo].[Region] OFF
/****** Object:  Table [dbo].[Province]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceName] [nvarchar](255) NOT NULL,
	[SectionId] [int] NOT NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Province] ON
INSERT [dbo].[Province] ([Id], [ProvinceName], [SectionId]) VALUES (1, N'TP HCM', 1)
INSERT [dbo].[Province] ([Id], [ProvinceName], [SectionId]) VALUES (2, N'Dong Nai', 1)
INSERT [dbo].[Province] ([Id], [ProvinceName], [SectionId]) VALUES (3, N'Binh Duong', 1)
INSERT [dbo].[Province] ([Id], [ProvinceName], [SectionId]) VALUES (4, N'Ha Noi', 2)
INSERT [dbo].[Province] ([Id], [ProvinceName], [SectionId]) VALUES (5, N'Hai Duong', 2)
INSERT [dbo].[Province] ([Id], [ProvinceName], [SectionId]) VALUES (6, N'Hai Phong', 2)
SET IDENTITY_INSERT [dbo].[Province] OFF
/****** Object:  Table [dbo].[Promotion]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](255) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Content] [ntext] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AdministratorId] [int] NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Promotion] ON
INSERT [dbo].[Promotion] ([Id], [UpiCode], [Title], [Content], [StartDate], [EndDate], [AdministratorId]) VALUES (1, N'PRM01', N'Promotion December 2011', N'Khuyen mai giam gia thuoc', CAST(0x00009FAC00000000 AS DateTime), CAST(0x00009FC900000000 AS DateTime), 1)
INSERT [dbo].[Promotion] ([Id], [UpiCode], [Title], [Content], [StartDate], [EndDate], [AdministratorId]) VALUES (2, N'PRM02', N'Promotion January 2012', N'This is promotion for january 2012', CAST(0x00009FCB00000000 AS DateTime), CAST(0x00009FE900000000 AS DateTime), 1)
INSERT [dbo].[Promotion] ([Id], [UpiCode], [Title], [Content], [StartDate], [EndDate], [AdministratorId]) VALUES (3, N'PRM03', N'Promotion February 2012', NULL, CAST(0x00009FEA00000000 AS DateTime), CAST(0x0000A00600000000 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
/****** Object:  Table [dbo].[AssignFunction]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignFunction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FunctionId] [int] NOT NULL,
	[AdministratorId] [int] NULL,
 CONSTRAINT [PK_AssignFunction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AssignFunction] ON
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (1, 1, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (2, 2, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (3, 3, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (4, 4, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (5, 5, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (6, 6, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (7, 7, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (8, 8, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (9, 9, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (10, 10, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (11, 11, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (12, 12, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (13, 13, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (14, 14, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (15, 15, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (16, 16, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (17, 17, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (18, 18, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (19, 19, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (20, 20, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (21, 21, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (22, 22, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (23, 23, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (24, 24, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (25, 25, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (26, 1, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (27, 2, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (28, 3, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (29, 4, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (30, 5, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (31, 6, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (32, 7, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (33, 10, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (34, 16, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (35, 17, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (36, 18, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (37, 19, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (38, 20, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (39, 21, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (40, 22, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (41, 23, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (42, 24, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (43, 25, NULL)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (44, 26, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (45, 27, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (46, 28, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (47, 29, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (48, 30, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (49, 31, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (50, 32, 1)
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (51, 33, 1)
SET IDENTITY_INSERT [dbo].[AssignFunction] OFF
/****** Object:  Table [dbo].[Salesmen]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Salesmen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[RoleId] [int] NULL,
	[SmsQuota] [int] NULL,
	[SmsUsed] [int] NULL,
	[ExpiredDate] [datetime] NULL,
 CONSTRAINT [PK_Salesmen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Salesmen] ON
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (1, N'SAL01', N'David Beckham', N'0914343432', 1, 50, 0, CAST(0x00009F9700000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (2, N'SAL02', N'Luis Nani', N'0987435325', 2, 50, 0, CAST(0x00009F9700000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (3, N'SAL03', N'Cris Ronaldo', N'0909432575', 3, 50, 0, CAST(0x00009F9700000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (4, N'SAL04', N'Ronaldinho', N'01224356421', 1, 50, 0, CAST(0x00009F9700000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (6, N'SAL05', N'Alex Ferguson', N'0987542764', 4, 50, 0, CAST(0x00009F9700000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (7, N'SAL06', N'Jose Mourinho', N'0907325465', 5, 50, 0, CAST(0x00009F9700000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (8, N'SAL07', N'Didier Drogba', N'0912345324', 6, 50, 0, CAST(0x00009F9700000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (9, N'SAL08', N'Luis Figo', N'0951234567', 4, 50, NULL, CAST(0x00009FC900000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (10, N'SAL09', N'Zinedin Zindane', N'0967348328', 4, 60, NULL, CAST(0x00009FC800000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (11, N'SAL010', N'Park Ji Sung', N'0483928764', 5, 50, NULL, CAST(0x00009FC900000000 AS DateTime))
INSERT [dbo].[Salesmen] ([Id], [UpiCode], [FullName], [Phone], [RoleId], [SmsQuota], [SmsUsed], [ExpiredDate]) VALUES (12, N'SAL011', N'Valencia', N'0986473843', 6, 120, NULL, CAST(0x00009FCB00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Salesmen] OFF
/****** Object:  Table [dbo].[SmsObj]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SmsObj](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SMSCode] [varchar](100) NULL,
	[SenderNumber] [varchar](50) NULL,
	[ReceiverNumber] [ntext] NULL,
	[Date] [datetime] NULL,
	[Subject] [nvarchar](max) NULL,
	[Content] [ntext] NULL,
	[IsSendSuccess] [bit] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[SmsTypeId] [int] NULL,
	[PromotionId] [int] NULL,
	[SenderType] [int] NULL,
	[ReceiverType] [int] NULL,
	[ParentSmsId] [int] NULL,
 CONSTRAINT [PK_SmsObj] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SmsObj] ON
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (40, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0909838826', CAST(0x00009FC5016E9817 AS DateTime), N'Test', N'txtPhoneNumber.Text', 1, 1, 0, 1, 1, 0, 0, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (41, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909838826', N'0909313336', CAST(0x00009FC5016EDCD3 AS DateTime), N'RE: Test', N'gdr grdg sdg sdg sdg sd gsg sd.', 1, 1, 0, 1, 1, 0, 0, 40)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (42, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0909838826', CAST(0x00009FC5016F97E3 AS DateTime), N'RE: RE: Test', N'fgshf idsh ifu hdsuiof hosd fhod.', 0, 1, 0, 1, 1, 0, 0, 41)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (43, N'e315b471-854c-43df-a082-a8b05dd16121', N'0909313336', N'0909838826', CAST(0x00009FC601264B47 AS DateTime), N'No subject', N'Toi di hoc. Toi di vejgljdlgj dlfj glfdj g;l jfd;g ;lfd g;df g;lfd lg;j fdl; g;lfd gjl;fd gjlfd jgl; fdj;g f;dl glj;fdj g;df jg;lj fdl;g jlfd;j gl;fdj', 0, 1, 0, 1, 1, 0, 0, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (44, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0905253311', CAST(0x00009FC700DF46E9 AS DateTime), N'FW: RE: Test of forward', N'Doc di ku : g sdg sdg sdg sd gsg sd.', 1, 1, 0, 1, 1, 0, 0, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (47, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0909838826', CAST(0x00009FD800A77D04 AS DateTime), N'RE : Test', N'sada', 1, 1, 0, 1, 1, 0, 0, 41)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (48, N'1/14/2012 12:00:00 AM12012101132', N'0909313336', N'01223456783', CAST(0x00009FD800A7F759 AS DateTime), N'sadsadas', N'dadzXZZ', 1, 1, 0, 1, 1, 2, 2, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (49, N'1/14/2012 12:00:00 AM12012101132', N'0909313336', N'0909313336', CAST(0x00009FD800A813CB AS DateTime), N'RE : sadsadas', N'DASDAASCZ VX', 1, 1, 0, 1, 1, 0, 2, 48)
SET IDENTITY_INSERT [dbo].[SmsObj] OFF
/****** Object:  Table [dbo].[SalesRegion]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesRegion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesmenId] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
 CONSTRAINT [PK_SalesRegion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SalesRegion] ON
INSERT [dbo].[SalesRegion] ([Id], [SalesmenId], [RegionId]) VALUES (1, 6, 1)
INSERT [dbo].[SalesRegion] ([Id], [SalesmenId], [RegionId]) VALUES (2, 6, 2)
INSERT [dbo].[SalesRegion] ([Id], [SalesmenId], [RegionId]) VALUES (3, 10, 2)
SET IDENTITY_INSERT [dbo].[SalesRegion] OFF
/****** Object:  Table [dbo].[District]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[District](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DistrictName] [nvarchar](max) NOT NULL,
	[ProvinceId] [int] NOT NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[District] ON
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (1, N'District 1', 1)
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (2, N'District 2', 1)
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (3, N'District 3', 2)
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (4, N'District 4', 3)
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (5, N'AAA', 4)
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (6, N'BBB', 5)
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (7, N'CCC', 6)
INSERT [dbo].[District] ([Id], [DistrictName], [ProvinceId]) VALUES (8, N'DDD', 2)
SET IDENTITY_INSERT [dbo].[District] OFF
/****** Object:  Table [dbo].[Area]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Area](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](255) NOT NULL,
	[AreaName] [nvarchar](max) NOT NULL,
	[Description] [ntext] NULL,
	[RegionId] [int] NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Area] ON
INSERT [dbo].[Area] ([Id], [UpiCode], [AreaName], [Description], [RegionId]) VALUES (1, N'ARA01', N'Area 1', N'this is area 1', 1)
INSERT [dbo].[Area] ([Id], [UpiCode], [AreaName], [Description], [RegionId]) VALUES (2, N'ARA02', N'Area 2', N'This is area2', 2)
INSERT [dbo].[Area] ([Id], [UpiCode], [AreaName], [Description], [RegionId]) VALUES (3, N'ARA03', N'Area 3', N'taa', 3)
SET IDENTITY_INSERT [dbo].[Area] OFF
/****** Object:  Table [dbo].[SalesGroup]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesmenId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_SalesGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SalesGroup] ON
INSERT [dbo].[SalesGroup] ([Id], [SalesmenId], [GroupId]) VALUES (2, 3, 1)
INSERT [dbo].[SalesGroup] ([Id], [SalesmenId], [GroupId]) VALUES (3, 1, 2)
INSERT [dbo].[SalesGroup] ([Id], [SalesmenId], [GroupId]) VALUES (4, 2, 2)
INSERT [dbo].[SalesGroup] ([Id], [SalesmenId], [GroupId]) VALUES (6, 4, 2)
SET IDENTITY_INSERT [dbo].[SalesGroup] OFF
/****** Object:  Table [dbo].[Local]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Local](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](max) NOT NULL,
	[LocalName] [nvarchar](255) NOT NULL,
	[Description] [ntext] NULL,
	[AreaId] [int] NOT NULL,
 CONSTRAINT [PK_Local] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Local] ON
INSERT [dbo].[Local] ([Id], [UpiCode], [LocalName], [Description], [AreaId]) VALUES (1, N'LC01', N'Local 1', N'this is local 1', 1)
INSERT [dbo].[Local] ([Id], [UpiCode], [LocalName], [Description], [AreaId]) VALUES (2, N'LC02', N'Local 2', N'aaaa', 2)
INSERT [dbo].[Local] ([Id], [UpiCode], [LocalName], [Description], [AreaId]) VALUES (3, N'LC03', N'Local 3', N'aaa', 2)
INSERT [dbo].[Local] ([Id], [UpiCode], [LocalName], [Description], [AreaId]) VALUES (4, N'LC04', N'Local 3', N'aaa', 3)
INSERT [dbo].[Local] ([Id], [UpiCode], [LocalName], [Description], [AreaId]) VALUES (5, N'LCL05', N'Local 5', N'This is local 5', 3)
SET IDENTITY_INSERT [dbo].[Local] OFF
/****** Object:  Table [dbo].[SalesArea]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesArea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesmenId] [int] NOT NULL,
	[AreaId] [int] NOT NULL,
 CONSTRAINT [PK_SaleArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesLocal]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesLocal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesmenId] [int] NOT NULL,
	[LocalId] [int] NOT NULL,
 CONSTRAINT [PK_SalesLocal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SalesLocal] ON
INSERT [dbo].[SalesLocal] ([Id], [SalesmenId], [LocalId]) VALUES (1, 8, 1)
SET IDENTITY_INSERT [dbo].[SalesLocal] OFF
/****** Object:  Table [dbo].[Customer]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [varchar](max) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[Street] [nvarchar](255) NULL,
	[Ward] [nvarchar](255) NULL,
	[Phone] [varchar](50) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[CustomerTypeId] [int] NULL,
	[ChannelId] [int] NULL,
	[DistrictId] [int] NULL,
	[LocalId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[Status] [bit] NULL,
	[IsEnable] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT [dbo].[Customer] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsEnable]) VALUES (1, N'CUST01', N'Nguyen Van An', N'123', N'Nguyen Van Troi', N'4', N'0962345636', N'123', 1, 1, 1, 1, CAST(0x00009FBC00000000 AS DateTime), CAST(0x00009FBC00000000 AS DateTime), 1, 1)
INSERT [dbo].[Customer] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsEnable]) VALUES (2, N'CUST2', N'Tran Van B1rtet', N'12/A', N'Dien Bien Phu', N'Da Kao', N'01223456783', N'123', 1, 1, 2, 4, CAST(0x00009FBC00000000 AS DateTime), CAST(0x00009FBC00000000 AS DateTime), 1, 1)
INSERT [dbo].[Customer] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsEnable]) VALUES (4, N'customerdê', N'nguoicungkho', N'qwe', N'12', N'1', N'0123999555', N'123456', 1, 1, 1, 1, CAST(0x00009FD300000000 AS DateTime), CAST(0x00009FE400000000 AS DateTime), 1, 0)
INSERT [dbo].[Customer] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsEnable]) VALUES (5, N'customerdê', N'nguoicungkho', N'qwe', N'12', N'1', N'0123999555', N'123456', 1, 1, 1, 1, CAST(0x00009FD300000000 AS DateTime), CAST(0x00009FE400000000 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Customer] OFF
/****** Object:  Table [dbo].[CustomerSupervisor]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerSupervisor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[Street] [nvarchar](255) NULL,
	[Ward] [nvarchar](255) NULL,
	[Phone] [varchar](50) NOT NULL,
	[CustomerId] [int] NULL,
	[DistrictId] [int] NULL,
	[PositionId] [int] NULL,
 CONSTRAINT [PK_CustomerSupervisor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerSupervisor] ON
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (1, N'Dang Vu', N'532/21/25', N'Kinh Duong Vuong', N'Binh Tring Dong B', N'0912356831', 1, 1, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (2, N'aaa', N'abc', N'111', N'123', N'0987654732', 1, 2, 1)
SET IDENTITY_INSERT [dbo].[CustomerSupervisor] OFF
/****** Object:  Table [dbo].[CustomerLog]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpiCode] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Street] [nvarchar](max) NULL,
	[Ward] [nvarchar](max) NULL,
	[Phone] [varchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[CustomerTypeId] [int] NULL,
	[ChannelId] [int] NULL,
	[DistrictId] [int] NULL,
	[LocalId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[Status] [bit] NULL,
	[IsApprove] [bit] NULL,
	[ApproveBy] [int] NULL,
	[ChangeBy] [int] NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_CustomerLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerLog] ON
INSERT [dbo].[CustomerLog] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsApprove], [ApproveBy], [ChangeBy], [CustomerId]) VALUES (10, N'CUST2', N'Tran Van B1', N'12/A', N'Dien Bien Phu', N'Da Kao', N'01223456783', N'123', 1, 1, 2, 4, CAST(0x00009FBC00000000 AS DateTime), CAST(0x00009FBC00000000 AS DateTime), 1, 0, 0, NULL, 2)
INSERT [dbo].[CustomerLog] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsApprove], [ApproveBy], [ChangeBy], [CustomerId]) VALUES (12, N'CUST01', N'Nguyen Van A', N'123', N'Nguyen Van Troi', N'4', N'0962345636', N'123', 1, 1, 1, 1, CAST(0x00009FBC00000000 AS DateTime), CAST(0x00009FBC00000000 AS DateTime), 1, 0, 0, NULL, 1)
INSERT [dbo].[CustomerLog] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsApprove], [ApproveBy], [ChangeBy], [CustomerId]) VALUES (13, N'CUST02', N'Tran Van B1rtet', N'12/A', N'Dien Bien Phu', N'Da Kao', N'01223456783', N'123', 1, 1, 2, 4, CAST(0x00009FBC00000000 AS DateTime), CAST(0x00009FBC00000000 AS DateTime), 1, 0, 0, NULL, 2)
INSERT [dbo].[CustomerLog] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsApprove], [ApproveBy], [ChangeBy], [CustomerId]) VALUES (14, N'customerđê', N'nguoicungkho', N'qwe', N'12', N'1', N'0123999555', N'123456', 1, 1, 1, 1, CAST(0x00009FD300000000 AS DateTime), CAST(0x00009FE400000000 AS DateTime), 1, 0, 0, NULL, 4)
INSERT [dbo].[CustomerLog] ([Id], [UpiCode], [FullName], [Address], [Street], [Ward], [Phone], [Password], [CustomerTypeId], [ChannelId], [DistrictId], [LocalId], [CreateDate], [UpdateDate], [Status], [IsApprove], [ApproveBy], [ChangeBy], [CustomerId]) VALUES (15, N'customerđê', N'nguoicungkho', N'qwe', N'12', N'1', N'0123999555', N'123456', 1, 1, 1, 1, CAST(0x00009FD300000000 AS DateTime), CAST(0x00009FE400000000 AS DateTime), 1, 0, 0, NULL, 5)
SET IDENTITY_INSERT [dbo].[CustomerLog] OFF
/****** Object:  Table [dbo].[SupervisorManageCustomer]    Script Date: 01/14/2012 19:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupervisorManageCustomer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[SupervisorId] [int] NULL,
 CONSTRAINT [PK_SupervisorManageCustomer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SupervisorManageCustomer] ON
INSERT [dbo].[SupervisorManageCustomer] ([Id], [CustomerId], [SupervisorId]) VALUES (1, 2, 2)
INSERT [dbo].[SupervisorManageCustomer] ([Id], [CustomerId], [SupervisorId]) VALUES (2, 1, 2)
SET IDENTITY_INSERT [dbo].[SupervisorManageCustomer] OFF
/****** Object:  Default [DF_Administrator_AllowApprove]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Administrator] ADD  CONSTRAINT [DF_Administrator_AllowApprove]  DEFAULT ((0)) FOR [AllowApprove]
GO
/****** Object:  ForeignKey [FK_Area_Region]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_Region]
GO
/****** Object:  ForeignKey [FK_AssignFunction_Administrator]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[AssignFunction]  WITH CHECK ADD  CONSTRAINT [FK_AssignFunction_Administrator] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[Administrator] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[AssignFunction] CHECK CONSTRAINT [FK_AssignFunction_Administrator]
GO
/****** Object:  ForeignKey [FK_AssignFunction_Function]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[AssignFunction]  WITH CHECK ADD  CONSTRAINT [FK_AssignFunction_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Function] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssignFunction] CHECK CONSTRAINT [FK_AssignFunction_Function]
GO
/****** Object:  ForeignKey [FK_Customer_Channel]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Channel] FOREIGN KEY([ChannelId])
REFERENCES [dbo].[Channel] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Channel]
GO
/****** Object:  ForeignKey [FK_Customer_CustomerType]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerType] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[CustomerType] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerType]
GO
/****** Object:  ForeignKey [FK_Customer_District]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_District]
GO
/****** Object:  ForeignKey [FK_Customer_Local]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Local] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Local] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Local]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Channel]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Channel] FOREIGN KEY([ChannelId])
REFERENCES [dbo].[Channel] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Channel]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Customer]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerLog_CustomerType]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_CustomerType] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[CustomerType] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_CustomerType]
GO
/****** Object:  ForeignKey [FK_CustomerLog_District]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_District]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Local]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Local] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Local] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Local]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Salesmen]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Salesmen] FOREIGN KEY([ChangeBy])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Salesmen]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_Customer]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSupervisor_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerSupervisor] CHECK CONSTRAINT [FK_CustomerSupervisor_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_District]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSupervisor_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerSupervisor] CHECK CONSTRAINT [FK_CustomerSupervisor_District]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_SupervisorPosition]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[CustomerSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSupervisor_SupervisorPosition] FOREIGN KEY([PositionId])
REFERENCES [dbo].[SupervisorPosition] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerSupervisor] CHECK CONSTRAINT [FK_CustomerSupervisor_SupervisorPosition]
GO
/****** Object:  ForeignKey [FK_District_Province]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_Province] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[Province] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_Province]
GO
/****** Object:  ForeignKey [FK_Local_Area]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Local]  WITH CHECK ADD  CONSTRAINT [FK_Local_Area] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Local] CHECK CONSTRAINT [FK_Local_Area]
GO
/****** Object:  ForeignKey [FK_Promotion_Administrator]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Administrator] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[Administrator] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Administrator]
GO
/****** Object:  ForeignKey [FK_Province_Section]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Province]  WITH CHECK ADD  CONSTRAINT [FK_Province_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Province] CHECK CONSTRAINT [FK_Province_Section]
GO
/****** Object:  ForeignKey [FK_Region_Group]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Region]  WITH CHECK ADD  CONSTRAINT [FK_Region_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Region] CHECK CONSTRAINT [FK_Region_Group]
GO
/****** Object:  ForeignKey [FK_SalesArea_Area]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesArea]  WITH CHECK ADD  CONSTRAINT [FK_SalesArea_Area] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesArea] CHECK CONSTRAINT [FK_SalesArea_Area]
GO
/****** Object:  ForeignKey [FK_SalesArea_Salesmen]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesArea]  WITH CHECK ADD  CONSTRAINT [FK_SalesArea_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesArea] CHECK CONSTRAINT [FK_SalesArea_Salesmen]
GO
/****** Object:  ForeignKey [FK_SalesGroup_Group]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesGroup]  WITH CHECK ADD  CONSTRAINT [FK_SalesGroup_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesGroup] CHECK CONSTRAINT [FK_SalesGroup_Group]
GO
/****** Object:  ForeignKey [FK_SalesGroup_Salesmen]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesGroup]  WITH CHECK ADD  CONSTRAINT [FK_SalesGroup_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesGroup] CHECK CONSTRAINT [FK_SalesGroup_Salesmen]
GO
/****** Object:  ForeignKey [FK_SalesLocal_Local]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesLocal]  WITH CHECK ADD  CONSTRAINT [FK_SalesLocal_Local] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Local] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesLocal] CHECK CONSTRAINT [FK_SalesLocal_Local]
GO
/****** Object:  ForeignKey [FK_SalesLocal_Salesmen]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesLocal]  WITH CHECK ADD  CONSTRAINT [FK_SalesLocal_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesLocal] CHECK CONSTRAINT [FK_SalesLocal_Salesmen]
GO
/****** Object:  ForeignKey [FK_Salesmen_Role]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[Salesmen]  WITH CHECK ADD  CONSTRAINT [FK_Salesmen_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Salesmen] CHECK CONSTRAINT [FK_Salesmen_Role]
GO
/****** Object:  ForeignKey [FK_SalesRegion_Region]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesRegion]  WITH CHECK ADD  CONSTRAINT [FK_SalesRegion_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesRegion] CHECK CONSTRAINT [FK_SalesRegion_Region]
GO
/****** Object:  ForeignKey [FK_SalesRegion_Salesmen]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SalesRegion]  WITH CHECK ADD  CONSTRAINT [FK_SalesRegion_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesRegion] CHECK CONSTRAINT [FK_SalesRegion_Salesmen]
GO
/****** Object:  ForeignKey [FK_SmsObj_Promotion]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SmsObj]  WITH CHECK ADD  CONSTRAINT [FK_SmsObj_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotion] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[SmsObj] CHECK CONSTRAINT [FK_SmsObj_Promotion]
GO
/****** Object:  ForeignKey [FK_SmsObj_SmsType]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SmsObj]  WITH CHECK ADD  CONSTRAINT [FK_SmsObj_SmsType] FOREIGN KEY([SmsTypeId])
REFERENCES [dbo].[SmsType] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[SmsObj] CHECK CONSTRAINT [FK_SmsObj_SmsType]
GO
/****** Object:  ForeignKey [FK_SupervisorManageCustomer_Customer]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SupervisorManageCustomer]  WITH CHECK ADD  CONSTRAINT [FK_SupervisorManageCustomer_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupervisorManageCustomer] CHECK CONSTRAINT [FK_SupervisorManageCustomer_Customer]
GO
/****** Object:  ForeignKey [FK_SupervisorManageCustomer_CustomerSupervisor]    Script Date: 01/14/2012 19:13:21 ******/
ALTER TABLE [dbo].[SupervisorManageCustomer]  WITH CHECK ADD  CONSTRAINT [FK_SupervisorManageCustomer_CustomerSupervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[CustomerSupervisor] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupervisorManageCustomer] CHECK CONSTRAINT [FK_SupervisorManageCustomer_CustomerSupervisor]
GO
