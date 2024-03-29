USE [baseLocal]
GO
/****** Object:  Table [dbo].[Current]    Script Date: 11/02/2008 20:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Current](
	[site] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[current_user] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[logged_date] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[last_modification] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Current_1] PRIMARY KEY CLUSTERED 
(
	[site] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
