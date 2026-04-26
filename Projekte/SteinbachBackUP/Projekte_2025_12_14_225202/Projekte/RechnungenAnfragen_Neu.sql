USE [ProjektDB_SteinbachOrg]
GO

/****** Object:  Table [dbo].[projekt_RechnungKunde]    Script Date: 04/04/2013 13:56:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[projekt_RechnungKunde](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NULL,
	[id_projekt] [int] NULL,
	[idx] [int] NULL,
	[rechnungsdatum] [datetime] NULL,
	[kundenname] [nvarchar](64) NULL,
	[rechnungsnummer] [nvarchar](24) NULL,
	[rechnungfaellig] [datetime] NULL,
	[rechnungvom] [datetime] NULL,
	[rechnungbezahlt] [datetime] NULL,
	[id_personchange] [int] NULL,
	[hassend] [smallint] NULL,
	[emailbezahlt] [smallint] NULL,
	[rechnungsbetrag] [money] NULL,
	[rechnungseingang] [money] NULL,
	[mwstbetrag] [money] NULL,
	[wkz] [nvarchar](10) NULL,
 CONSTRAINT [PK_projekt_RechnungKunde] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[projekt_RechnungKunde]  WITH NOCHECK ADD  CONSTRAINT [FK_projekt_RechnungKunde] FOREIGN KEY([id_projekt])
REFERENCES [dbo].[projekt] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[projekt_RechnungKunde] CHECK CONSTRAINT [FK_projekt_RechnungKunde]
GO

ALTER TABLE [dbo].[projekt_RechnungKunde] ADD  CONSTRAINT [DF_projekt_RechnungKunde_rechnungsbetrag]  DEFAULT ((0)) FOR [rechnungsbetrag]
GO




/****** Object:  Table [dbo].[projekt_gutschriftkunde]    Script Date: 04/04/2013 14:04:12 ******/


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[projekt_gutschriftkunde](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NULL,
	[id_projekt] [int] NULL,
	[idx] [int] NULL,
	[gutschriftdatum] [datetime] NULL,
	[kundenname] [nvarchar](64) NULL,
	[gutschriftnummer] [nvarchar](24) NULL,
	[gutschriftbeglichen] [datetime] NULL,
	[gutschriftbetrag] [money] NULL,
	[mwstbetrag] [money] NULL,
	[wkz] [nvarchar](10) NULL,
 CONSTRAINT [PK_projekt_gutschriftkunde] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[projekt_gutschriftkunde]  WITH NOCHECK ADD  CONSTRAINT [FK_projekt_gutschriftkunde] FOREIGN KEY([id_projekt])
REFERENCES [dbo].[projekt] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[projekt_gutschriftkunde] CHECK CONSTRAINT [FK_projekt_gutschriftkunde]
GO

ALTER TABLE [dbo].[projekt_gutschriftkunde] ADD  CONSTRAINT [DF_projekt_gutschriftkunde_rechnungsbetrag]  DEFAULT ((0)) FOR [gutschriftbetrag]
GO




/****** Object:  Table [dbo].[projekt_rechnunglieferant]    Script Date: 04/04/2013 14:06:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[projekt_rechnunglieferant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NULL,
	[id_projekt] [int] NULL,
	[idx] [int] NULL,
	[rechnungsnummer] [nvarchar](32) NULL,
	[lieferantname] [nvarchar](32) NULL,
	[rechnungfaellig] [datetime] NULL,
	[rechnungbezahlt] [datetime] NULL,
	[eust] [datetime] NULL,
	[emailbezahlt] [smallint] NULL,
	[rechnungsdatum] [datetime] NULL,
	[rechnungseingang] [datetime] NULL,
	[rechnungsbetrag] [money] NULL,
	[mwstbetrag] [money] NULL,
	[eustbetrag] [money] NULL,
	[gebuehr] [money] NULL,
	[mwstaufgebuehr] [money] NULL,
	[zollgebuehren] [money] NULL,
	[wkz] [nvarchar](10) NULL,
 CONSTRAINT [PK_projekt_rechnunglieferant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[projekt_rechnunglieferant]  WITH NOCHECK ADD  CONSTRAINT [FK_projekt_rechnunglieferant] FOREIGN KEY([id_projekt])
REFERENCES [dbo].[projekt] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[projekt_rechnunglieferant] CHECK CONSTRAINT [FK_projekt_rechnunglieferant]
GO




/****** Object:  Table [dbo].[projekt_gutschriftlieferant]    Script Date: 04/04/2013 14:07:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[projekt_gutschriftlieferant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NULL,
	[id_projekt] [int] NULL,
	[idx] [int] NULL,
	[gutschriftnummer] [nvarchar](32) NULL,
	[lieferantname] [nvarchar](24) NULL,
	[gutschrifterhalten] [datetime] NULL,
	[gutschriftbetrag] [money] NULL,
	[mwstbetrag] [money] NULL,
	[wkz] [nvarchar](10) NULL,
	[gutschriftdatum] [datetime] NULL,
 CONSTRAINT [PK_projekt_gutschriftlieferant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[projekt_gutschriftlieferant]  WITH NOCHECK ADD  CONSTRAINT [FK_projekt_gutschriftlieferant] FOREIGN KEY([id_projekt])
REFERENCES [dbo].[projekt] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[projekt_gutschriftlieferant] CHECK CONSTRAINT [FK_projekt_gutschriftlieferant]
GO

ALTER TABLE [dbo].[projekt_gutschriftlieferant] ADD  CONSTRAINT [DF_projekt_gutschriftlieferant_rechnungsbetrag]  DEFAULT ((0)) FOR [gutschriftbetrag]
GO




USE [ProjektDB_SteinbachOrg]
GO

/****** Object:  Table [dbo].[Firmen_Kundenbesuche]    Script Date: 04/17/2013 03:25:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Firmen_Kundenbesuche](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_firma] [int] NULL,
	[id_kontakt] [int] NULL,
	[kfzkennzeichen] [nvarchar](25) NULL,
	[kmgefahren] [money] NULL,
	[position] [nvarchar](50) NULL,
	[datum_von] [datetime] NULL,
	[datum_bis] [datetime] NULL,
	[id_siperson] [int] NULL,
	[id_vertretenefirma] [int] NULL,
	[produkte] [nvarchar](255) NULL,
	[kurzbericht] [nvarchar](max) NULL,
	[besuchsthema] [nvarchar](255) NULL,
	[todo] [nvarchar](max) NULL,
	[projektnummer] [nvarchar](16) NULL,
	[id_projekt] [int] NULL,
 CONSTRAINT [PK_Firmen_Kundenbesuche] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Kundenbesuche_firmakunde] FOREIGN KEY([id_firma])
REFERENCES [dbo].[firma] ([id])
GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche] CHECK CONSTRAINT [FK_Firmen_Kundenbesuche_firmakunde]
GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Kundenbesuche_Kontakt] FOREIGN KEY([id_kontakt])
REFERENCES [dbo].[Firmen_Personen] ([id])
GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche] CHECK CONSTRAINT [FK_Firmen_Kundenbesuche_Kontakt]
GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Kundenbesuche_siperson] FOREIGN KEY([id_siperson])
REFERENCES [dbo].[person] ([id])
GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche] CHECK CONSTRAINT [FK_Firmen_Kundenbesuche_siperson]
GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Kundenbesuche_vertretenefirma] FOREIGN KEY([id_vertretenefirma])
REFERENCES [dbo].[firma] ([id])
GO

ALTER TABLE [dbo].[Firmen_Kundenbesuche] CHECK CONSTRAINT [FK_Firmen_Kundenbesuche_vertretenefirma]
GO








USE [ProjektDB_SteinbachOrg]
GO

/****** Object:  Table [dbo].[Kundenbesuche_TeilnehmerExtern]    Script Date: 04/17/2013 03:25:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kundenbesuche_TeilnehmerExtern](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_besuch] [int] NULL,
	[id_TeilnehmerExtern] [int] NULL,
 CONSTRAINT [PK_Kundenbesuche_TeilnehmerExtern] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Kundenbesuche_TeilnehmerExtern]  WITH CHECK ADD  CONSTRAINT [FK_Kundenbesuche_TeilnehmerExtern_Firmen_Kundenbesuche] FOREIGN KEY([id_besuch])
REFERENCES [dbo].[Firmen_Kundenbesuche] ([id])
GO

ALTER TABLE [dbo].[Kundenbesuche_TeilnehmerExtern] CHECK CONSTRAINT [FK_Kundenbesuche_TeilnehmerExtern_Firmen_Kundenbesuche]
GO

ALTER TABLE [dbo].[Kundenbesuche_TeilnehmerExtern]  WITH CHECK ADD  CONSTRAINT [FK_Kundenbesuche_TeilnehmerExtern_Firmen_Personen] FOREIGN KEY([id_TeilnehmerExtern])
REFERENCES [dbo].[Firmen_Personen] ([id])
GO

ALTER TABLE [dbo].[Kundenbesuche_TeilnehmerExtern] CHECK CONSTRAINT [FK_Kundenbesuche_TeilnehmerExtern_Firmen_Personen]
GO