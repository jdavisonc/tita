USE [baseLocal]
GO
/****** Objeto:  Table [dbo].[Contracts]    Fecha de la secuencia de comandos: 09/27/2008 20:00:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contracts]') AND type in (N'U'))
DROP TABLE [dbo].[Contracts]