USE [baseLocal]
GO
/****** Object:  Table [dbo].[Current]    Script Date: 10/19/2008 12:02:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Current]') AND type in (N'U'))
DROP TABLE [dbo].[Current]