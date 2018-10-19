using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace WaWi.Vorgaenge
{
    public class Vorgaenge  // : IDisposable
    {
        SteinbachEntities db;

        public const string VorgangsNummerNichtVorhandenMessage = "Die Vorgangsnummer ist nicht vorhanden";
        public const string FirmenNummerNichtVorhandenMessage = "Die Firmennummer ist nicht vorhanden";
        public const string FirmaKannNichtNullSeinMessage = "Die Firma kann nicht Null sein";
        public const string BelegKannNichtNullSeinMessage = "Der Beleg kann nicht Null sein";
        public const string VorgangKannNichtNullSeinMessage = "Der Vorgang kann nicht Null sein";


        public Vorgaenge(SteinbachEntities Context)
        {
            db = Context;
        }
        //public Vorgaenge()
        //{

        //}


        private bool ProcessBelegPositionen(SI_Vorgaenge vorgang, SI_Belege beleg)
        {


            if (vorgang == null)
            {
                throw new ArgumentNullException("Vorgang", VorgangKannNichtNullSeinMessage);
            }

            if (beleg == null)
            {
                throw new ArgumentNullException("Beleg", BelegKannNichtNullSeinMessage);
            }

            if (vorgang != null && beleg != null && beleg.SI_BelegePositionen != null)
            {
                foreach (var item in beleg.SI_BelegePositionen)
                {
                    var vPos = vorgang.SI_VorgaengePositionen.Where(n => n.id_Artikel == item.id_Artikel).SingleOrDefault();
                    if (vPos == null)
                    {
                        vPos = new SI_VorgaengePositionen();
                        vPos.SI_Vorgaenge = vorgang;
                        vPos.id_Artikel = item.id_Artikel;
                        vorgang.SI_VorgaengePositionen.Add(vPos);
                        db.AddToSI_VorgaengePositionen(vPos);
                    }
                    switch (beleg.StammBelegarten.CalculateColumn)
                    {
                        case "ab":
                            if (vPos.MengeAB.HasValue)
                            {
                                vPos.MengeAB += ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }
                            else
                            {
                                vPos.MengeAB = ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }

                            break;

                        case "re":
                            if (vPos.MengeRg.HasValue)
                            {
                                vPos.MengeRg += ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }
                            else
                            {
                                vPos.MengeRg = ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }

                            break;

                        case "gs":
                            if (vPos.MengeGS.HasValue)
                            {
                                vPos.MengeGS += ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }
                            else
                            {
                                vPos.MengeGS = ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }

                            break;


                        case "ls":
                            if (vPos.MengeLS.HasValue)
                            {
                                vPos.MengeLS += ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }
                            else
                            {
                                vPos.MengeLS = ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }

                            break;

                        case "rls":
                            if (vPos.MengeLS.HasValue)
                            {
                                vPos.MengeRLS += ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }
                            else
                            {
                                vPos.MengeRLS = ((decimal)item.Menge * beleg.StammBelegarten.Wirkung);
                            }

                            break;
                        default:
                            break;


                    }

                }

                return true;
            }

            return false;
        }



        public bool AddToVorgang(int id_Vorgang, SI_Belege NewBeleg)
        {
            var vorgang = GetVorgang(id_Vorgang);
            return AddToVorgang(vorgang, NewBeleg);
        }

        public bool AddToVorgang(SI_Vorgaenge Vorgang, SI_Belege NewBeleg)
        {
            if (Vorgang != null && NewBeleg != null)
            {
                if (Vorgang.firma == NewBeleg.firma)
                {
                    NewBeleg.SI_Vorgaenge = Vorgang;
                    ProcessBelegPositionen(Vorgang, NewBeleg);
                    return true;
                }

                return false;

            }
            return false;

        }




        //public void ProcessVorgang(SI_Belege QuellBeleg, SI_Belege NewBeleg)
        //{
        //    if (QuellBeleg == null)
        //    {
        //        throw new ArgumentNullException("Quellbeleg darf nicht Null sein");
        //    }

        //    SI_Vorgaenge Vorgang;


        //    if (QuellBeleg.id_Vorgang == 0 || QuellBeleg.id_Vorgang == null)
        //    {
        //        if (QuellBeleg.firma == null)
        //        {
        //            throw new ArgumentNullException("Quellbeleg Firma kann nicht Null sein.");
        //        }

        //        if (NewBeleg.firma == null)
        //        {

        //        }

        //        Vorgang = CreateNewVorgang(NewBeleg.firma);
        //        if (QuellBeleg.firma != NewBeleg.firma)
        //        {
        //            throw new ArgumentOutOfRangeException("Quellbeleg und Folgebeleg müssen sich auf dieselbe Firma beziehen");
        //        }
        //        QuellBeleg.SI_Vorgaenge = Vorgang;
        //        ProcessBelegPositionen(Vorgang, QuellBeleg);


        //    }
        //    else
        //    {
        //       // Vorgang = GetVorgang((int)QuellBeleg.id_Vorgang);
        //        Vorgang = QuellBeleg.SI_Vorgaenge;
        //    }

        //    NewBeleg.id_Quellbeleg = QuellBeleg.id;
        //    NewBeleg.SI_Vorgaenge = Vorgang;
        //    ProcessBelegPositionen(Vorgang, NewBeleg);


        //}




        public bool ProcessVorgang(SI_Belege QuellBeleg, SI_Belege NewBeleg)
        {
            if (QuellBeleg == null)
            {
                return false;
            }

            SI_Vorgaenge Vorgang;


            if (QuellBeleg.id_Vorgang == 0 || QuellBeleg.id_Vorgang == null)
            {
                Vorgang = CreateNewVorgang(NewBeleg.firma);
                if (QuellBeleg.firma != NewBeleg.firma)
                {

                    return false;
                }
                QuellBeleg.SI_Vorgaenge = Vorgang;
                ProcessBelegPositionen(Vorgang, QuellBeleg);


            }
            else
            {
                Vorgang = GetVorgang((int)QuellBeleg.id_Vorgang);
            }

            NewBeleg.id_Quellbeleg = QuellBeleg.id;
            NewBeleg.SI_Vorgaenge = Vorgang;
            return ProcessBelegPositionen(Vorgang, NewBeleg);


        }

        public SI_Vorgaenge GetVorgang(int id_Vorgang)
        {
            SI_Vorgaenge Vorgang;
            Vorgang = db.SI_Vorgaenge.Where(n => n.id == id_Vorgang).SingleOrDefault();
            if (Vorgang == null)
            {
                throw new ArgumentOutOfRangeException("id_Vorgang", id_Vorgang, VorgangsNummerNichtVorhandenMessage);
            }
            return Vorgang;
        }


        public SI_Vorgaenge CreateNewVorgang(int id_Firma)
        {
            var fi = db.firmen.Where(n => n.id == id_Firma).SingleOrDefault();

            if (fi == null)
            {
                throw new ArgumentOutOfRangeException("id_Firma", id_Firma, FirmenNummerNichtVorhandenMessage);
            }
            return CreateNewVorgang(fi);
        }

        public SI_Vorgaenge CreateNewVorgang(firma Firma)
        {

            if (Firma == null)
            {
                throw new ArgumentNullException("Firma kann nicht null sein");
            }

            if (Firma != null)
            {
                SI_Vorgaenge vorgang = new SI_Vorgaenge();
                db.AddToSI_Vorgaenge(vorgang);

                vorgang.Datum = DateTime.Now;
                vorgang.firma = Firma;


                return vorgang;
            }

            return null;

        }

        public void CreateAndProcessNewVorgang(SI_Belege Beleg)
        {
            try
            {
                if (Beleg.SI_Vorgaenge == null)
                {
                    Beleg.SI_Vorgaenge = CreateNewVorgang(Beleg.firma);
                    ProcessBelegPositionen(Beleg.SI_Vorgaenge, Beleg);
                }
            }
            catch (Exception ex)
            {

               // CommonTools.Tools.UserMessage.NotifyUser(ex.Message);
                throw;
            }



        }



    }
}
