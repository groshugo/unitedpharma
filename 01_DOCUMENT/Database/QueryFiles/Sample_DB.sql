USE [UPI]
GO
/****** Object:  ForeignKey [FK_Area_Region]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Area] DROP CONSTRAINT [FK_Area_Region]
GO
/****** Object:  ForeignKey [FK_AssignFunction_Administrator]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[AssignFunction] DROP CONSTRAINT [FK_AssignFunction_Administrator]
GO
/****** Object:  ForeignKey [FK_AssignFunction_Function]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[AssignFunction] DROP CONSTRAINT [FK_AssignFunction_Function]
GO
/****** Object:  ForeignKey [FK_Customer_Channel]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Channel]
GO
/****** Object:  ForeignKey [FK_Customer_CustomerType]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_CustomerType]
GO
/****** Object:  ForeignKey [FK_Customer_Local]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Local]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Channel]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_Channel]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Customer]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerLog_CustomerType]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_CustomerType]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Local]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_Local]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_Customer]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerSupervisor] DROP CONSTRAINT [FK_CustomerSupervisor_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_District]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerSupervisor] DROP CONSTRAINT [FK_CustomerSupervisor_District]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_SupervisorPosition]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerSupervisor] DROP CONSTRAINT [FK_CustomerSupervisor_SupervisorPosition]
GO
/****** Object:  ForeignKey [FK_District_Province]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[District] DROP CONSTRAINT [FK_District_Province]
GO
/****** Object:  ForeignKey [FK_Local_Area]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Local] DROP CONSTRAINT [FK_Local_Area]
GO
/****** Object:  ForeignKey [FK_Promotion_Administrator]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Promotion] DROP CONSTRAINT [FK_Promotion_Administrator]
GO
/****** Object:  ForeignKey [FK_Province_Section]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Province] DROP CONSTRAINT [FK_Province_Section]
GO
/****** Object:  ForeignKey [FK_Region_Group]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Region] DROP CONSTRAINT [FK_Region_Group]
GO
/****** Object:  ForeignKey [FK_SalesArea_Area]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesArea] DROP CONSTRAINT [FK_SalesArea_Area]
GO
/****** Object:  ForeignKey [FK_SalesArea_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesArea] DROP CONSTRAINT [FK_SalesArea_Salesmen]
GO
/****** Object:  ForeignKey [FK_SalesGroup_Group]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesGroup] DROP CONSTRAINT [FK_SalesGroup_Group]
GO
/****** Object:  ForeignKey [FK_SalesGroup_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesGroup] DROP CONSTRAINT [FK_SalesGroup_Salesmen]
GO
/****** Object:  ForeignKey [FK_SalesLocal_Local]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesLocal] DROP CONSTRAINT [FK_SalesLocal_Local]
GO
/****** Object:  ForeignKey [FK_SalesLocal_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesLocal] DROP CONSTRAINT [FK_SalesLocal_Salesmen]
GO
/****** Object:  ForeignKey [FK_Salesmen_Role]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Salesmen] DROP CONSTRAINT [FK_Salesmen_Role]
GO
/****** Object:  ForeignKey [FK_SalesRegion_Region]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesRegion] DROP CONSTRAINT [FK_SalesRegion_Region]
GO
/****** Object:  ForeignKey [FK_SalesRegion_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesRegion] DROP CONSTRAINT [FK_SalesRegion_Salesmen]
GO
/****** Object:  ForeignKey [FK_SmsObj_SmsType]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SmsObj] DROP CONSTRAINT [FK_SmsObj_SmsType]
GO
/****** Object:  ForeignKey [FK_SupervisorManageCustomer_Customer]    Script Date: 04/30/2012 23:50:25 ******/
ALTER TABLE [dbo].[SupervisorManageCustomer] DROP CONSTRAINT [FK_SupervisorManageCustomer_Customer]
GO
/****** Object:  ForeignKey [FK_SupervisorManageCustomer_CustomerSupervisor]    Script Date: 04/30/2012 23:50:25 ******/
ALTER TABLE [dbo].[SupervisorManageCustomer] DROP CONSTRAINT [FK_SupervisorManageCustomer_CustomerSupervisor]
GO
/****** Object:  StoredProcedure [dbo].[SP_SchedulePromotion]    Script Date: 04/30/2012 23:50:44 ******/
DROP PROCEDURE [dbo].[SP_SchedulePromotion]
GO
/****** Object:  Table [dbo].[SupervisorManageCustomer]    Script Date: 04/30/2012 23:50:25 ******/
ALTER TABLE [dbo].[SupervisorManageCustomer] DROP CONSTRAINT [FK_SupervisorManageCustomer_Customer]
GO
ALTER TABLE [dbo].[SupervisorManageCustomer] DROP CONSTRAINT [FK_SupervisorManageCustomer_CustomerSupervisor]
GO
DROP TABLE [dbo].[SupervisorManageCustomer]
GO
/****** Object:  Table [dbo].[CustomerLog]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_Channel]
GO
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_Customer]
GO
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_CustomerType]
GO
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_Local]
GO
DROP TABLE [dbo].[CustomerLog]
GO
/****** Object:  Table [dbo].[CustomerSupervisor]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerSupervisor] DROP CONSTRAINT [FK_CustomerSupervisor_Customer]
GO
ALTER TABLE [dbo].[CustomerSupervisor] DROP CONSTRAINT [FK_CustomerSupervisor_District]
GO
ALTER TABLE [dbo].[CustomerSupervisor] DROP CONSTRAINT [FK_CustomerSupervisor_SupervisorPosition]
GO
DROP TABLE [dbo].[CustomerSupervisor]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Channel]
GO
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_CustomerType]
GO
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Local]
GO
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[SalesLocal]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesLocal] DROP CONSTRAINT [FK_SalesLocal_Local]
GO
ALTER TABLE [dbo].[SalesLocal] DROP CONSTRAINT [FK_SalesLocal_Salesmen]
GO
DROP TABLE [dbo].[SalesLocal]
GO
/****** Object:  Table [dbo].[Local]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Local] DROP CONSTRAINT [FK_Local_Area]
GO
DROP TABLE [dbo].[Local]
GO
/****** Object:  Table [dbo].[SalesArea]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesArea] DROP CONSTRAINT [FK_SalesArea_Area]
GO
ALTER TABLE [dbo].[SalesArea] DROP CONSTRAINT [FK_SalesArea_Salesmen]
GO
DROP TABLE [dbo].[SalesArea]
GO
/****** Object:  Table [dbo].[SalesGroup]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesGroup] DROP CONSTRAINT [FK_SalesGroup_Group]
GO
ALTER TABLE [dbo].[SalesGroup] DROP CONSTRAINT [FK_SalesGroup_Salesmen]
GO
DROP TABLE [dbo].[SalesGroup]
GO
/****** Object:  Table [dbo].[District]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[District] DROP CONSTRAINT [FK_District_Province]
GO
DROP TABLE [dbo].[District]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Area] DROP CONSTRAINT [FK_Area_Region]
GO
DROP TABLE [dbo].[Area]
GO
/****** Object:  Table [dbo].[SalesRegion]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesRegion] DROP CONSTRAINT [FK_SalesRegion_Region]
GO
ALTER TABLE [dbo].[SalesRegion] DROP CONSTRAINT [FK_SalesRegion_Salesmen]
GO
DROP TABLE [dbo].[SalesRegion]
GO
/****** Object:  Table [dbo].[Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Salesmen] DROP CONSTRAINT [FK_Salesmen_Role]
GO
DROP TABLE [dbo].[Salesmen]
GO
/****** Object:  Table [dbo].[SmsObj]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SmsObj] DROP CONSTRAINT [FK_SmsObj_SmsType]
GO
DROP TABLE [dbo].[SmsObj]
GO
/****** Object:  Table [dbo].[AssignFunction]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[AssignFunction] DROP CONSTRAINT [FK_AssignFunction_Administrator]
GO
ALTER TABLE [dbo].[AssignFunction] DROP CONSTRAINT [FK_AssignFunction_Function]
GO
DROP TABLE [dbo].[AssignFunction]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Promotion] DROP CONSTRAINT [FK_Promotion_Administrator]
GO
DROP TABLE [dbo].[Promotion]
GO
/****** Object:  Table [dbo].[Province]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Province] DROP CONSTRAINT [FK_Province_Section]
GO
DROP TABLE [dbo].[Province]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Region] DROP CONSTRAINT [FK_Region_Group]
GO
DROP TABLE [dbo].[Region]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 04/30/2012 23:50:24 ******/
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Function]    Script Date: 04/30/2012 23:50:23 ******/
DROP TABLE [dbo].[Function]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 04/30/2012 23:50:23 ******/
DROP TABLE [dbo].[Groups]
GO
/****** Object:  Table [dbo].[Administrator]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Administrator] DROP CONSTRAINT [DF_Administrator_AllowApprove]
GO
DROP TABLE [dbo].[Administrator]
GO
/****** Object:  Table [dbo].[Channel]    Script Date: 04/30/2012 23:50:23 ******/
DROP TABLE [dbo].[Channel]
GO
/****** Object:  Table [dbo].[CustomerType]    Script Date: 04/30/2012 23:50:23 ******/
DROP TABLE [dbo].[CustomerType]
GO
/****** Object:  Table [dbo].[Dashboard]    Script Date: 04/30/2012 23:50:23 ******/
DROP TABLE [dbo].[Dashboard]
GO
/****** Object:  Table [dbo].[SmsType]    Script Date: 04/30/2012 23:50:25 ******/
DROP TABLE [dbo].[SmsType]
GO
/****** Object:  Table [dbo].[SupervisorPosition]    Script Date: 04/30/2012 23:50:25 ******/
DROP TABLE [dbo].[SupervisorPosition]
GO
/****** Object:  Table [dbo].[SchedulePromotion]    Script Date: 04/30/2012 23:50:24 ******/
DROP TABLE [dbo].[SchedulePromotion]
GO
/****** Object:  Table [dbo].[Section]    Script Date: 04/30/2012 23:50:24 ******/
DROP TABLE [dbo].[Section]
GO
/****** Object:  Table [dbo].[Section]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[SchedulePromotion]    Script Date: 04/30/2012 23:50:24 ******/
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
	[SMSIdList] [ntext] NULL,
 CONSTRAINT [PK_SchedulePromotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SchedulePromotion] ON
INSERT [dbo].[SchedulePromotion] ([Id], [UpiCode], [Title], [SMSContent], [WebContent], [StartDate], [EndDate], [AdministratorId], [IsApprove], [PhoneNumbers], [SMSIdList]) VALUES (7, N'222', N'nguoicungkho', N'Virtual scrolling for RadGrid', N'Virtual scrolling for RadGrid can be attained by ajaxifying the control via RadAjaxManager', CAST(0x00009FF800000000 AS DateTime), CAST(0x0000A01500000000 AS DateTime), 7, 1, N'0962345636,0123456789', NULL)
INSERT [dbo].[SchedulePromotion] ([Id], [UpiCode], [Title], [SMSContent], [WebContent], [StartDate], [EndDate], [AdministratorId], [IsApprove], [PhoneNumbers], [SMSIdList]) VALUES (8, N'666', N'66', N'fdsf', N'df', CAST(0x00009FDA00000000 AS DateTime), CAST(0x00009FE900000000 AS DateTime), 1, 1, N'0962345636,0962345636,01223456783,0123999555', NULL)
SET IDENTITY_INSERT [dbo].[SchedulePromotion] OFF
/****** Object:  Table [dbo].[SupervisorPosition]    Script Date: 04/30/2012 23:50:25 ******/
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
/****** Object:  Table [dbo].[SmsType]    Script Date: 04/30/2012 23:50:25 ******/
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
/****** Object:  Table [dbo].[Dashboard]    Script Date: 04/30/2012 23:50:23 ******/
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
	[SenderPhoneNumber] [nvarchar](max) NULL,
	[ReceiverPhoneNumber] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[IsRead] [bit] NULL,
	[AttachedFileName] [nvarchar](400) NULL,
 CONSTRAINT [PK_Dashborad] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Dashboard] ON
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (1, N'Number 1', N'This is Number 1', CAST(0x00009FBB00713FA7 AS DateTime), CAST(0x00009FBB00713FA7 AS DateTime), NULL, N'0914343432', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (2, N'Number 2', N'aaaa', CAST(0x00009FBB00714D49 AS DateTime), CAST(0x00009FBB00714D49 AS DateTime), NULL, N'0914343432', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (3, N'For Ronaldo', N'This is inform from Admin4b', CAST(0x00009FBB0071D33D AS DateTime), CAST(0x00009FBB007206A1 AS DateTime), NULL, N'0909432575', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (4, N'', N'', CAST(0x00009FD80121EE69 AS DateTime), CAST(0x00009FD80121EE69 AS DateTime), NULL, N'01224356421', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (5, N'', N'', CAST(0x00009FD80121EEA9 AS DateTime), CAST(0x00009FD80121EEA9 AS DateTime), NULL, N'0914343432', 1, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (6, N'', N'', CAST(0x00009FD80121EEAB AS DateTime), CAST(0x00009FD80121EEAB AS DateTime), NULL, N'0987435325', 1, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (7, N'', N'', CAST(0x00009FD80121EEAB AS DateTime), CAST(0x00009FD80121EEAB AS DateTime), NULL, N'0909432575', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (8, N'', N'', CAST(0x00009FD80121EEAD AS DateTime), CAST(0x00009FD80121EEAD AS DateTime), NULL, N'0987542764', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (9, N'', N'', CAST(0x00009FD80121EEAE AS DateTime), CAST(0x00009FD80121EEAE AS DateTime), NULL, N'0951234567', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (10, N'SSS', N'DDD', CAST(0x00009FD80121EEAE AS DateTime), CAST(0x0000A03400250FCB AS DateTime), NULL, N'0967348328', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (11, N'', N'', CAST(0x00009FD80121EEAF AS DateTime), CAST(0x00009FD80121EEAF AS DateTime), NULL, N'0483928764', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (12, N'', N'', CAST(0x00009FD80121EEB3 AS DateTime), CAST(0x00009FD80121EEB3 AS DateTime), NULL, N'0907325465', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (13, N'', N'', CAST(0x00009FD80121EEB4 AS DateTime), CAST(0x00009FD80121EEB4 AS DateTime), NULL, N'0912345324', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (14, N'', N'', CAST(0x00009FD80121EEB5 AS DateTime), CAST(0x00009FD80121EEB5 AS DateTime), NULL, N'0986473843', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (15, N'test', N'test.....', CAST(0x00009FD80134D534 AS DateTime), CAST(0x00009FD80134D535 AS DateTime), NULL, N'01224356421', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (16, N'test', N'test.....', CAST(0x00009FD80134D53A AS DateTime), CAST(0x00009FD80134D53A AS DateTime), NULL, N'0909432575', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (17, N'test', N'test.....', CAST(0x00009FD80134D53B AS DateTime), CAST(0x00009FD80134D53B AS DateTime), NULL, N'0987435325', 0, 1, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (18, N'test', N'test.....', CAST(0x00009FD80134D53B AS DateTime), CAST(0x00009FD80134D53B AS DateTime), NULL, N'0987542764', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (19, N'test', N'test.....', CAST(0x00009FD80134D53C AS DateTime), CAST(0x00009FD80134D53C AS DateTime), NULL, N'0907325465', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (20, N'test', N'test.....', CAST(0x00009FD80134D53C AS DateTime), CAST(0x00009FD80134D53C AS DateTime), NULL, N'0912345324', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (21, N'test', N'test.....', CAST(0x00009FD80134D541 AS DateTime), CAST(0x00009FD80134D541 AS DateTime), NULL, N'0909313336', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (22, N'test', N'test.....', CAST(0x00009FD80134D542 AS DateTime), CAST(0x00009FD80134D542 AS DateTime), NULL, N'0909838826', 0, NULL, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (23, N'ABC', N'TEST SAD', CAST(0x0000A00C00CFFA15 AS DateTime), CAST(0x0000A034001FFD7D AS DateTime), N'0909313336', N'0903437857', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (24, N'AAATest', N'AAAA', CAST(0x0000A02B00C85B8E AS DateTime), CAST(0x0000A02B00C85B8E AS DateTime), N'0909313336', N'0914343432', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (25, N'AAATest', N'AAAA', CAST(0x0000A02B00C85BC7 AS DateTime), CAST(0x0000A02B00C85BC7 AS DateTime), N'0909313336', N'0987435300', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (26, N'AAATest', N'AAAA', CAST(0x0000A02B00C85BEC AS DateTime), CAST(0x0000A02B00C85BEC AS DateTime), N'0909313336', N'0909432575', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (27, N'AAADDD', N'DDD', CAST(0x0000A02B00C8E7C4 AS DateTime), CAST(0x0000A02B00C8E7C4 AS DateTime), N'0909313336', N'0483928764', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (28, N'AAADDD', N'DDD', CAST(0x0000A02B00C8E7CB AS DateTime), CAST(0x0000A02B00C8E7CB AS DateTime), N'0909313336', N'0967348328', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (29, N'QQQ', N'QQQ', CAST(0x0000A02B00C95B1E AS DateTime), CAST(0x0000A02B00C95B1E AS DateTime), N'0909313336', N'0914343432', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (30, N'DDD', N'DDD', CAST(0x0000A02B00CB1EE7 AS DateTime), CAST(0x0000A02B00CB1EE7 AS DateTime), N'0909313336', N'0987435300', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (31, N'DDD', N'DDD', CAST(0x0000A02B00CB1EF2 AS DateTime), CAST(0x0000A02B00CB1EF2 AS DateTime), N'0909313336', N'0909432575', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (32, N'DDD', N'DDD', CAST(0x0000A02B00CB1EF8 AS DateTime), CAST(0x0000A02B00CB1EF8 AS DateTime), N'0909313336', N'01224356421', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (33, N'OKOKOOK', N'OKOKOOK', CAST(0x0000A02B00CE1B05 AS DateTime), CAST(0x0000A02B00CE1B05 AS DateTime), N'0909313336', N'0914343432', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (34, N'OKOKOOK', N'OKOKOOK', CAST(0x0000A02B00CE1B2D AS DateTime), CAST(0x0000A02B00CE1B2D AS DateTime), N'0909313336', N'0987435300', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (35, N'CCCCC', N'CCCCC', CAST(0x0000A02B00CEE71C AS DateTime), CAST(0x0000A02B00CEE71C AS DateTime), N'0909313336', N'0987435325', 0, 1, N'1_AudienceType.jpg')
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (36, N'CCCCC', N'CCCCC', CAST(0x0000A02B00CEE722 AS DateTime), CAST(0x0000A02B00CEE722 AS DateTime), N'0909313336', N'0986473843', 0, 0, N'1_AudienceType.jpg')
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (37, N'DDD', N'DDDDD', CAST(0x0000A02B00CF3F9F AS DateTime), CAST(0x0000A02B00CF3F9F AS DateTime), N'0909313336', N'0909432575', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (38, N'DDD', N'DDDDD', CAST(0x0000A02B00CF3FA3 AS DateTime), CAST(0x0000A02B00CF3FA3 AS DateTime), N'0909313336', N'0987435300', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (39, N'FFF', N'FFFF', CAST(0x0000A02B00CFC921 AS DateTime), CAST(0x0000A02B00CFC921 AS DateTime), N'0909313336', N'0986473843', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (40, N'FFF', N'FFFF', CAST(0x0000A02B00CFC928 AS DateTime), CAST(0x0000A02B00CFC928 AS DateTime), N'0909313336', N'0483928764', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (41, N'EEEEE', N'EEEE', CAST(0x0000A02B00CFDDC6 AS DateTime), CAST(0x0000A02B00CFDDC6 AS DateTime), N'0909313336', N'01224356421', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (42, N'EEEEE', N'EEEE', CAST(0x0000A02B00CFDDCC AS DateTime), CAST(0x0000A02B00CFDDCC AS DateTime), N'0909313336', N'0909432575', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (43, N'444', N'4444', CAST(0x0000A02B00CFF709 AS DateTime), CAST(0x0000A02B00CFF709 AS DateTime), N'0909313336', N'0951234567', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (44, N'444', N'4444', CAST(0x0000A02B00CFF70E AS DateTime), CAST(0x0000A02B00CFF70E AS DateTime), N'0909313336', N'0967348328', 0, 0, NULL)
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (45, N'Test detail', N'Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail ', CAST(0x0000A02C017927AA AS DateTime), CAST(0x0000A02C017927AA AS DateTime), N'0909313336', N'0987435300', 0, 0, N'1_1x1.png')
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (46, N'Test detail', N'Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail  Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail  ', CAST(0x0000A02C017927DF AS DateTime), CAST(0x0000A02C017927DF AS DateTime), N'0909313336', N'0908419981', 0, 0, N'1_1x1.png')
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (47, N'Test detail bbb', N'Cu chuoi', CAST(0x0000A02C017927EF AS DateTime), CAST(0x0000A034000E8503 AS DateTime), N'0909313336', N'0908419981', 0, 0, N'1_1x1.png')
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (48, N'Test detail nua ne', N'Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail ', CAST(0x0000A02C0179B93A AS DateTime), CAST(0x0000A02C0179B93A AS DateTime), N'0909313336', N'0908419981', 0, 0, N'1_developing-with-the-catalog-system.doc')
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (49, N'Test detail nua ne', N'Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail ', CAST(0x0000A02C0179B93C AS DateTime), CAST(0x0000A02C0179B93C AS DateTime), N'0909313336', N'0908419981', 0, 0, N'1_developing-with-the-catalog-system.doc')
INSERT [dbo].[Dashboard] ([Id], [Title], [Content], [CreateDate], [UpdateDate], [SenderPhoneNumber], [ReceiverPhoneNumber], [IsDeleted], [IsRead], [AttachedFileName]) VALUES (50, N'Test detail nua ne', N'Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail Test detail ', CAST(0x0000A02C0179B945 AS DateTime), CAST(0x0000A02C0179B945 AS DateTime), N'0909313336', N'0908419981', 0, 0, N'1_developing-with-the-catalog-system.doc')
SET IDENTITY_INSERT [dbo].[Dashboard] OFF
/****** Object:  Table [dbo].[CustomerType]    Script Date: 04/30/2012 23:50:23 ******/
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
/****** Object:  Table [dbo].[Channel]    Script Date: 04/30/2012 23:50:23 ******/
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
/****** Object:  Table [dbo].[Administrator]    Script Date: 04/30/2012 23:50:23 ******/
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
	[AllowApprove] [bit] NULL CONSTRAINT [DF_Administrator_AllowApprove]  DEFAULT ((0)),
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
/****** Object:  Table [dbo].[Groups]    Script Date: 04/30/2012 23:50:23 ******/
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
/****** Object:  Table [dbo].[Function]    Script Date: 04/30/2012 23:50:23 ******/
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
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (2, N'SMS & Customers Data', NULL, NULL)
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (3, N'Geographic', NULL, NULL)
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (4, N'Supervisor Position', 2, N'SupervisorPositionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (5, N'Customer Supervisor', 2, N'CustomerSupervisorManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (6, N'Customer Type', 2, N'CustomerTypeManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (7, N'Channels', 2, N'ChannelManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (8, N'Customers', 1000, N'CustomerManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (9, N'Customer Log', 1000, N'CustomerLogManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (10, N'Roles', 1, N'RoleManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (11, N'Dashboard ', 1000, N'DashboardManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (12, N'Salesmen', 1000, N'SalesmanManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (13, N'Administrator', 1, N'AdministratorManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (14, N'Functions', 1000, N'FunctionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (15, N'Assign Function', 1, N'AssignFunctionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (16, N'Promotion', 1000, N'PromotionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (17, N'SMS Type', 2, N'SmsTypeManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (18, N'Sms Quota', 1000, N'SmsQuota.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (19, N'Groups', 34, N'GroupManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (20, N'Regions', 34, N'RegionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (21, N'Areas', 34, N'AreaManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (22, N'Locals', 34, N'LocalManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (23, N'Sections', 3, N'SectionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (24, N'Provinces', 3, N'ProvinceManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (25, N'Districts', 3, N'DistrictManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (26, N'Salesmen - Group', 34, N'SalesmenGroupManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (27, N'Salesmen - Region', 34, N'SalesmenRegionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (28, N'Salesmen - Area', 34, N'SalesmenAreaManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (29, N'Salesmen - Local', 34, N'SalesmenLocalManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (30, N'Schedule Promotion', 1000, N'SchedulePromotionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (31, N'Allow Approve', 1, N'AllowApprove.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (32, N'Detail Schedule Promotion', 1000, N'DetailSchedulePromotionManagement.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (33, N'Customer detail', 1000, N'CustomerDetail.aspx')
INSERT [dbo].[Function] ([Id], [FunctionName], [ParentFunctionId], [Action]) VALUES (34, N'Group - Region - Area - Local', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Function] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 04/30/2012 23:50:24 ******/
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
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (7, N'TPR')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (8, N'TPS')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (9, N'TROM')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (10, N'PSR1')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (11, N'PSS1')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (12, N'EROM')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (13, N'PSR2')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (14, N'PSS2')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (15, N'EROM2')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Region]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[Province]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[AssignFunction]    Script Date: 04/30/2012 23:50:23 ******/
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
INSERT [dbo].[AssignFunction] ([Id], [FunctionId], [AdministratorId]) VALUES (52, 34, 1)
SET IDENTITY_INSERT [dbo].[AssignFunction] OFF
/****** Object:  Table [dbo].[SmsObj]    Script Date: 04/30/2012 23:50:24 ******/
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
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (40, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0909838826', CAST(0x0000A02B00000000 AS DateTime), N'Test', N'txtPhoneNumber.Text', 1, 1, 0, 1, 1, 0, 0, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (41, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909838826', N'0909313336', CAST(0x0000A02B00000000 AS DateTime), N'RE: Test', N'gdr grdg sdg sdg sdg sd gsg sd.', 1, 1, 0, 1, 1, 0, 0, 40)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (42, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0909838826', CAST(0x0000A02B00000000 AS DateTime), N'RE: RE: Test', N'fgshf idsh ifu hdsuiof hosd fhod.', 0, 1, 0, 1, 1, 0, 0, 41)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (43, N'e315b471-854c-43df-a082-a8b05dd16121', N'0909313336', N'0909838826', CAST(0x0000A02B00000000 AS DateTime), N'No subject', N'Toi di hoc. Toi di vejgljdlgj dlfj glfdj g;l jfd;g ;lfd g;df g;lfd lg;j fdl; g;lfd gjl;fd gjlfd jgl; fdj;g f;dl glj;fdj g;df jg;lj fdl;g jlfd;j gl;fdj', 0, 1, 0, 1, 1, 0, 0, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (44, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0905253311', CAST(0x0000A02B00000000 AS DateTime), N'FW: RE: Test of forward', N'Doc di ku : g sdg sdg sdg sd gsg sd.', 1, 1, 0, 1, 1, 0, 0, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (47, N'afe4e6ba-8f66-46f9-99b5-291e87ee054f', N'0909313336', N'0909838826', CAST(0x0000A02B00000000 AS DateTime), N'RE : Test', N'sada', 1, 1, 0, 1, 1, 0, 0, 41)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (48, N'1/14/2012 12:00:00 AM12012101132', N'0909313336', N'01223456783', CAST(0x0000A02B00000000 AS DateTime), N'sadsadas', N'dadzXZZ', 1, 1, 0, 1, 1, 2, 2, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (49, N'1/14/2012 12:00:00 AM12012101132', N'0909313336', N'0909313336', CAST(0x0000A02B00000000 AS DateTime), N'RE : sadsadas', N'DASDAASCZ VX', 1, 1, 0, 1, 1, 0, 2, 48)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (50, N'5bd209ce-d90b-4144-af13-7b00ebc2afd5', N'0909313336', N'0987435325', CAST(0x0000A02B00000000 AS DateTime), N'Test of SMS', N'hh hgf hg', 1, 1, 0, 1, 1, 0, 1, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (57, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0909313336', N'0123456789', CAST(0x0000A02D00000000 AS DateTime), N'3333333', N'dddddd', 1, 1, 0, 1, 7, 0, 2, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (58, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0123456789', N'0909313336', CAST(0x0000A02B00000000 AS DateTime), N'RE: 3333333', N'eeeee', 1, 1, 0, 1, 7, 2, 0, 57)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (59, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0123456789', N'0909313336', CAST(0x0000A02B00000000 AS DateTime), N'RE: 3333333', N'', 1, 1, 0, 1, 7, 2, 0, 57)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (60, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0123456789', N'0909313336', CAST(0x0000A02B00000000 AS DateTime), N'RE: 3333333', N'', 1, 1, 0, 1, 7, 2, 0, 57)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (61, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0123456789', N'0909313336', CAST(0x0000A02B00000000 AS DateTime), N'RE: 3333333', N'123', 1, 1, 0, 1, 7, 2, 0, 57)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (62, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0123456789', N'0909313336', CAST(0x0000A02B00000000 AS DateTime), N'RE: 3333333', N'              ', 1, 1, 0, 1, 7, 2, 0, 57)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (63, N'e0814044-3c4b-4ecd-ac5e-ff852d5c711e', N'0909313336', N'0123456789', CAST(0x0000A02B00000000 AS DateTime), N'77777777', N'777777', 1, 0, 0, 1, 8, 0, 2, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (64, N'2eeb51d5-56cb-4a5a-9fdb-885ec5b47e5f', N'0909313336', N'0123456789', CAST(0x0000A02B00000000 AS DateTime), N'fdsfds', N'dvcvxvv v vcx v', 1, 1, 0, 1, 7, 0, 2, 0)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (65, N'2eeb51d5-56cb-4a5a-9fdb-885ec5b47e5f', N'0123456789', N'0909313336', CAST(0x0000A02D0163EB9C AS DateTime), N'RE: fdsfds', N'asdsadasd', 1, 0, 0, 1, 7, 2, 0, 64)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (66, N'2eeb51d5-56cb-4a5a-9fdb-885ec5b47e5f', N'0123456789', N'0909313336', CAST(0x0000A02D0165C5F8 AS DateTime), N'RE: fdsfds', N'AAAAAA', 1, 0, 0, 1, 7, 2, 0, 64)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (67, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0123456789', N'0909313336', CAST(0x0000A02D0165D912 AS DateTime), N'RE: 3333333', N'DDDDD', 1, 0, 0, 1, 7, 2, 0, 57)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (68, N'2eeb51d5-56cb-4a5a-9fdb-885ec5b47e5f', N'0123456789', N'0909313336', CAST(0x0000A02D01730465 AS DateTime), N'RE: fdsfds', N'asdsadad', 1, 0, 0, 1, 7, 2, 0, 64)
INSERT [dbo].[SmsObj] ([Id], [SMSCode], [SenderNumber], [ReceiverNumber], [Date], [Subject], [Content], [IsSendSuccess], [IsRead], [IsDeleted], [SmsTypeId], [PromotionId], [SenderType], [ReceiverType], [ParentSmsId]) VALUES (69, N'3a255e2a-eca6-4f03-9a57-1f9845da05da', N'0123456789', N'0909313336', CAST(0x0000A02D0175B8C2 AS DateTime), N'RE: 3333333', N'asdasdsadsadasd', 1, 1, 0, 1, 7, 2, 0, 57)
SET IDENTITY_INSERT [dbo].[SmsObj] OFF
/****** Object:  Table [dbo].[Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
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
	[SalesmenManagerId] [int] NULL,
 CONSTRAINT [PK_Salesmen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesRegion]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[Area]    Script Date: 04/30/2012 23:50:23 ******/
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
/****** Object:  Table [dbo].[District]    Script Date: 04/30/2012 23:50:23 ******/
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
/****** Object:  Table [dbo].[SalesGroup]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[SalesArea]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[Local]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[SalesLocal]    Script Date: 04/30/2012 23:50:24 ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 04/30/2012 23:50:23 ******/
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
	[LastLoggedDate] [datetime] NULL,
	[UsedSMS] [int] NULL,
	[NoteOfSalesmen] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerSupervisor]    Script Date: 04/30/2012 23:50:23 ******/
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
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (1, N'Dang Vu', N'532/21/25', N'Kinh Duong Vuong', N'Binh Tring Dong B', N'0912356831', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (2, N'aaa', N'abc', N'111', N'123', N'0987654732', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (3, N'aaa bb', N'abc', N'111', N'123', N'0987654733', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (4, N'Dang Vu', N'532/21/25', N'Kinh Duong Vuong', N'Binh Tring Dong B', N'0912356831', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (5, N'aaa', N'abc', N'111', N'123', N'0987654732', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (6, N'aaa bb', N'abc', N'111', N'123', N'0987654733', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (7, N'Dang Vu', N'532/21/25', N'Kinh Duong Vuong', N'Binh Tring Dong B', N'0912356831', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (8, N'aaa', N'abc', N'111', N'123', N'0987654732', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (9, N'aaa bb', N'abc', N'111', N'123', N'0987654733', NULL, NULL, 1)
INSERT [dbo].[CustomerSupervisor] ([Id], [FullName], [Address], [Street], [Ward], [Phone], [CustomerId], [DistrictId], [PositionId]) VALUES (10, N'aaa bb', N'abc', N'111', N'123', N'0987654733', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[CustomerSupervisor] OFF
/****** Object:  Table [dbo].[CustomerLog]    Script Date: 04/30/2012 23:50:23 ******/
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
	[NoteOfSalesmen] [nvarchar](4000) NULL,
 CONSTRAINT [PK_CustomerLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SupervisorManageCustomer]    Script Date: 04/30/2012 23:50:25 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SchedulePromotion]    Script Date: 04/30/2012 23:50:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_SchedulePromotion]
(@Array NVARCHAR(MAX))
as
begin
SELECT a.FullName as CustomerName,a.UpiCode, a.Phone, b.FullName,s.PositionName
  FROM [UPI].[dbo].[Customer] as a 
  LEFT JOIN [UPI].[dbo].[CustomerSupervisor] as b on a.Id=b.CustomerId
  left join [UPI].[dbo].SupervisorPosition as s on b.PositionId=s.Id
  where a.Phone in (@Array)
 end
GO
/****** Object:  ForeignKey [FK_Area_Region]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_Region]
GO
/****** Object:  ForeignKey [FK_AssignFunction_Administrator]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[AssignFunction]  WITH CHECK ADD  CONSTRAINT [FK_AssignFunction_Administrator] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[Administrator] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[AssignFunction] CHECK CONSTRAINT [FK_AssignFunction_Administrator]
GO
/****** Object:  ForeignKey [FK_AssignFunction_Function]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[AssignFunction]  WITH CHECK ADD  CONSTRAINT [FK_AssignFunction_Function] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[Function] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssignFunction] CHECK CONSTRAINT [FK_AssignFunction_Function]
GO
/****** Object:  ForeignKey [FK_Customer_Channel]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Channel] FOREIGN KEY([ChannelId])
REFERENCES [dbo].[Channel] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Channel]
GO
/****** Object:  ForeignKey [FK_Customer_CustomerType]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerType] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[CustomerType] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerType]
GO
/****** Object:  ForeignKey [FK_Customer_Local]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Local] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Local] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Local]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Channel]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Channel] FOREIGN KEY([ChannelId])
REFERENCES [dbo].[Channel] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Channel]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Customer]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerLog_CustomerType]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_CustomerType] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[CustomerType] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_CustomerType]
GO
/****** Object:  ForeignKey [FK_CustomerLog_Local]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerLog]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLog_Local] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Local] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerLog] CHECK CONSTRAINT [FK_CustomerLog_Local]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_Customer]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSupervisor_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerSupervisor] CHECK CONSTRAINT [FK_CustomerSupervisor_Customer]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_District]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSupervisor_District] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerSupervisor] CHECK CONSTRAINT [FK_CustomerSupervisor_District]
GO
/****** Object:  ForeignKey [FK_CustomerSupervisor_SupervisorPosition]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[CustomerSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_CustomerSupervisor_SupervisorPosition] FOREIGN KEY([PositionId])
REFERENCES [dbo].[SupervisorPosition] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CustomerSupervisor] CHECK CONSTRAINT [FK_CustomerSupervisor_SupervisorPosition]
GO
/****** Object:  ForeignKey [FK_District_Province]    Script Date: 04/30/2012 23:50:23 ******/
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_Province] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[Province] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_Province]
GO
/****** Object:  ForeignKey [FK_Local_Area]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Local]  WITH CHECK ADD  CONSTRAINT [FK_Local_Area] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Local] CHECK CONSTRAINT [FK_Local_Area]
GO
/****** Object:  ForeignKey [FK_Promotion_Administrator]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Administrator] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[Administrator] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Administrator]
GO
/****** Object:  ForeignKey [FK_Province_Section]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Province]  WITH CHECK ADD  CONSTRAINT [FK_Province_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Province] CHECK CONSTRAINT [FK_Province_Section]
GO
/****** Object:  ForeignKey [FK_Region_Group]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Region]  WITH CHECK ADD  CONSTRAINT [FK_Region_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Region] CHECK CONSTRAINT [FK_Region_Group]
GO
/****** Object:  ForeignKey [FK_SalesArea_Area]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesArea]  WITH CHECK ADD  CONSTRAINT [FK_SalesArea_Area] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesArea] CHECK CONSTRAINT [FK_SalesArea_Area]
GO
/****** Object:  ForeignKey [FK_SalesArea_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesArea]  WITH CHECK ADD  CONSTRAINT [FK_SalesArea_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesArea] CHECK CONSTRAINT [FK_SalesArea_Salesmen]
GO
/****** Object:  ForeignKey [FK_SalesGroup_Group]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesGroup]  WITH CHECK ADD  CONSTRAINT [FK_SalesGroup_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesGroup] CHECK CONSTRAINT [FK_SalesGroup_Group]
GO
/****** Object:  ForeignKey [FK_SalesGroup_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesGroup]  WITH CHECK ADD  CONSTRAINT [FK_SalesGroup_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesGroup] CHECK CONSTRAINT [FK_SalesGroup_Salesmen]
GO
/****** Object:  ForeignKey [FK_SalesLocal_Local]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesLocal]  WITH CHECK ADD  CONSTRAINT [FK_SalesLocal_Local] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Local] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesLocal] CHECK CONSTRAINT [FK_SalesLocal_Local]
GO
/****** Object:  ForeignKey [FK_SalesLocal_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesLocal]  WITH CHECK ADD  CONSTRAINT [FK_SalesLocal_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesLocal] CHECK CONSTRAINT [FK_SalesLocal_Salesmen]
GO
/****** Object:  ForeignKey [FK_Salesmen_Role]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[Salesmen]  WITH CHECK ADD  CONSTRAINT [FK_Salesmen_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Salesmen] CHECK CONSTRAINT [FK_Salesmen_Role]
GO
/****** Object:  ForeignKey [FK_SalesRegion_Region]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesRegion]  WITH CHECK ADD  CONSTRAINT [FK_SalesRegion_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesRegion] CHECK CONSTRAINT [FK_SalesRegion_Region]
GO
/****** Object:  ForeignKey [FK_SalesRegion_Salesmen]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SalesRegion]  WITH CHECK ADD  CONSTRAINT [FK_SalesRegion_Salesmen] FOREIGN KEY([SalesmenId])
REFERENCES [dbo].[Salesmen] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesRegion] CHECK CONSTRAINT [FK_SalesRegion_Salesmen]
GO
/****** Object:  ForeignKey [FK_SmsObj_SmsType]    Script Date: 04/30/2012 23:50:24 ******/
ALTER TABLE [dbo].[SmsObj]  WITH CHECK ADD  CONSTRAINT [FK_SmsObj_SmsType] FOREIGN KEY([SmsTypeId])
REFERENCES [dbo].[SmsType] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[SmsObj] CHECK CONSTRAINT [FK_SmsObj_SmsType]
GO
/****** Object:  ForeignKey [FK_SupervisorManageCustomer_Customer]    Script Date: 04/30/2012 23:50:25 ******/
ALTER TABLE [dbo].[SupervisorManageCustomer]  WITH CHECK ADD  CONSTRAINT [FK_SupervisorManageCustomer_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupervisorManageCustomer] CHECK CONSTRAINT [FK_SupervisorManageCustomer_Customer]
GO
/****** Object:  ForeignKey [FK_SupervisorManageCustomer_CustomerSupervisor]    Script Date: 04/30/2012 23:50:25 ******/
ALTER TABLE [dbo].[SupervisorManageCustomer]  WITH CHECK ADD  CONSTRAINT [FK_SupervisorManageCustomer_CustomerSupervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[CustomerSupervisor] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupervisorManageCustomer] CHECK CONSTRAINT [FK_SupervisorManageCustomer_CustomerSupervisor]
GO
