USE [UPI]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CustomerLog_Salesmen]') AND parent_object_id = OBJECT_ID(N'[dbo].[CustomerLog]'))
ALTER TABLE [dbo].[CustomerLog] DROP CONSTRAINT [FK_CustomerLog_Salesmen]
GO


