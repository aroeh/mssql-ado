USE [Samples]
GO

/****** Object:  Table [dbo].[RestuarantLocation]    Script Date: 8/17/2024 1:04:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RestuarantLocation]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RestuarantId] [int] NOT NULL,
	[Street] [nvarchar](200) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[State] [char](2) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](10) NOT NULL,
	CONSTRAINT [PK_RestuarantLocation] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	CONSTRAINT [FK_Restuarant_Id] FOREIGN KEY ([RestuarantId]) REFERENCES [dbo].[Restuarants]([Id])
) ON [PRIMARY]
GO


