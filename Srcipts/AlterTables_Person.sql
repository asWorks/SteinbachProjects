USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[person]    Script Date: 06/10/2012 13:44:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE dbo.person
 ADD MailTimerActive smallint
ALTER TABLE dbo.person
 ADD MailTimerInterval bigint
ALTER TABLE dbo.person
 ADD ProjekteAufklappen smallint


GO


