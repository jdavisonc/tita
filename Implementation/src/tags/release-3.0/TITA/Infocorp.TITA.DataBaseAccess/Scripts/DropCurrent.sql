USE [baseLocal]
GO
/****** Object:  Table [dbo].[Current]    Script Date: 11/02/2008 20:30:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Current]') AND type in (N'U'))
DROP TABLE [dbo].[Current]