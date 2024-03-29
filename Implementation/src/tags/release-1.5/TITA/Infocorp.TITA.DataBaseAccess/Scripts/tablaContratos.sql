USE [baseLocal]
GO
/****** Objeto:  Table [dbo].[Contracts]    Fecha de la secuencia de comandos: 09/27/2008 19:45:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[id_contract] [int] IDENTITY(1,1) NOT NULL,
	[site] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[user] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[id_contract] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
