USE [baseLocal]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 10/10/2008 17:00:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contracts]') AND type in (N'U'))
DROP TABLE [dbo].[Contracts]