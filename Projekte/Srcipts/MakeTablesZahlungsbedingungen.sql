USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[StammZahlungsbedingungen]    Script Date: 07/12/2012 22:19:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StammZahlungsbedingungen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Zahlungsbedingung] [nvarchar](150) NULL,
 CONSTRAINT [PK_StammZahlungsZiele] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[StammZahlungsfristen]    Script Date: 07/12/2012 22:19:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StammZahlungsfristen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Zahlungsbedingung] [int] NULL,
	[FristInTagen] [int] NULL,
	[SkontoInProzent] [float] NULL,
 CONSTRAINT [PK_StammZahlungsfristen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StammZahlungsfristen]  WITH CHECK ADD  CONSTRAINT [FK_StammZahlungsfristen_StammZahlungsbedingungen] FOREIGN KEY([id_Zahlungsbedingung])
REFERENCES [dbo].[StammZahlungsbedingungen] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StammZahlungsfristen] CHECK CONSTRAINT [FK_StammZahlungsfristen_StammZahlungsbedingungen]
GO
