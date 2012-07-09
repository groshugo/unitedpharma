--USE [UPI]
GO

/****** Object:  Table [dbo].[PromotionSearchHistory]    Script Date: 07/08/2012 23:11:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PromotionSearchHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[PromotionId] [int] NOT NULL,
	[SearchCriteria] [nvarchar](4000) NOT NULL,
	[SearchCriteriaLiteral] [nvarchar](4000) NOT NULL,
	[SearchResults] [nvarchar](4000) NULL,
	[CreatedDate] [dbo].[udtCreatedDateTime] NULL,
 CONSTRAINT [PK_PromotionSearchHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PromotionSearchHistory]  WITH CHECK ADD  CONSTRAINT [FK_PromotionSearchHistory_Promotion] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[SchedulePromotion] ([Id])
GO

ALTER TABLE [dbo].[PromotionSearchHistory] CHECK CONSTRAINT [FK_PromotionSearchHistory_Promotion]
GO


