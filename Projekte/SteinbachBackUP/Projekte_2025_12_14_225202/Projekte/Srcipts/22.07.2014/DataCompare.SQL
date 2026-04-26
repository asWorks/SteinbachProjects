/*
This script was created by Visual Studio on 22.07.2014 at 02:59.
Run this script on WINDOWS8SSD\SQLSERVEREXPRESS.ProjektDB_Steinbach_Kunde (WINDOWS8SSD\Noone) to make it the same as WINDOWS8SSD\SQLSERVEREXPRESS.ProjektDB_Steinbach (WINDOWS8SSD\Noone).
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
ALTER TABLE [dbo].[StammZahlungsfristen] DROP CONSTRAINT [FK_StammZahlungsfristen_StammZahlungsbedingungen]
ALTER TABLE [dbo].[SI_BelegartenTextbausteine] DROP CONSTRAINT [FK_SI_BelegartenTextbausteine_SI_BelegartenTextbausteine]
ALTER TABLE [dbo].[SI_BelegartenBelegUebenahme] DROP CONSTRAINT [FK_BelegartenBelegUebenahme_StammBelegarten]
ALTER TABLE [dbo].[SI_BelegartenBelegUebenahme] DROP CONSTRAINT [FK_BelegartenBelegUebenahme_StammBelegarten1]
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen] DROP CONSTRAINT [FK_SI_BelegartenZusatzzeilen_SI_BelegartenZusatzzeilen]
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen] DROP CONSTRAINT [FK_SI_BelegartenZusatzzeilen_StammZusatzzeilen]
ALTER TABLE [dbo].[AuswahlEintraege] DROP CONSTRAINT [FK_AuswahlEintraege_AuswahlEintraegeGruppen]
DELETE FROM [dbo].[StammTextbausteine] WHERE [id]=1
DELETE FROM [dbo].[StammTextbausteine] WHERE [id]=2
DELETE FROM [dbo].[SI_BelegartenTextbausteine] WHERE [id]=1
DELETE FROM [dbo].[SI_BelegartenTextbausteine] WHERE [id]=2
DELETE FROM [dbo].[SI_BelegartenTextbausteine] WHERE [id]=3
DELETE FROM [dbo].[SI_BelegartenTextbausteine] WHERE [id]=4
UPDATE [dbo].[config] SET [value]=N'1' WHERE [id]=18
UPDATE [dbo].[AuswahlEintraege] SET [ai_string]=N'de-DE' WHERE [id]=190
UPDATE [dbo].[AuswahlEintraege] SET [ai_string]=N'en-US' WHERE [id]=191
SET IDENTITY_INSERT [dbo].[StammZahlungsbedingungen] ON
INSERT INTO [dbo].[StammZahlungsbedingungen] ([id], [Zahlungsbedingung]) VALUES (2, N'Test')
SET IDENTITY_INSERT [dbo].[StammZahlungsbedingungen] OFF
SET IDENTITY_INSERT [dbo].[SI_BelegartenZusatzzeilen] ON
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (3, N're', 1, 1, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (4, N're', 2, 2, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1004, N're', 1, 1001, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1005, N're', 2, 1002, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1006, N'gs', 1, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1007, N'gs', 2, 2, NULL)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1008, N'gs', 1, 1001, NULL)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1009, N'gs', 2, 1002, NULL)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1010, N'an', 1, 1, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1011, N'an', 2, 2, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1012, N'an', 1, 1001, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1013, N'an', 2, 1002, 0)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1014, N'ab', 1, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1015, N'ab', 2, 2, NULL)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1016, N'ab', 1, 1001, NULL)
INSERT INTO [dbo].[SI_BelegartenZusatzzeilen] ([id], [id_Belegart], [id_Sprache], [id_Zusartzeile], [Mandantory]) VALUES (1017, N'ab', 2, 1002, NULL)
SET IDENTITY_INSERT [dbo].[SI_BelegartenZusatzzeilen] OFF
SET IDENTITY_INSERT [dbo].[SI_BelegartenBelegUebenahme] ON
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (2, N'ls', N're', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (3, N'ls', N'rls', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (4, N'ab', N'ls', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (5, N'ab', N'pre', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (6, N'an', N'ab', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (7, N'an', N'ls', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (8, N'an', N'pre', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (9, N'gs', N're', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (11, N'ls', N'pre', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (12, N'ls', N'ls', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (13, N'pre', N're', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (14, N're', N'gs', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (15, N'ret', N'we', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (16, N'rls', N'ls', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (17, N'rls', N'gs', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (18, N'rls', N'rls', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (20, N'we', N'ret', NULL)
INSERT INTO [dbo].[SI_BelegartenBelegUebenahme] ([id], [id_BelegArt], [UebernahmeBelgart], [ColumnName]) VALUES (21, N'ab', N're', NULL)
SET IDENTITY_INSERT [dbo].[SI_BelegartenBelegUebenahme] OFF
SET IDENTITY_INSERT [dbo].[SI_BelegartenTextbausteine] ON
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (1056, N're', 1004, 1, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (1058, N're', 1005, 1, 1, NULL)
INSERT INTO [dbo].[SI_BelegartenTextbausteine] ([id], [id_Belegart], [id_Textbaustein], [id_Sprache], [Mandantory], [index]) VALUES (1059, N're', 1009, 2, 1, NULL)
SET IDENTITY_INSERT [dbo].[SI_BelegartenTextbausteine] OFF
SET IDENTITY_INSERT [dbo].[StammTextbausteine] ON
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (7, N'Seagoing vessel ', N'Seagoing vessel', N'Seagoing vessel - no VAT.', 1, 2, '20140513', NULL)
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (1004, N'Haftungsauschluß ', N'Die enstsprechende Beschreibunb des Ausschlußes', N'
Die Haftung ist ausgeschlossen', 1, 1, '20140616', '20140625')
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (1005, N'Ohne Unterschrift gültig', N'Ohne Unterschrift gültig', N'

Dierser Beleg wurde maschinell erstellt und ist ohne Unterschrift gültig.
', 1, 1, '20140625', NULL)
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (1008, N'Valid without Signature', NULL, N'
This document was generated by computer and is valid without signature.
', 1, 2, '20140716', NULL)
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (1009, N'goods remain our property until payment has been r', NULL, N'The goods remain our property until payment has been received in full, irrespective of their possible resale. 
', 1, 2, '20140716', NULL)
INSERT INTO [dbo].[StammTextbausteine] ([id], [Caption], [Description], [Text], [id_Group], [id_Sprache], [created], [lastmodified]) VALUES (1010, N'Just another text', N'Just another text', N'
Just another text.
', 1, 2, '20140716', NULL)
SET IDENTITY_INSERT [dbo].[StammTextbausteine] OFF
SET IDENTITY_INSERT [dbo].[StammZusatzzeilen] ON
INSERT INTO [dbo].[StammZusatzzeilen] ([id], [Typ], [Text], [Wert]) VALUES (1, N'MWST', N'Mehrwertsteuer', 19.0000)
INSERT INTO [dbo].[StammZusatzzeilen] ([id], [Typ], [Text], [Wert]) VALUES (2, N'MWST', N'VAT', 19.0000)
INSERT INTO [dbo].[StammZusatzzeilen] ([id], [Typ], [Text], [Wert]) VALUES (1001, N'Ustbefreit', N'Umsatzsteuer
Lieferung für ein Seeschiff, daher von
der UST befreit, laut § 4, Nr. 2 i.V. mit § 8 Abs. 1 UStG', 0.0000)
INSERT INTO [dbo].[StammZusatzzeilen] ([id], [Typ], [Text], [Wert]) VALUES (1002, N'Ustbefreit', N'VAT
Seagoing vessel,
no VAT', 0.0000)
SET IDENTITY_INSERT [dbo].[StammZusatzzeilen] OFF
SET IDENTITY_INSERT [dbo].[StammZahlungsfristen] ON
INSERT INTO [dbo].[StammZahlungsfristen] ([id], [id_Zahlungsbedingung], [FristInTagen], [SkontoInProzent]) VALUES (3, 1, NULL, NULL)
INSERT INTO [dbo].[StammZahlungsfristen] ([id], [id_Zahlungsbedingung], [FristInTagen], [SkontoInProzent]) VALUES (4, 1, NULL, NULL)
INSERT INTO [dbo].[StammZahlungsfristen] ([id], [id_Zahlungsbedingung], [FristInTagen], [SkontoInProzent]) VALUES (5, 1, NULL, NULL)
INSERT INTO [dbo].[StammZahlungsfristen] ([id], [id_Zahlungsbedingung], [FristInTagen], [SkontoInProzent]) VALUES (6, 1, NULL, NULL)
INSERT INTO [dbo].[StammZahlungsfristen] ([id], [id_Zahlungsbedingung], [FristInTagen], [SkontoInProzent]) VALUES (7, 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[StammZahlungsfristen] OFF
SET IDENTITY_INSERT [dbo].[config] ON
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (77, NULL, N'HeaderPicture_SI_Beleg', N'H_SI-Brief-Kopf-II.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (78, NULL, N'FooterPicture_SI_Beleg', N'F_SI-Brief-Fuss.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (79, NULL, N'SI_Belege_HeaderFooterPicture_Path', N'F:\ALLGEMEIN\EDV\Datenbank_Neu\BilderBelege\', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (80, NULL, N'HeaderPicture_SI_Handelgesellschaft_Beleg', N'H_SI-Brief-Hndlsgs_Kopf-II.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (81, NULL, N'FooterPicture_SI_Handelgesellschaft_Beleg', N'F_SI-Brief-Hndlsgs_Fuss-I.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (82, NULL, N'HeaderPicture_SI_Brunvoll_Beleg', N'H_SI-Brunvoll-Kopf.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (83, NULL, N'FooterPicture_SI_Brunvoll_Beleg', N'F_SI-Brunvoll-Fuss.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (84, NULL, N'HeaderPicture_Blanko', N'Blanko', N'Wenn Name ==  ''Blanko'' wird kein Bild geladen.')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (85, NULL, N'FooterPicture_Blanko', N'Blanko', N'Wenn Name ==  ''Blanko'' wird kein Bild geladen.')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (86, NULL, N'AlternateHeaderPicture_SI_Beleg', N'H_SI-Brief-Kopf_as.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (87, NULL, N'AlternateFooterPicture_SI_Beleg', N'F_SI-Wanne.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (88, NULL, N'AlternateHeaderPicture_SI_Handelgesellschaft_Beleg', N'H_SI-Brief-Hndlsgs_Kopf-II.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (89, NULL, N'AlternateFooterPicture_SI_Handelgesellschaft_Beleg', N'F_SI-Brief-Hndlsgs_Fuss-I.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (90, NULL, N'AlternateHeaderPicture_SI_Brunvoll_Beleg', N'H_SI-Brunvoll-Kopf_S2.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (91, NULL, N'AlternateFooterPicture_SI_Brunvoll_Beleg', N'F_SI-Wanne.jpg', N'')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (92, NULL, N'AlternateHeaderPicture_Blanko', N'Blanko', N'Wenn Name ==  ''Blanko'' wird kein Bild geladen.')
INSERT INTO [dbo].[config] ([id], [created], [mkey], [value], [Description]) VALUES (93, NULL, N'AlternateFooterPicture_Blanko', N'Blanko', N'Wenn Name ==  ''Blanko'' wird kein Bild geladen.')
SET IDENTITY_INSERT [dbo].[config] OFF
SET IDENTITY_INSERT [dbo].[_Localizing] ON
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (1, 1, 1, N'Header_ArtikelNr', N'Artikel Nr.')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (2, 2, 1, N'Header_ArtikelNr', N'Article No.')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (3, 1, 1, N'Header_ArtikelBez', N'Beschreibung')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (4, 2, 1, N'Header_ArtikelBez', N'Description')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (5, 1, 1, N'Header_Menge', N'Menge')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (6, 2, 1, N'Header_Menge', N'Qua.')
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
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (17, 1, 1, N'Header_SummeBeleg', N'Rechnungsbetrag')
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
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (30, 2, 2, N'an', N'Quote')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (31, 1, 1, N'Header_Unit', N'Einh.')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (32, 2, 1, N'Header_Unit', N'Unit')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (33, 1, 1, N'HeaderPaymentTerms', N'Zahlungsbedingungen :
')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (34, 2, 1, N'HeaderPaymentTerms', N'Payment terms :
')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (35, 1, 4, N'DateFormatString', N'd. MMMM yyyy')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (36, 2, 4, N'DateFormatString', N'MMMM / d / yyyy')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (37, 1, 1, N'Header_VAT', N'UMST ID  :   ')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (38, 2, 1, N'Header_VAT', N'VAT :
')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (39, 1, 2, N'rls', N'Rücklieferschein')
INSERT INTO [dbo].[_Localizing] ([id], [id_Sprache], [Objektname], [Begriffname], [Wert]) VALUES (40, 2, 2, N'rls', N'Return delivery note')
SET IDENTITY_INSERT [dbo].[_Localizing] OFF
ALTER TABLE [dbo].[StammZahlungsfristen]
    ADD CONSTRAINT [FK_StammZahlungsfristen_StammZahlungsbedingungen] FOREIGN KEY ([id_Zahlungsbedingung]) REFERENCES [dbo].[StammZahlungsbedingungen] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[SI_BelegartenTextbausteine]
    ADD CONSTRAINT [FK_SI_BelegartenTextbausteine_SI_BelegartenTextbausteine] FOREIGN KEY ([id_Belegart]) REFERENCES [dbo].[StammBelegarten] ([id])
ALTER TABLE [dbo].[SI_BelegartenBelegUebenahme]
    ADD CONSTRAINT [FK_BelegartenBelegUebenahme_StammBelegarten] FOREIGN KEY ([id_BelegArt]) REFERENCES [dbo].[StammBelegarten] ([id])
ALTER TABLE [dbo].[SI_BelegartenBelegUebenahme]
    ADD CONSTRAINT [FK_BelegartenBelegUebenahme_StammBelegarten1] FOREIGN KEY ([UebernahmeBelgart]) REFERENCES [dbo].[StammBelegarten] ([id])
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen]
    ADD CONSTRAINT [FK_SI_BelegartenZusatzzeilen_SI_BelegartenZusatzzeilen] FOREIGN KEY ([id_Belegart]) REFERENCES [dbo].[StammBelegarten] ([id])
ALTER TABLE [dbo].[SI_BelegartenZusatzzeilen]
    ADD CONSTRAINT [FK_SI_BelegartenZusatzzeilen_StammZusatzzeilen] FOREIGN KEY ([id_Zusartzeile]) REFERENCES [dbo].[StammZusatzzeilen] ([id])
ALTER TABLE [dbo].[AuswahlEintraege]
    WITH NOCHECK ADD CONSTRAINT [FK_AuswahlEintraege_AuswahlEintraegeGruppen] FOREIGN KEY ([id_Gruppe]) REFERENCES [dbo].[AuswahlEintraegeGruppen] ([id])
COMMIT TRANSACTION
