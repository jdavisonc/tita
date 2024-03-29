USE [baseLocal]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 11/05/2008 21:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[id_contract] [int] IDENTITY(1,1) NOT NULL,
	[site] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[issues_list] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[workpackage_list] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[task_list] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[id_contract] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
