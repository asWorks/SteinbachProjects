/*
This script was created by Visual Studio on 03.06.2014 at 06:25.
Run this script on WINDOWS8SSD\SQLSERVEREXPRESS.ProjektDB_Steinbach (WINDOWS8SSD\Noone) to make it the same as WINDOWS8SSD\SQLSERVEREXPRESS.ProjektDB_Steinbach_Dev (WINDOWS8SSD\Noone).
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[AuswahlEintraege] DROP CONSTRAINT [FK_AuswahlEintraege_AuswahlEintraegeGruppen]
ALTER TABLE [dbo].[SI_BelegartenTextbausteine] DROP CONSTRAINT [FK_SI_BelegartenTextbausteine_SI_BelegartenTextbausteine]
ALTER TABLE [dbo].[SI_BelegartenTextbausteine] DROP CONSTRAINT [FK_SI_BelegartenTextbausteine_SI_Textbausteine]
ALTER TABLE [dbo].[SI_BelegeTextbausteine] DROP CONSTRAINT [FK_SI_BelegeTextbausteine_SI_BelegeTextbausteine]
ALTER TABLE [dbo].[SI_BelegeTextbausteine] DROP CONSTRAINT [FK_SI_BelegeTextbausteine_StammTextbausteine]
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen] DROP CONSTRAINT [FK_SI_BelegartenZusatzzeilen_SI_BelegartenZusatzzeilen]
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen] DROP CONSTRAINT [FK_SI_BelegartenZusatzzeilen_StammZusatzzeilen]
SET IDENTITY_INSERT [dbo].[StammZusatzzeilen] ON
INSERT INTO [dbo].[StammZusatzzeilen] ([id], [Typ], [Text], [Wert]) VALUES (1, N'MWST', N'Umsatzsteuer', 19.0000)
INSERT INTO [dbo].[StammZusatzzeilen] ([id], [Typ], [Text], [Wert]) VALUES (2, N'MWST', N'VAT', 19.0000)
SET IDENTITY_INSERT [dbo].[StammZusatzzeilen] OFF
SET IDENTITY_INSERT [dbo].[SI_BelegartenZusatzzeilen] ON
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (3, N're', 1, 1, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (4, N're', 2, 2, 0)
SET IDENTITY_INSERT [dbo].[SI_BelegartenZusatzzeilen] OFF
SET IDENTITY_INSERT [dbo].[StammTextbausteine] ON
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (4, N'Text3', N'Text 3', N'
Ein weiterer Text den man hinzufügen kann.

', 1, 1, '20140511', NULL)
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (7, N'Seagoing vessel ', N'Seagoing vessel', N'Seagoing vessel - no VAT.', 1, 2, '20140513', NULL)
SET IDENTITY_INSERT [dbo].[StammTextbausteine] OFF
SET IDENTITY_INSERT [dbo].[SI_BelegartenTextbausteine] ON
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (5, N'pre', 4, NULL, 0, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (7, N'gs', 4, NULL, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (9, N'ret', 4, NULL, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (10, N'rls', 4, NULL, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (11, N'gs', 2, 1, 0, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (13, N'ls', 2, 1, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (15, N'ls', 1, 1, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (16, N'ls', 4, 1, 0, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (19, N'ls', 7, 2, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (20, N're', 4, 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[SI_BelegartenTextbausteine] OFF
SET IDENTITY_INSERT [dbo].[_Localizing] ON
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (1, 1, 1, N'Header_ArtikelNr', N'Artikel Nr.')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (2, 2, 1, N'Header_ArtikelNr', N'Article No.')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (3, 1, 1, N'Header_ArtikelBez', N'Bezeichnung')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (4, 2, 1, N'Header_ArtikelBez', N'Description')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (5, 1, 1, N'Header_Menge', N'Menge')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (6, 2, 1, N'Header_Menge', N'Quntity')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (7, 1, 1, N'Header_WKZ', N'Währ.')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (8, 2, 1, N'Header_WKZ', N'Cur.')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (9, 1, 1, N'Header_Preis', N'Preis')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (10, 2, 1, N'Header_Preis', N'Price')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (11, 1, 1, N'Header_ZuAbschlag', N'Zu/Abschlag')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (12, 2, 1, N'Header_ZuAbschlag', N'Discount/Markup')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (13, 1, 1, N'Header_Gesamtpreis', N'Gesamt')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (14, 2, 1, N'Header_Gesamtpreis', N'Total')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (15, 1, 1, N'Header_SummePositionen', N'Summe Positionen')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (16, 2, 1, N'Header_SummePositionen', N'Total Positions')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (17, 1, 1, N'Header_SummeBeleg', N'Summe Beleg')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (18, 2, 1, N'Header_SummeBeleg', N'Total')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (19, 1, 2, N'ab', N'Auftragsbestätigung')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (20, 2, 2, N'ab', N'Order Confirmation')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (21, 1, 2, N're', N'Rechnung')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (22, 2, 2, N're', N'Invoice')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (23, 1, 3, N'95', N'Jörg Steinbach
Geschäftsführender Gesellschafter')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (24, 2, 3, N'95', N'Jörg Steinbach
Managing Director')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (25, 1, 3, N'97', N'Ute Kudnik
Verkauf')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (26, 2, 3, N'97', N'Ute Kudnik
Sales')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (27, 1, 2, N'ls', N'Lieferschein')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (28, 2, 2, N'ls', N'Delivery note')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (29, 1, 2, N'an', N'Angebot')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (30, 2, 2, N'an', N'Offer')
SET IDENTITY_INSERT [dbo].[_Localizing] OFF
SET IDENTITY_INSERT [dbo].[config] ON
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (68, NULL, N'HeaderPicture_SI_Beleg', N'H_SI-Brief-Kopf-II.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (69, NULL, N'FooterPicture_SI_Beleg', N'F_SI-Brief-Fuss.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (70, NULL, N'SI_Belege_HeaderFooterPicture_Path', N'F:\ALLGEMEIN\EDV\Datenbank_Neu\BilderBelege\', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (71, NULL, N'HeaderPicture_SI_Handelgesellschaft_Beleg', N'H_SI-Brief-Hndlsgs_Kopf-II.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (72, NULL, N'FooterPicture_SI_Handelgesellschaft_Beleg', N'F_SI-Brief-Hndlsgs_Fuss-I.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (73, NULL, N'HeaderPicture_SI_Brunvoll_Beleg', N'H_SI-Brunvoll-Kopf.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (74, NULL, N'FooterPicture_SI_Brunvoll_Beleg', N'F_SI-Brunvoll-Fuss.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (75, NULL, N'HeaderPicture_Blanko', N'Blanko', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (76, NULL, N'FooterPicture_Blanko', N'Blanko', N'')
SET IDENTITY_INSERT [dbo].[config] OFF
ALTER TABLE [dbo].[AuswahlEintraege]
    WITH NOCHECK ADD CONSTRAINT [FK_AuswahlEintraege_AuswahlEintraegeGruppen] FOREIGN KEY ([id_Gruppe]) REFERENCES [dbo].[AuswahlEintraegeGruppen] ([id])
ALTER TABLE [dbo].[SI_BelegartenTextbausteine]
    ADD CONSTRAINT [FK_SI_BelegartenTextbausteine_SI_BelegartenTextbausteine] FOREIGN KEY ([id_Belegart]) REFERENCES [dbo].[StammBelegarten] ([id])
ALTER TABLE [dbo].[SI_BelegartenTextbausteine]
    ADD CONSTRAINT [FK_SI_BelegartenTextbausteine_SI_Textbausteine] FOREIGN KEY ([id_Textbaustein]) REFERENCES [dbo].[StammTextbausteine] ([id])
ALTER TABLE [dbo].[SI_BelegeTextbausteine]
    ADD CONSTRAINT [FK_SI_BelegeTextbausteine_SI_BelegeTextbausteine] FOREIGN KEY ([id_Beleg]) REFERENCES [dbo].[SI_Belege] ([id])
ALTER TABLE [dbo].[SI_BelegeTextbausteine]
    ADD CONSTRAINT [FK_SI_BelegeTextbausteine_StammTextbausteine] FOREIGN KEY ([id_TextBaustein]) REFERENCES [dbo].[StammTextbausteine] ([id])
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen]
    ADD CONSTRAINT [FK_SI_BelegartenZusatzzeilen_SI_BelegartenZusatzzeilen] FOREIGN KEY ([id_Belegart]) REFERENCES [dbo].[StammBelegarten] ([id])
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen]
    ADD CONSTRAINT [FK_SI_BelegartenZusatzzeilen_StammZusatzzeilen] FOREIGN KEY ([id_Zusartzeile]) REFERENCES [dbo].[StammZusatzzeilen] ([id])
COMMIT TRANSACTION
