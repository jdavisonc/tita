USE [baseLocal]
GO
/****** Object:  Table [dbo].[Current]    Script Date: 10/19/2008 12:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Current](
	[site] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[current_user] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[logged_date] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[last_modification] [nchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Current] PRIMARY KEY CLUSTERED 
(
	[site] ASC,
	[current_user] ASC,
	[logged_date] ASC,
	[last_modification] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
