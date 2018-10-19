USE [ProjektDB_SteinbachNeu]
GO

/****** Object:  Table [dbo].[Firmen_Kategorien]    Script Date: 01/09/2013 10:31:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Firmen_Kategorien](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Firma] [int] NULL,
	[id_Kategorie] [int] NULL,
	[Kategoriename] [nvarchar](50) NULL,
 CONSTRAINT [PK_Firmen_Kategorien] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Firmen_Kategorien]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Kategorien_firma] FOREIGN KEY([id_Firma])
REFERENCES [dbo].[firma] ([id])
GO

ALTER TABLE [dbo].[Firmen_Kategorien] CHECK CONSTRAINT [FK_Firmen_Kategorien_firma]
GO

ALTER TABLE [dbo].[Firmen_Kategorien]  WITH CHECK ADD  CONSTRAINT [FK_Firmen_Kategorien_Kategorien] FOREIGN KEY([id_Kategorie])
REFERENCES [dbo].[firma] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Firmen_Kategorien] CHECK CONSTRAINT [FK_Firmen_Kategorien_Kategorien]
GO


