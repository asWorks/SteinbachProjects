USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[person]    Script Date: 06/10/2012 13:44:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE dbo.firma
	ADD [KundenTyp] [nvarchar](30) NULL,
ALTER TABLE dbo.firma
    ADD [Status] [nvarchar](50) NULL,
    ALTER TABLE dbo.firma
    ADD [Typ] [nvarchar](50) NULL,
    ALTER TABLE dbo.firma
    ADD [Webseite] [nvarchar](150) NULL,
    ALTER TABLE dbo.firma
    [Quelle] [nvarchar](50) NULL,
    ALTER TABLE dbo.firma
    [Gebiet] [nvarchar](50) NULL,
    ALTER TABLE dbo.firma
    [AngelegtVon] [int] NULL,
    ALTER TABLE dbo.firma
    [AngelegtAm] [datetime] NULL,
    ALTER TABLE dbo.firma
    [Zahlungsbedingungen] [int] NULL

GO