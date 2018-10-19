USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[StammWaehrungen]    Script Date: 06/15/2012 01:49:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StammWaehrungen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[WKZ] [nvarchar](10) NOT NULL,
	[Waehrung] [nvarchar](50) NULL,
	[Symbol] [nvarchar](10) NULL,
	[Kurs] [money] NULL,
 CONSTRAINT [PK_StammWaehrungen_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


