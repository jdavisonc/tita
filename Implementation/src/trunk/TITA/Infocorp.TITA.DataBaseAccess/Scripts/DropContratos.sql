USE [baseLocal]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 11/05/2008 21:40:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contracts]') AND type in (N'U'))
DROP TABLE [dbo].[Contracts]