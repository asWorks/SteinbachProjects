using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C1.WPF.DataGrid;
using System.Windows.Data;
using ProjektDB;


namespace ProjektDB.Tools
{

    class ColumnDescription
    {

        static bool DoUseConverter = true;

        public static List<ColumnDescription> ColumnList { get; set; }
        public string header { get; set; }
        public string binding { get; set; }
        public int width { get; set; }
        public bool WidthAuto { get; set; }
        public bool SetFilterPath { get; set; }
        public bool SetSortPath { get; set; }
        public string Type { get; set; }
        public Binding oBinding { get; set; }
        public enumType TypeEnum { get; set; }
        public IValueConverter Converter { get; set; }
        public string BackGroundBinding { get; set; }
        public enumDataType DataType { get; set; }
        public string FiltermemberPath { get; set; }
        public string SortmemberPath { get; set; }



        private ColumnDescription(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                    bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter)
        {
            header = ColumnHeader;
            binding = ColumnBinding;
            width = ColumnWidth;
            WidthAuto = widthAuto;
            SetFilterPath = setFilterPath;
            SetSortPath = setSortPath;
            TypeEnum = type;
            Converter = converter;
            DataType = enumDataType.String;
        }

        private ColumnDescription(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                 bool setFilterPath, bool setSortPath, enumType type, enumDataType dataType)
        {
            header = ColumnHeader;
            binding = ColumnBinding;
            width = ColumnWidth;
            WidthAuto = widthAuto;
            SetFilterPath = setFilterPath;
            SetSortPath = setSortPath;
            TypeEnum = type;
            Converter = null;
            DataType = dataType;
        }




        private ColumnDescription(string ColumnHeader, Binding oBinding, int ColumnWidth, bool widthAuto,
            bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter)
        {
            header = ColumnHeader;
            this.oBinding = oBinding;
            width = ColumnWidth;
            WidthAuto = widthAuto;
            SetFilterPath = setFilterPath;
            SetSortPath = setSortPath;
            TypeEnum = type;
            Converter = converter;
            DataType = enumDataType.String;

        }


        private ColumnDescription(string ColumnHeader, Binding oBinding, int ColumnWidth, bool widthAuto,
         bool setFilterPath, bool setSortPath, enumType type, enumDataType dataType)
        {
            header = ColumnHeader;
            this.oBinding = oBinding;
            width = ColumnWidth;
            WidthAuto = widthAuto;
            SetFilterPath = setFilterPath;
            SetSortPath = setSortPath;
            TypeEnum = type;
            Converter = null;
            DataType = dataType;

        }





        private ColumnDescription(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                 bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter, string backGroundBinding)
        {
            header = ColumnHeader;
            binding = ColumnBinding;
            width = ColumnWidth;
            WidthAuto = widthAuto;
            SetFilterPath = setFilterPath;
            SetSortPath = setSortPath;
            TypeEnum = type;
            Converter = converter;
            DataType = enumDataType.String;
            BackGroundBinding = backGroundBinding;

        }

        private ColumnDescription(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                              bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter, string backGroundBinding, enumDataType dataType)
        {
            header = ColumnHeader;
            binding = ColumnBinding;
            width = ColumnWidth;
            WidthAuto = widthAuto;
            SetFilterPath = setFilterPath;
            SetSortPath = setSortPath;
            TypeEnum = type;
            Converter = converter;
            DataType = dataType;
            BackGroundBinding = backGroundBinding;

        }


        private ColumnDescription(string ColumnHeader, Binding oBinding, int ColumnWidth, bool widthAuto,
                           bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter, string backGroundBinding, enumDataType dataType)
        {
            header = ColumnHeader;
            this.oBinding = oBinding;
            width = ColumnWidth;
            WidthAuto = widthAuto;
            SetFilterPath = setFilterPath;
            SetSortPath = setSortPath;
            TypeEnum = type;
            Converter = converter;
            DataType = dataType;
            BackGroundBinding = backGroundBinding;

        }




        public static void AddColumn(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                        bool setFilterPath, bool setSortPath, enumType type)
        {
            AddColumn(ColumnHeader, ColumnBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, null);

        }


        public static void AddColumn(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                        bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter)
        {
            if (ColumnList == null)
                ColumnList = new List<ColumnDescription>();
            var cd = new ColumnDescription(ColumnHeader, ColumnBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, converter);

            ColumnList.Add(cd);
        }

        public static void AddColumn(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                    bool setFilterPath, bool setSortPath, enumType type, enumDataType dataType)
        {
            if (ColumnList == null)
                ColumnList = new List<ColumnDescription>();
            var cd = new ColumnDescription(ColumnHeader, ColumnBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, dataType);

            ColumnList.Add(cd);
        }



        public static void AddColumn(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                     bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter, string backGroundBinding)
        {
            if (ColumnList == null)
                ColumnList = new List<ColumnDescription>();
            var cd = new ColumnDescription(ColumnHeader, ColumnBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, converter, backGroundBinding);

            ColumnList.Add(cd);
        }


        public static void AddColumn(string ColumnHeader, string ColumnBinding, int ColumnWidth, bool widthAuto,
                                  bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter, string backGroundBinding, enumDataType dataType)
        {
            if (ColumnList == null)
                ColumnList = new List<ColumnDescription>();
            var cd = new ColumnDescription(ColumnHeader, ColumnBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, converter, backGroundBinding, dataType);

            ColumnList.Add(cd);
        }


        public static void AddColumn(string ColumnHeader, Binding oBinding, int ColumnWidth, bool widthAuto,
                               bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter, string backGroundBinding, enumDataType dataType)
        {
            if (ColumnList == null)
                ColumnList = new List<ColumnDescription>();
            var cd = new ColumnDescription(ColumnHeader, oBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, converter, backGroundBinding, dataType);

            ColumnList.Add(cd);
        }


        public static void AddColumn(string ColumnHeader, Binding oBinding, int ColumnWidth, bool widthAuto, bool setFilterPath, bool setSortPath, enumType type, enumDataType datatype)
        {

            if (ColumnList == null)
                ColumnList = new List<ColumnDescription>();
            var cd = new ColumnDescription(ColumnHeader, oBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, datatype);
            ColumnList.Add(cd);

        }



        public static void AddColumn(string ColumnHeader, Binding oBinding, int ColumnWidth, bool widthAuto,
                                    bool setFilterPath, bool setSortPath, enumType type)
        {

            AddColumn(ColumnHeader, oBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, null);

        }



        public static void AddColumn(string ColumnHeader, Binding oBinding, int ColumnWidth, bool widthAuto,
                                        bool setFilterPath, bool setSortPath, enumType type, IValueConverter converter)
        {
            if (ColumnList == null)
                ColumnList = new List<ColumnDescription>();
            var cd = new ColumnDescription(ColumnHeader, oBinding, ColumnWidth, widthAuto, setFilterPath, setSortPath, type, converter);

            ColumnList.Add(cd);


        }







        public static void ClearColumnList()
        {
            if (ColumnList != null)
                ColumnList.Clear();

        }


        public static void AddStructure(C1.WPF.DataGrid.C1DataGrid ListView, string struktur)
        {

            switch (struktur)
            {

                case "Aggregat":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("ID", "id", 30, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Aggregat", "aggregat", 150, false, true, true, enumType.Text);
                        Binding bAgg = new Binding("Firma");
                        bAgg.Converter = new FirmaIDConverter();
                        ColumnDescription.AddColumn("Firma", bAgg, 120, false, true, true, enumType.Text);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);

                        break;
                    }
                case "Firma":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("ID", "id", 30, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Erstellt", "created", 75, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("KdNr", "KdNr", 70, false, true, true, enumType.Text, enumDataType.Numeric);
                        ColumnDescription.AddColumn("Name", "name", 150, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("IstKunde", "IstKunde", 60, false, true, true, enumType.Checkbox, enumDataType.Numeric);
                        ColumnDescription.AddColumn("Kurzname", "kurzname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Ist SI Firma", "istFirma", 40, false, true, true, enumType.Text, enumDataType.Numeric);

                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);

                        break;
                    }

                case "Person":
                    {
                        //Test Git Comment
                        ColumnDescription.ClearColumnList();
                        //ColumnDescription.AddColumn("ID", "id", 30, false, true, true, enumType.Text);
                        //ColumnDescription.AddColumn("Erstellt", "created", 75, false, true, true, enumType.Date,enumDataType.Date);
                        //ColumnDescription.AddColumn("Männlich", "maennlich", 40, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("Vorname", "vorname", 90, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Nachname", "nachname", 90, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Funktion", "Funktion", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Abteilung", "Abteilung", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Telefon", "Telefon", 150, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Telefax", "Telefax", 150, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("EMail", "email", 180, false, true, true, enumType.Text);
                        //ColumnDescription.AddColumn("Benutzername", "benutzername", 90, false, true, true, enumType.Text);
                        //ColumnDescription.AddColumn("Passwort", "passwort", 90, false, true, true, enumType.Text);
                        //ColumnDescription.AddColumn("Berechtigungen", "rights", 90, false, true, true, enumType.Text);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);

                        break;
                    }


                case "Schiff":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("ID", "id", 30, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Besitzer", "owner", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Name", "name", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Werftname", "werftname", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Werftnummer", "werftnummer", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("ImoNummer", "imonummer", 75, false, true, true, enumType.Text, enumDataType.Numeric);
                        Binding typ = new Binding("id") { Converter = new SchiffAnlagenListeConverter() };
                        ColumnDescription.AddColumn("Anlagen", typ, 150, true, true, true, enumType.MultiLineText);
                        //ColumnDescription.AddColumn("BrPlantNr 1", "brplantnr", 75, false, true, true, enumType.Text);
                        //ColumnDescription.AddColumn("BrPlantNr 2", "brplantnr2", 75, false, true, true, enumType.Text);
                        //ColumnDescription.AddColumn("BrPlantNr 3", "brplantnr3", 75, false, true, true, enumType.Text);
                        //ColumnDescription.AddColumn("BrPlantNr 4", "brplantnr4", 75, false, true, true, enumType.Text);

                        ColumnDescription.AddColumn("JT UnitNr", "jtunitnr", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("JT UrProjekt", "jtursprungsprojekt", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("SP Nr", "spnr", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("SP UrProjekt", "spursprungsprojekt", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("BR ProjNr", "brpprojektnr", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("BR UrProjekt", "brursprungsprojekt", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("TA UrProjekt", "taursprungsprojekt", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("NY UrProjekt", "nyursprungsprojekt", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("FO UrProjekt", "foursprungsprojekt", 75, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("TE UrProjekt", "teursprungsprojekt", 75, false, true, true, enumType.Text);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);

                        break;
                    }

                case "Kalkulationstabellen":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Erstellt", "created", 75, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("Inhalt", "inhalt", 180, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Projektnummer", "projektnummer", 120, false, true, true, enumType.Text);

                        //Binding Hyper = new Binding("projektnummer");
                        //Hyper.Converter = new KalkulationHyperlinkConverter();
                        //ColumnDescription.AddColumn("Projektnummer", Hyper, 120, false, true, true, enumType.Hyperlink);

                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);

                        break;
                    }


                case "Lagerlisten":
                    {
                        ColumnDescription.ClearColumnList();

                        var LiefCon = new Binding("id_lieferant") { Converter = new FirmaIDConverter() };
                        ColumnDescription.AddColumn("Lieferant", LiefCon, 120, false, false, true, enumType.Text);


                        ColumnDescription.AddColumn("Bezeichnung", "bezeichnung", 90, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Artikel Nr.", "artikelnr", 90, false, true, true, enumType.Text);


                        ColumnDescription.AddColumn("Produkt Nr.", "produktnr", 90, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Beschreibung", "beschreibung", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Description", "beschreibungeng", 120, false, true, true, enumType.Text);

                        //var oa = new Binding("id_parent") { Converter = new LagerlistenOAConverter() };
                        ColumnDescription.AddColumn("Oberartikel", "oberartikel", 120, false, true, true, enumType.Text);

                        //var AnzMin = new Binding("AnzMin") { Converter = new LLAnzahlMinimumConverter() };
                        //ColumnDescription.AddColumn("Beschreibung", AnzMin, 120, false, true, true, enumType.none);

                        var Gesamt = new Binding("id") { Converter = new LagerlisteGesamtbestandConverter() };
                        // ColumnDescription.AddColumn("AnzahlX", Gesamt, 40, false, false, false, enumType.TemplateBackground, null, "anzahlmin", enumDataType.Numeric);
                        ColumnDescription.AddColumn("AnzahlX", Gesamt, 40, false, false, false, enumType.TemplateBackground, null, "anzahlmin", enumDataType.Numeric);
                        // ColumnDescription.AddColumn("Anzahl", Gesamt, 40, false, false, false,enumType.Text);

                        ColumnDescription.AddColumn("Min", "anzahlmin", 40, false, false, false, enumType.Text, enumDataType.Numeric);
                        ColumnDescription.AddColumn("Kommission Anzahl", "kommissionanzahl", 40, false, false, false, enumType.Text, enumDataType.Numeric);
                        ColumnDescription.AddColumn("Kauf Anzahl", "kaufanzahl", 40, false, false, false, enumType.Text, enumDataType.Numeric);
                        ColumnDescription.AddColumn("Preis €", "preiseuro", 50, false, false, false, enumType.Text);
                        ColumnDescription.AddColumn("Preis NOK", "preisnok", 50, false, false, false, enumType.Text);
                        ColumnDescription.AddColumn("Regal", "ortregal", 40, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Box", "ortbox", 40, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Entfernt", "hinzugefuegtanzahl", 50, false, false, false, enumType.Text);

                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }


                case "SI_Belege":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("BelegID", "id", 70, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Belegdatum", "Belegdatum", 90, false, true, true, enumType.Date);
                        ColumnDescription.AddColumn("Belegnummer", "Belegnummer", 90, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Belegart", "BelegartName", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Bemerkung", "Bemerkung", 120, false, true, true, enumType.Text);

                        ColumnDescription.AddColumn("Projekt", "projekt_schiff", 90, false, true, true, enumType.Text);
                        //var oa = new Binding("id_Firma") { Converter = new FirmaIDConverter() };
                        //oa.Path.PathParameters.Add("kundenname");
                        ColumnDescription.AddColumn("Lieferant/Kunde", "kundenname", 320, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("BelegNr Lief.", "BelegnummerLieferant", 150, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Vorgang", "id_Vorgang", 90, false, true, true, enumType.Text,enumDataType.Numeric);
                        ColumnDescription.AddColumn("Quellbeleg", "id_Quellbeleg", 90, false, true, true, enumType.Text);


                        //var AnzMin = new Binding("AnzMin") { Converter = new LLAnzahlMinimumConverter() };
                        //ColumnDescription.AddColumn("Beschreibung", AnzMin, 120, false, true, true, enumType.none);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }


                case "SI_Inventuren":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Inventur ID", "id", 70, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Inventurdatum", "Inventurdatum", 90, false, true, true, enumType.Date);
                        ColumnDescription.AddColumn("Inventurart", "InventurartName", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Verantwortlicher", "VeranwortlicherName", 120, false, true, true, enumType.Text);

                        ColumnDescription.AddColumn("Notiz", "Notiz", 240, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Inventur beendet", "Inventurbeendet", 110, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("Inventur gebucht", "Inventurgebucht", 110, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("Buchungsbeleg", "BelegID", 100, false, true, true, enumType.Text);

                        //var AnzMin = new Binding("AnzMin") { Converter = new LLAnzahlMinimumConverter() };
                        //ColumnDescription.AddColumn("Beschreibung", AnzMin, 120, false, true, true, enumType.none);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }


                case "Lagerorte":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("ID", "id", 70, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Lagerortname", "Lagerortname", 150, false, true, true, enumType.Date);
                        ColumnDescription.AddColumn("Lagerortkurzname", "Lagerortkurzname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Memo", "Memo", 240, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Aktiv", "Aktiv", 90, false, true, true, enumType.Text);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }

                case "Textbausteine":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("ID", "id", 50, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Bezeichnung", "Caption", 150, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Beschreibung", "Description", 180, false, true, true, enumType.Text);
                        var gruppe = new Binding("id_Group") { Converter = new CommonTools.Converter.TextbausteinIDConverter() };
                        ColumnDescription.AddColumn("Gruppe", gruppe, 120, false, true, true, enumType.Text);
                        //var sprache = new Binding("id_Sprache") { Converter = new CommonTools.Converter.SpracheIDConverter() };
                        //sprache.Path.PathParameters.Add("id_Sprache");
                        ColumnDescription.AddColumn("Sprache", "Sprache", 120, false, true, true, enumType.Text);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }




                case "StammZahlungsbedingungen":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Zahlungsbedingung", "Zahlungsbedingung", 180, false, true, true, enumType.Text);




                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);

                        break;
                    }








                case "bvAnlagenAuftraege":
                    {
                        ColumnDescription.ClearColumnList();

                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);


                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("BR Proj. Nr.", "brunvollprojektnr", 90, false, true, true, enumType.none);

                        var bTipp = new Binding("id");
                        bTipp.Converter = new ImageReminderConverter();
                        ColumnDescription.AddColumn("Verlauf", bTipp, 60, false, false, false, enumType.Image);

                        var b = new Binding("id");
                        b.Converter = new ImageStatusConverter();
                        ColumnDescription.AddColumn("Status", b, 60, false, false, false, enumType.Image);

                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("Auftrag", "auftrag", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "auftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("SI", "si", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "ersatzteileauftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Referenz (Kd.) BestNr", "referenzkdbestellnummer", 90, true, true, true, enumType.none);
                        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 90, true, true, true, enumType.none);




                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }

                case "AlleAnlagenAuftraege":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);


                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);

                        var bTipp = new Binding("id");
                        bTipp.Converter = new ImageReminderConverter();
                        ColumnDescription.AddColumn("Verlauf", bTipp, 60, false, false, false, enumType.Image);

                        var b = new Binding("id");
                        b.Converter = new ImageStatusConverter();
                        ColumnDescription.AddColumn("Status", b, 60, false, false, false, enumType.Image);

                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("Auftrag", "auftrag", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "auftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("SI", "si", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "ersatzteileauftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Referenz (Kd.) BestNr", "referenzkdbestellnummer", 90, true, true, true, enumType.none);
                        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 90, true, true, true, enumType.none);

                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }

                case "AlleAnlagenAuftraege_Land":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);


                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);

                        var bTipp = new Binding("id");
                        bTipp.Converter = new ImageReminderConverter();
                        ColumnDescription.AddColumn("Verlauf", bTipp, 60, false, false, false, enumType.Image);

                        var b = new Binding("id");
                        b.Converter = new ImageStatusConverter();
                        ColumnDescription.AddColumn("Status", b, 60, false, false, false, enumType.Image);

                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("Auftrag", "auftrag", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "auftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("SI", "si", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "ersatzteileauftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Referenz (Kd.) BestNr", "referenzkdbestellnummer", 90, true, true, true, enumType.none);
                        ColumnDescription.AddColumn("Projekt", "projekt_schiff", 90, true, true, true, enumType.none);

                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }

                case "Angebote": // Brunvoll
                    {
                        ColumnDescription.ClearColumnList();

                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Brunvoll ProjektNr.", "brunvollprojektnr", 75, false, true, true, enumType.none);

                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        Binding b = new Binding("id") { Converter = new AnlagenTypConverter() };
                        ColumnDescription.AddColumn("Anlagentyp", b, 150, true, false, false, enumType.MultiLineText);
                        // ColumnDescription.AddColumn("Typ", "typ", 90, true, true, true, enumType.none);
                        //Binding typ = new Binding("id") { Converter = new LieferzeitTypNoSpaceConverter() };
                        //ColumnDescription.AddColumn("Lieferzeit", typ, 90, true, true, true, enumType.MultiLineText);

                        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 90, true, true, true, enumType.none);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };


                case "AuftragsBestand": // Brunvoll
                    {
                        ColumnDescription.ClearColumnList();
                        // Type wieder rausnehmen - nur zum testen
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("BR Proj.Nr.", "brunvollprojektnr", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 90, true, true, true, enumType.none);
                        ColumnDescription.AddColumn("NB-Nr", "werftnummer", 90, false, true, true, enumType.none);

                        Binding nummer = new Binding("id") { Converter = new LieferzeitKomponenteConverter() };
                        ColumnDescription.AddColumn("Komponente", nummer, 90, true, true, true, enumType.MultiLineText);

                        Binding typ = new Binding("id") { Converter = new LieferzeitTypConverter() };
                        ColumnDescription.AddColumn("Typ", typ, 90, true, true, true, enumType.MultiLineText);

                        Binding lieferzeit = new Binding("id") { Converter = new LieferzeitLieferzeitConverter() };
                        ColumnDescription.AddColumn("Lieferzeit", lieferzeit, 70, false, false, true, enumType.MultiLineText);

                        Binding versandtam = new Binding("id") { Converter = new LieferzeitVersandtAmConverter() };
                        ColumnDescription.AddColumn("Versandt am", versandtam, 70, false, false, true, enumType.MultiLineText);

                        Binding plants = new Binding("id") { Converter = new LieferzeitPlantConverter() };
                        ColumnDescription.AddColumn("Plant Nr: ", plants, 90, false, true, true, enumType.MultiLineText);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };


                //case "Angebote":
                //    {
                //        ColumnDescription.ClearColumnList();

                //        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                //        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);
                //        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                //        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date,enumDataType.Date);
                //        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.none);
                //        ColumnDescription.AddColumn("Typ", "typ", 90, true, true, true, enumType.none);
                //        Binding typ = new Binding("id") { Converter = new LieferzeitTypConverter() };
                //        ColumnDescription.AddColumn("Typ", typ, 90, true, true, true, enumType.MultiLineText);

                //        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 90, true, true, true, enumType.none);
                //        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                //        break;

                //    };


                case "jetAngebot": // Jets
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);

                        ColumnDescription.AddColumn("L", "landx", 50, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("S", "schiff", 50, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Int.No.", "internnumber", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Offer Nr.", "orderno", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        AddColumnKundenname(DoUseConverter);
                        Binding b = new Binding("id");
                        b.Converter = new AnlagenTypConverter();
                        ColumnDescription.AddColumn("Anlagentyp", b, 150, true, false, false, enumType.MultiLineText, new AnlagenTypConverter());
                        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 90, true, true, true, enumType.none);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }



                case "jetAngebot_Land": // Jets
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);

                        ColumnDescription.AddColumn("L", "landx", 50, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("S", "schiff", 50, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Int.No.", "internnumber", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Offer Nr.", "orderno", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        Binding b = new Binding("id");
                        b.Converter = new AnlagenTypConverter();
                        ColumnDescription.AddColumn("Anlagentyp", b, 150, true, false, false, enumType.MultiLineText, new AnlagenTypConverter());
                        ColumnDescription.AddColumn("Projekt", "projekt_schiff", 90, true, true, true, enumType.none);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }


                case "AnlagenAuftraege": // Jets
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);
                        //ColumnDescription.AddColumn("Created", "created", 70, false, true, true, enumType.Date,enumDataType.Date);
                        ColumnDescription.AddColumn("L", "landx", 20, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("S", "schiff", 20, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);

                        var bTipp = new Binding("id");
                        bTipp.Converter = new ImageReminderConverter();
                        ColumnDescription.AddColumn("Verlauf", bTipp, 60, false, false, false, enumType.Image);

                        var b = new Binding("id");
                        b.Converter = new ImageStatusConverter();
                        ColumnDescription.AddColumn("Status", b, 60, false, false, false, enumType.Image);

                        ColumnDescription.AddColumn("Datum", "datum", 70, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("Auftrag", "auftrag", 40, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "auftragsbestaetigung", 70, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("SI", "si", 30, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "ersatzteileauftragsbestaetigung", 70, false, true, true, enumType.Date, enumDataType.Date);

                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("KdNr", "KdNr", 70, false, true, true, enumType.Text, enumDataType.Numeric);

                        ColumnDescription.AddColumn("Referenz (Kd.) BestNr", "referenzkdbestellnummer", 150, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 120, true, true, true, enumType.none);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }

                case "AnlagenAuftraege_Land": // Jets
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);
                        //ColumnDescription.AddColumn("Created", "created", 70, false, true, true, enumType.Date,enumDataType.Date);
                        ColumnDescription.AddColumn("L", "landx", 20, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("S", "schiff", 20, false, false, false, enumType.Checkbox);
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);

                        var bTipp = new Binding("id");
                        bTipp.Converter = new ImageReminderConverter();
                        ColumnDescription.AddColumn("Verlauf", bTipp, 60, false, false, false, enumType.Image);

                        var b = new Binding("id");
                        b.Converter = new ImageStatusConverter();
                        ColumnDescription.AddColumn("Status", b, 60, false, false, false, enumType.Image);

                        ColumnDescription.AddColumn("Datum", "datum", 70, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("Auftrag", "auftrag", 40, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "auftragsbestaetigung", 70, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("SI", "si", 30, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "ersatzteileauftragsbestaetigung", 70, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Referenz (Kd.) BestNr", "referenzkdbestellnummer", 150, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Projekt", "projekt_schiff", 120, true, true, true, enumType.none);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }



                //case "jetKommission": // Jets
                //    {
                //        ColumnDescription.ClearColumnList();


                //        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);
                //        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                //        ColumnDescription.AddColumn("NB Nr.", "werftnummer", 90, true, true, true, enumType.none);

                //        Binding intno = new Binding("id") { Converter = new RechnungIntOrderNoConverter() };
                //        ColumnDescription.AddColumn("Int. Order No", intno, 90, true, true, true, enumType.MultiLineText);
                //        Binding rnr = new Binding("id") { Converter = new RechnungRechnungNrConverter() };
                //        ColumnDescription.AddColumn("Invoice No.", rnr, 90, true, true, true, enumType.MultiLineText);

                //        ColumnDescription.AddColumn("Cust. Name", "kundenname", 120, false, true, true, enumType.none);
                //        ColumnDescription.AddColumn("Unit-Nr", "brunvollprojektnr", 90, true, true, true, enumType.none);
                //        Binding faktor = new Binding("id") { Converter = new RechnungFaktorConverter() };
                //        ColumnDescription.AddColumn("%", faktor, 90, true, true, true, enumType.MultiLineText);
                //        Binding Kommission = new Binding("id") { Converter = new RechnungKommissionConverter() };
                //        ColumnDescription.AddColumn("Comm Amount NOK", Kommission, 90, true, true, true, enumType.MultiLineText);
                //        Binding faelligAm = new Binding("id") { Converter = new RechnungFaelligAmConverter() };
                //        ColumnDescription.AddColumn("Fällig", faelligAm, 90, false, true, true, enumType.MultiLineText);
                //        Binding bezahlt = new Binding("id") { Converter = new RechnungKommissioErhaltenConverter() };
                //        ColumnDescription.AddColumn("Komm. bezahlt", bezahlt, 120, true, true, true, enumType.MultiLineText);


                //        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                //        break;

                //    };


                case "jetKommission": // Jets
                    {
                        ColumnDescription.ClearColumnList();

                        //Binding Firma = new Binding("firmenname") { Converter = new TestLeerzeileConverter() };
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.MultiLineText);
                        // Binding pn = new Binding("projektnummer") { Converter = new TestLeerzeileConverter() };
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.Text);


                        ColumnDescription.AddColumn("NB Nr.", "werftnummer", 90, true, true, true, enumType.none);

                        //Binding intno = new Binding("id") { Converter = new RechnungIntOrderNoConverter() };
                        ColumnDescription.AddColumn("Int. Order No", "internnumber", 90, true, true, true, enumType.MultiLineText);
                        // Binding rnr = new Binding("id") { Converter = new RechnungRechnungNrConverter() };
                        ColumnDescription.AddColumn("Invoice No.", "rechnungnr", 90, true, true, true, enumType.MultiLineText);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Cust. Name", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Unit-Nr", "brunvollprojektnr", 90, true, true, true, enumType.none);
                        // Binding faktor = new Binding("id") { Converter = new RechnungFaktorConverter() };
                        ColumnDescription.AddColumn("%", "faktor", 90, true, true, true, enumType.MultiLineText);
                        // Binding Kommission = new Binding("id") { Converter = new RechnungKommissionConverter() };
                        ColumnDescription.AddColumn("Comm Amount NOK", "kommission", 90, true, true, true, enumType.MultiLineText);
                        // Binding faelligAm = new Binding("id") { Converter = new RechnungFaelligAmConverter() };
                        ColumnDescription.AddColumn("Fällig", "faelligam", 90, false, true, true, enumType.MultiLineText);
                        // Binding bezahlt = new Binding("id") { Converter = new RechnungKommissioErhaltenConverter() };
                        ColumnDescription.AddColumn("Komm. bezahlt", "kommissionerhalten", 120, true, true, true, enumType.MultiLineText);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };

                case "jetKommission_Land": // Jets
                    {
                        ColumnDescription.ClearColumnList();

                        //Binding Firma = new Binding("firmenname") { Converter = new TestLeerzeileConverter() };
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.MultiLineText);
                        // Binding pn = new Binding("projektnummer") { Converter = new TestLeerzeileConverter() };
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.Text);


                        // ColumnDescription.AddColumn("NB Nr.", "werftnummer", 90, true, true, true, enumType.none);

                        //Binding intno = new Binding("id") { Converter = new RechnungIntOrderNoConverter() };
                        ColumnDescription.AddColumn("Int. Order No", "internnumber", 90, true, true, true, enumType.MultiLineText);
                        // Binding rnr = new Binding("id") { Converter = new RechnungRechnungNrConverter() };
                        ColumnDescription.AddColumn("Invoice No.", "rechnungnr", 90, true, true, true, enumType.MultiLineText);
                        var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Cust. Name", kd, 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Unit-Nr", "brunvollprojektnr", 90, true, true, true, enumType.none);
                        // Binding faktor = new Binding("id") { Converter = new RechnungFaktorConverter() };
                        ColumnDescription.AddColumn("%", "faktor", 90, true, true, true, enumType.MultiLineText);
                        // Binding Kommission = new Binding("id") { Converter = new RechnungKommissionConverter() };
                        ColumnDescription.AddColumn("Comm Amount NOK", "kommission", 90, true, true, true, enumType.MultiLineText);
                        // Binding faelligAm = new Binding("id") { Converter = new RechnungFaelligAmConverter() };
                        ColumnDescription.AddColumn("Fällig", "faelligam", 90, false, true, true, enumType.MultiLineText);
                        // Binding bezahlt = new Binding("id") { Converter = new RechnungKommissioErhaltenConverter() };
                        ColumnDescription.AddColumn("Komm. bezahlt", "kommissionerhalten", 120, true, true, true, enumType.MultiLineText);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };


                case "jetKommissionErsatzteileNeu": // Jets
                    {
                        ColumnDescription.ClearColumnList();



                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Jets-Int.", "internnumber", 90, true, true, true, enumType.none);

                        //Binding intno = new Binding("id") { Converter = new RechnungRechnungNrConverter() };
                        ColumnDescription.AddColumn("Rechn. Nr.", "rechnungnr", 90, true, true, true, enumType.MultiLineText);

                        ColumnDescription.AddColumn("Unit Nr.:", "brunvollprojektnr", 90, false, true, true, enumType.none);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kunde", "kundenname", 200, false, true, true, enumType.Text);

                        Binding Kommission = new Binding("kommission") { Converter = new FormatCurrencyConverter() };
                        // Kommission.StringFormat = "c";
                        ColumnDescription.AddColumn("Komm. offen", Kommission, 90, true, true, true, enumType.Currency);

                        Binding bezahlt = new Binding("kommissionbetragerhalten") { Converter = new FormatCurrencyConverter() };
                        bezahlt.StringFormat = "{}{0:#,0.00}";
                        ColumnDescription.AddColumn("gezahlte Kommission", bezahlt, 90, false, true, true, enumType.Currency);

                        Binding faelligAm = new Binding("faelligam") { Converter = new FormatDateConverter() };
                        //faelligAm.StringFormat = "{}{1:dd.MM.yyyy}";
                        ColumnDescription.AddColumn("Fällig", faelligAm, 90, false, true, true, enumType.Date);

                        Binding Bemerkung = new Binding("kommissionerhalten") { Converter = new FormatDateConverter() };
                        // Bemerkung.StringFormat = "c";
                        ColumnDescription.AddColumn("Bemerkung", Bemerkung, 90, false, true, true, enumType.Date);

                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };


                case "jetKommissionErsatzteile": // Jets
                    {
                        ColumnDescription.ClearColumnList();



                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Jets-Int.", "internnumber", 90, true, true, true, enumType.none);

                        Binding intno = new Binding("id") { Converter = new RechnungRechnungNrConverter() };
                        ColumnDescription.AddColumn("Rechn. Nr.", intno, 90, true, true, true, enumType.MultiLineText);

                        ColumnDescription.AddColumn("Unit Nr.:", "brunvollprojektnr", 90, false, true, true, enumType.none);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kunde", "kundenname", 200, false, true, true, enumType.Text);

                        Binding Kommission = new Binding("id") { Converter = new RechnungKommissionConverter() };
                        ColumnDescription.AddColumn("Komm. offen", Kommission, 90, true, true, true, enumType.MultiLineText);

                        Binding bezahlt = new Binding("id") { Converter = new RechnungKommissionBetragErhaltenConverter() };
                        ColumnDescription.AddColumn("gezahlte Kommission", bezahlt, 90, false, true, true, enumType.MultiLineText);

                        Binding faelligAm = new Binding("id") { Converter = new RechnungFaelligAmConverter() };
                        ColumnDescription.AddColumn("Fällig", faelligAm, 90, false, true, true, enumType.MultiLineText);

                        Binding Bemerkung = new Binding("id") { Converter = new RechnungKommissioErhaltenConverter() };
                        ColumnDescription.AddColumn("Bemerkung", Bemerkung, 90, false, true, true, enumType.MultiLineText);

                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };


                case "SperreAngebot": // Sperre
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);

                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Offer Nr.", "orderno", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        Binding b = new Binding("id");
                        b.Converter = new AnlagenTypConverter();
                        ColumnDescription.AddColumn("Anlagentyp", b, 150, true, false, false, enumType.MultiLineText, new AnlagenTypConverter());
                        ColumnDescription.AddColumn("Schiff/Projekt", "projekt_schiff", 90, true, true, true, enumType.none);




                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }

                case "SperreAnlagenAuftraege":
                    {
                        ColumnDescription.ClearColumnList();
                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);


                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Order Nr.", "brunvollprojektnr", 75, false, true, true, enumType.none);
                        var bTipp = new Binding("id");
                        bTipp.Converter = new ImageReminderConverter();
                        ColumnDescription.AddColumn("Verlauf", bTipp, 60, false, false, false, enumType.Image);

                        var b = new Binding("id");
                        b.Converter = new ImageStatusConverter();
                        ColumnDescription.AddColumn("Status", b, 60, false, false, false, enumType.Image);

                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("Auftrag", "auftrag", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "auftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        ColumnDescription.AddColumn("SI", "si", 60, false, true, true, enumType.Checkbox);
                        ColumnDescription.AddColumn("AB", "ersatzteileauftragsbestaetigung", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Referenz (Kd.) BestNr", "referenzkdbestellnummer", 90, true, true, true, enumType.none);
                        ColumnDescription.AddColumn("Schiff", "projekt_schiff", 90, true, true, true, enumType.none);




                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;
                    }

                case "SperreKommission": // Jets
                    {
                        ColumnDescription.ClearColumnList();



                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("NB Nr.", "werftnummer", 90, true, true, true, enumType.none);


                        ColumnDescription.AddColumn("Order No.", "orderno", 90, true, true, true, enumType.MultiLineText);
                        Binding rnr = new Binding("id") { Converter = new RechnungRechnungNrConverter() };
                        ColumnDescription.AddColumn("Rg Nr.", rnr, 90, true, true, true, enumType.MultiLineText);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 240, false, true, true, enumType.Text);
                        ColumnDescription.AddColumn("Schiff/Projekt", "projekt_schiff", 120, false, true, true, enumType.none);
                        Binding faktor = new Binding("id") { Converter = new RechnungFaktorConverter() };
                        ColumnDescription.AddColumn("%", faktor, 40, true, true, true, enumType.MultiLineText);
                        Binding Kommission = new Binding("id") { Converter = new RechnungKommissionConverter() };
                        ColumnDescription.AddColumn("Kommission", Kommission, 90, true, true, true, enumType.MultiLineText);
                        Binding bezahlt = new Binding("id") { Converter = new RechnungKommissioErhaltenConverter() };
                        ColumnDescription.AddColumn("Komm. erhalten", bezahlt, 90, false, true, true, enumType.MultiLineText);


                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };


                case "Sonderprojekte":
                    {
                        ColumnDescription.ClearColumnList();

                        ColumnDescription.AddColumn("Typ", "type", 75, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Firma", "firmenname", 90, false, true, true, enumType.none);
                        ColumnDescription.AddColumn("Projektnr.", "projektnummer", 75, false, true, true, enumType.none);
                        //ColumnDescription.AddColumn("Brunvoll ProjektNr.", "brunvollprojektnr", 75, false, true, true, enumType.none);

                        ColumnDescription.AddColumn("Datum", "datum", 80, false, true, true, enumType.Date, enumDataType.Date);
                        //var kd = new Binding("KdNr");
                        // kd.Converter = new KundennameKdNrConverter();
                        ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
                        //Binding b = new Binding("id") { Converter = new AnlagenTypConverter() };
                        //ColumnDescription.AddColumn("Anlagentyp", b, 150, true, false, false, enumType.MultiLineText);
                        // ColumnDescription.AddColumn("Typ", "typ", 90, true, true, true, enumType.none);
                        //Binding typ = new Binding("id") { Converter = new LieferzeitTypNoSpaceConverter() };
                        //ColumnDescription.AddColumn("Lieferzeit", typ, 90, true, true, true, enumType.MultiLineText);

                        ColumnDescription.AddColumn("Projekt", "projekt_schiff", 90, true, true, true, enumType.none);
                        TemplateHelper.AddColumns(ListView, ColumnDescription.ColumnList);
                        break;

                    };




                case "undfined":
                    {
                        break;
                    }

                default:
                    break;
            }



        }

        private static void AddColumnKundenname(bool UseConverter = false)
        {
            if (UseConverter)
            {
                //var KundennameFromKdNr = new Binding("KdNr");
                //KundennameFromKdNr.Converter = new KundennameKdNrConverter();
                //KundennameFromKdNr.Path.PathParameters.Add("KundennameFromKdNr");
                //ColumnDescription.AddColumn("Kundenname", KundennameFromKdNr, 120, false, true, true, enumType.Text);
                ColumnDescription.AddColumn("Kundenname", "KundennameFromKdNr", 120, false, true, true, enumType.Text);

            }
            else
            {
                ColumnDescription.AddColumn("Kundenname", "kundenname", 120, false, true, true, enumType.Text);
            }


            
        }

    }




}
