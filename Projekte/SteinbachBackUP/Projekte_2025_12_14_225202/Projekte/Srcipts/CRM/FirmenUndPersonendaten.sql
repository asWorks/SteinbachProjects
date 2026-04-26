USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[AuswahlEintraege]    Script Date: 12/15/2012 17:24:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuswahlEintraege](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Gruppe] [nvarchar](15) NULL,
	[Eintrag] [nvarchar](50) NULL,
 CONSTRAINT [PK_AuswahlEintraege] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Personen_Telefon]    Script Date: 12/15/2012 17:24:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Personen_Telefon](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Person] [int] NULL,
	[Typ] [nvarchar](25) NULL,
	[Vorwahl] [nvarchar](15) NULL,
	[Rufnummer] [nvarchar](25) NULL,
	[Memo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Kunden_Telefon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Personen_Telefon]  WITH CHECK ADD  CONSTRAINT [FK_Personen_Telefon_Firmen_Personen] FOREIGN KEY([id_Person])
REFERENCES [dbo].[Firmen_Personen] ([id])
GO

ALTER TABLE [dbo].[Personen_Telefon] CHECK CONSTRAINT [FK_Personen_Telefon_Firmen_Personen]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Personen_Mailadressen]    Script Date: 12/15/2012 17:24:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Personen_Mailadressen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Person] [int] NULL,
	[Typ] [nvarchar](25) NULL,
	[Mailadresse] [nchar](50) NULL,
	[Memo] [nchar](50) NULL,
 CONSTRAINT [PK_Personen_Mailadressen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Personen_Mailadressen]  WITH CHECK ADD  CONSTRAINT [FK_Personen_Mailadressen_Firmen_Personen] FOREIGN KEY([id_Person])
REFERENCES [dbo].[Firmen_Personen] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Personen_Mailadressen] CHECK CONSTRAINT [FK_Personen_Mailadressen_Firmen_Personen]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Firmen_Personen]    Script Date: 12/15/2012 17:23:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Firmen_Personen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Firma] [int] NULL,
	[Nachname] [nvarchar](50) NULL,
	[Vorname] [nvarchar](50) NULL,
	[Vorname2] [nvarchar](50) NULL,
	[Namenszusatz] [nvarchar](50) NULL,
	[Titel] [nvarchar](50) NULL,
	[Funktion] [nvarchar](50) NULL,
	[Abteilung] [nvarchar](50) NULL,
	[Gebiet] [nvarchar](50) NULL,
	[Anrede] [nvarchar](15) NULL,
	[Newsletter] [smallint] NULL,
	[Messeeinladung] [smallint] NULL,
	[Weihnachtskarte] [smallint] NULL,
	[Betreuer] [nvarchar](50) NULL,
	[Typ] [nvarchar](50) NULL,
 CONSTRAINT [PK_Personen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Firmen_Personen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_Personen_firma] FOREIGN KEY([id_Firma])
REFERENCES [dbo].[firma] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Firmen_Personen] CHECK CONSTRAINT [FK_Kunden_Personen_firma]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Firmen_Telefon]    Script Date: 12/15/2012 17:23:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Firmen_Telefon](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Firma] [int] NULL,
	[Typ] [nvarchar](25) NULL,
	[Vorwahl] [nvarchar](15) NULL,
	[Rufnummer] [nvarchar](20) NULL,
	[Memo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Firmen_Telefon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Firmen_Telefon]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Telefon_Firmen_Telefon] FOREIGN KEY([id_Firma])
REFERENCES [dbo].[firma] ([id])
GO

ALTER TABLE [dbo].[Firmen_Telefon] CHECK CONSTRAINT [FK_Firmen_Telefon_Firmen_Telefon]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Firmen_Mailadressen]    Script Date: 12/15/2012 17:23:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Firmen_Mailadressen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Firma] [int] NULL,
	[Typ] [nvarchar](25) NULL,
	[Mailadresse] [nvarchar](50) NULL,
	[Memo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Firmen_Mailadressen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Firmen_Mailadressen]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Mailadressen_Firmen_Mailadressen] FOREIGN KEY([id_Firma])
REFERENCES [dbo].[firma] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Firmen_Mailadressen] CHECK CONSTRAINT [FK_Firmen_Mailadressen_Firmen_Mailadressen]
GO

USE [ProjektDB_Steinbach]
GO

/****** Object:  Table [dbo].[Firmen_Adressen]    Script Date: 12/15/2012 17:22:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Firmen_Adressen](
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
	[PostfachPLZ] [nvarchar](10) NULL,
	[PostfachOrt] [nvarchar](50) NULL,
	[TypGeschaeftlich] [smallint] NULL,
	[TypRechnungsadresse] [smallint] NULL,
	[TypLieferadresse] [smallint] NULL,
	[Hauptadresse] [smallint] NULL,
 CONSTRAINT [PK_Adressen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Firmen_Adressen]  WITH CHECK ADD  CONSTRAINT [FK_Kunden_Adressen_firma] FOREIGN KEY([id_Firma])
REFERENCES [dbo].[firma] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Firmen_Adressen] CHECK CONSTRAINT [FK_Kunden_Adressen_firma]
GO

ALTER TABLE [dbo].[Firmen_Adressen] ADD  CONSTRAINT [DF_Firmen_Adressen_Hauptadresse]  DEFAULT ((0)) FOR [Hauptadresse]
GO