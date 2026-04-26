USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Kunden_Adressen]    Script Date: 10/16/2012 21:39:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kunden_Adressen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Firma] [int] NULL,
	[Straﬂe] [nvarchar](50) NULL,
	[PLZ] [nvarchar](10) NULL,
	[Ort] [nvarchar](50) NULL,
	[Postfach] [nvarchar](50) NULL,
	[Bundesland] [nvarchar](50) NULL,
	[Land] [nvarchar](50) NULL,
	[Bezeichnung] [nvarchar](50) NULL,
	[Typ] [int] NULL,
 CONSTRAINT [PK_Adressen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Kunden_FirmenLink_Personen]    Script Date: 10/16/2012 21:39:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kunden_FirmenLink_Personen](
	[id_Person] [int] NOT NULL,
	[id_Firma] [int] NOT NULL,
 CONSTRAINT [PK_Kunden_FirmenLink_Personen] PRIMARY KEY CLUSTERED 
(
	[id_Person] ASC,
	[id_Firma] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Kunden_FirmenLink_Personen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_FirmenLink_Personen_firma] FOREIGN KEY([id_Firma])
REFERENCES [dbo].[firma] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Kunden_FirmenLink_Personen] CHECK CONSTRAINT [FK_Kunden_FirmenLink_Personen_firma]
GO

ALTER TABLE [dbo].[Kunden_FirmenLink_Personen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_FirmenLink_Personen_person] FOREIGN KEY([id_Person])
REFERENCES [dbo].[Kunden_Personen] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Kunden_FirmenLink_Personen] CHECK CONSTRAINT [FK_Kunden_FirmenLink_Personen_person]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Kunden_Personen]    Script Date: 10/16/2012 21:39:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kunden_Personen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nachname] [nvarchar](50) NULL,
	[Vorname] [nvarchar](50) NULL,
	[Vorname2] [nvarchar](50) NULL,
	[Namenszusatz] [nvarchar](50) NULL,
	[Titel] [nvarchar](50) NULL,
	[Funktion] [nvarchar](50) NULL,
	[Abteilung] [nvarchar](50) NULL,
	[Gebiet] [nvarchar](50) NULL,
	[Anrede] [nvarchar](15) NULL,
 CONSTRAINT [PK_Personen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Kunden_PhoneLink_Firmen]    Script Date: 10/16/2012 21:40:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kunden_PhoneLink_Firmen](
	[id_Phone] [int] NOT NULL,
	[id_Firma] [int] NOT NULL,
 CONSTRAINT [PK_Kunden_PhoneLink_Firmen] PRIMARY KEY CLUSTERED 
(
	[id_Phone] ASC,
	[id_Firma] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Firmen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_PhoneLink_Firmen_firma] FOREIGN KEY([id_Firma])
REFERENCES [dbo].[firma] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Firmen] CHECK CONSTRAINT [FK_Kunden_PhoneLink_Firmen_firma]
GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Firmen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_PhoneLink_Firmen_Kunden_Telefon] FOREIGN KEY([id_Phone])
REFERENCES [dbo].[Kunden_Telefon] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Firmen] CHECK CONSTRAINT [FK_Kunden_PhoneLink_Firmen_Kunden_Telefon]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Kunden_PhoneLink_Personen]    Script Date: 10/16/2012 21:40:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kunden_PhoneLink_Personen](
	[id_Person] [int] NOT NULL,
	[id_Phone] [int] NOT NULL,
 CONSTRAINT [PK_Kunden_PhoneLink] PRIMARY KEY CLUSTERED 
(
	[id_Person] ASC,
	[id_Phone] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Personen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_PhoneLink_Kunden_Personen] FOREIGN KEY([id_Person])
REFERENCES [dbo].[Kunden_Personen] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Personen] CHECK CONSTRAINT [FK_Kunden_PhoneLink_Kunden_Personen]
GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Personen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_PhoneLink_Kunden_Telefon] FOREIGN KEY([id_Phone])
REFERENCES [dbo].[Kunden_Telefon] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Kunden_PhoneLink_Personen] CHECK CONSTRAINT [FK_Kunden_PhoneLink_Kunden_Telefon]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Kunden_Telefon]    Script Date: 10/16/2012 21:41:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kunden_Telefon](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Bezeichnung] [nvarchar](50) NULL,
	[Vorwahl] [nvarchar](15) NULL,
	[Telefonnummer] [nchar](25) NULL,
 CONSTRAINT [PK_Kunden_Telefon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO