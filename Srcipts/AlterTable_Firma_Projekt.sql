USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[person]    Script Date: 06/10/2012 13:44:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE dbo.person
 ADD istVerarbeitet smallint
ALTER TABLE dbo.person
 ADD IstGruppe smallint
ALTER TABLE dbo.person
 ADD IstKunde smallint
ALTER TABLE dbo.person
 ADD KdNr int
ALTER TABLE dbo.person
 ADD Kundennummer [nvarchar](20)
 
ALTER TABLE dbo.projekt
 ADD KdNr int
 
GO

