INSERT INTO [ProjektDB_Steinbach].[dbo].[config]
           ([created]
           ,[mkey]
           ,[value]
           ,[Description])
     VALUES
		(NULL,	'EMailZahlungFaellig',	'me@asWorks.de',	NULL),
		(NULL,	'EMailZahlungErfolgt',	'me@asWorks.de',	NULL),
		(NULL,	'NegativeLagerbestaende',	'0',	NULL),
		(NULL,	'EMailZahlungFaelligSI',	'e.steinbach@si-technik.de',	NULL),
		(NULL,	'EMailZahlungErfolgtSI',	'a.seider-manske@si-technik.de',	NULL),
		(NULL,	'LogState',	'4',	NULL)
	GO