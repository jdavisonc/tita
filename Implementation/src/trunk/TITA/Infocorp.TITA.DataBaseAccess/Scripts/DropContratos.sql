USE [baseLocal]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 09/24/2008 22:08:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contracts]') AND type in (N'U'))
DROP TABLE [dbo].[Contracts]