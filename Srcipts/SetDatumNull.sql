use SteinbachNew
update projekt_verlauf set datum = null where datum = '1900-01-01 00:00:00.000'
update projekt_verlauf set created = null where created = '1900-01-01 00:00:00.000'

update projekt set created = null where created = '1900-01-01 00:00:00.000'
update projekt set datum = null where datum = '1900-01-01 00:00:00.000'
update projekt set auftragerhalten = null where auftragerhalten = '1900-01-01 00:00:00.000'
update projekt set auftragsbestaetigung = null where auftragsbestaetigung = '1900-01-01 00:00:00.000'
update projekt set ersatzteileauftragerhalten = null where ersatzteileauftragerhalten = '1900-01-01 00:00:00.000'
update projekt set ersatzteileauftragsbestaetigung = null where ersatzteileauftragsbestaetigung = '1900-01-01 00:00:00.000'
update projekt set ersatzteilebestellt = null where ersatzteilebestellt = '1900-01-01 00:00:00.000'
update projekt set ersatzteileerhalten = null where ersatzteileerhalten = '1900-01-01 00:00:00.000'
update projekt set ersatzteileankunden = null where ersatzteileankunden = '1900-01-01 00:00:00.000'
update projekt set einbauzeichnung = null where einbauzeichnung = '1900-01-01 00:00:00.000'
update projekt set bedienungsanleitung = null where bedienungsanleitung = '1900-01-01 00:00:00.000'
update projekt set  creditnotenummerdatum = null where creditnotenummerdatum = '1900-01-01 00:00:00.000'
update projekt set creditcommissiondatum = null where creditcommissiondatum = '1900-01-01 00:00:00.000'
update projekt set creditinbetriebnahmesummedatum = null where creditinbetriebnahmesummedatum = '1900-01-01 00:00:00.000'
update projekt set creditverkaufsprovisiondatum = null where creditverkaufsprovisiondatum = '1900-01-01 00:00:00.000'
update projekt set creditinfinanzplanung = null where creditinfinanzplanung = '1900-01-01 00:00:00.000'
update projekt set servicetermin = null where servicetermin = '1900-01-01 00:00:00.000'
update projekt set serviceauftragsbestaetigung = null where serviceauftragsbestaetigung = '1900-01-01 00:00:00.000'
update projekt set reklamationauftragsbest = null where reklamationauftragsbest = '1900-01-01 00:00:00.000'

update projekt_anlage_lieferzeit set created = null where created = '1900-01-01 00:00:00.000'
update projekt_anlage_lieferzeit set lieferzeit = null where lieferzeit = '1900-01-01 00:00:00.000'
update projekt_anlage_lieferzeit set storno = null where storno = '1900-01-01 00:00:00.000'
update projekt_anlage_lieferzeit set versandtam = null where versandtam = '1900-01-01 00:00:00.000'

update projekt_anlagentyp set created = null where created = '1900-01-01 00:00:00.000'
update projekt_anlagentyp set angeboterstellt = null where angeboterstellt = '1900-01-01 00:00:00.000'
update projekt_anlagentyp set angebotweitergeleitet = null where angebotweitergeleitet = '1900-01-01 00:00:00.000'

update projekt_ausgang_grechnung set created = null where created = '1900-01-01 00:00:00.000'
update projekt_ausgang_grechnung set datum = null where datum = '1900-01-01 00:00:00.000'
update projekt_ausgang_grechnung set beglichen = null where beglichen = '1900-01-01 00:00:00.000'

update projekt_ausgang_rechnung set created = null where created = '1900-01-01 00:00:00.000'
update projekt_ausgang_rechnung set rgdatum = null where rgdatum = '1900-01-01 00:00:00.000'
update projekt_ausgang_rechnung set faelligam = null where faelligam = '1900-01-01 00:00:00.000'
update projekt_ausgang_rechnung set bezahltam = null where bezahltam = '1900-01-01 00:00:00.000'

update projekt_ersatzteile_gutschrift set created = null where created = '1900-01-01 00:00:00.000'
update projekt_ersatzteile_gutschrift set gutschrift = null where gutschrift = '1900-01-01 00:00:00.000'
update projekt_ersatzteile_gutschrift set faelligam = null where faelligam = '1900-01-01 00:00:00.000'
update projekt_ersatzteile_gutschrift set kommissionerhalten = null where kommissionerhalten = '1900-01-01 00:00:00.000'

update projekt_lieferant_grechnung set created = null where created = '1900-01-01 00:00:00.000'
update projekt_lieferant_grechnung set erhalten = null where erhalten = '1900-01-01 00:00:00.000'

update projekt_lieferant_rechnung set created = null where created = '1900-01-01 00:00:00.000'
update projekt_lieferant_rechnung set faellig = null where faellig = '1900-01-01 00:00:00.000'
update projekt_lieferant_rechnung set bezahlt = null where bezahlt = '1900-01-01 00:00:00.000'

update projekt_rechnung set created = null where created = '1900-01-01 00:00:00.000'
update projekt_rechnung set rechnung = null where rechnung = '1900-01-01 00:00:00.000'
update projekt_rechnung set faelligam = null where faelligam = '1900-01-01 00:00:00.000'
update projekt_rechnung set kommissionerhalten = null where kommissionerhalten = '1900-01-01 00:00:00.000'
update projekt_rechnung set storno = null where storno = '1900-01-01 00:00:00.000'

update projekt_reklamation_rechnung set created = null where created = '1900-01-01 00:00:00.000'
update projekt_reklamation_rechnung set ankunden = null where ankunden = '1900-01-01 00:00:00.000'

update projekt_si_gutschriftkunde set created = null where created = '1900-01-01 00:00:00.000'
update projekt_si_gutschriftkunde set ankunden = null where ankunden = '1900-01-01 00:00:00.000'
update projekt_si_gutschriftkunde set erstellt = null where erstellt = '1900-01-01 00:00:00.000'

update projekt_si_gutschriftlieferant set created = null where created = '1900-01-01 00:00:00.000'
update projekt_si_gutschriftlieferant set erhalten = null where erhalten = '1900-01-01 00:00:00.000'

update projekt_si_rgkunde set created = null where created = '1900-01-01 00:00:00.000'
update projekt_si_rgkunde set rechnungan = null where rechnungan = '1900-01-01 00:00:00.000'
update projekt_si_rgkunde set rechnungfaellig = null where rechnungfaellig = '1900-01-01 00:00:00.000'
update projekt_si_rgkunde set rechnungvom = null where rechnungvom = '1900-01-01 00:00:00.000'

update projekt_si_rglieferant set created = null where created = '1900-01-01 00:00:00.000'
update projekt_si_rglieferant set faellig = null where faellig = '1900-01-01 00:00:00.000'
update projekt_si_rglieferant set lieferantbezahlt = null where lieferantbezahlt = '1900-01-01 00:00:00.000'
update projekt_si_rglieferant set eust = null where eust = '1900-01-01 00:00:00.000'

update lagerliste set created = null where created = '1900-01-01 00:00:00.000'
update lagerliste set preisvom = null where preisvom = '1900-01-01 00:00:00.000'
update lagerliste set hinzugefuegtdatum = null where hinzugefuegtdatum = '1900-01-01 00:00:00.000'

update kalkulationstabelle_detail set created = null where created = '1900-01-01 00:00:00.000'

update brunvoll_fragenkatalog set created = null where created = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set lieferzeit = null where lieferzeit = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set motorbezeichnunganbr = null where motorbezeichnunganbr = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set motorlieferdatum = null where motorlieferdatum = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set motorbestanlieferanten = null where motorbestanlieferanten = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set motorabdurchlieferanten = null where motorabdurchlieferanten = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set motorzertifikat = null where motorzertifikat = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set motorlieferungankunden = null where motorlieferungankunden = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set farbspecerhaltenvonkunden = null where farbspecerhaltenvonkunden = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set farbspecweitergeleitetanbr = null where farbspecweitergeleitetanbr = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set einbauzeichnung = null where einbauzeichnung = '1900-01-01 00:00:00.000'
update brunvoll_fragenkatalog set bedienungsanleitung = null where bedienungsanleitung = '1900-01-01 00:00:00.000'



