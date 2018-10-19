using System.Collections.Generic;
using System.Xml;
using System.IO;
namespace libConfig
{
    /// <summary>
    /// Klasse zur Verwaltung von Konfigurationsdaten
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Speichert den Dateinamen der XML-Datei
        /// </summary>
        private string fileName;

        /// <summary>
        /// Verwaltet die Sektionen
        /// </summary>
        public Sections Sections;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="fileName">Der Dateiname der XML-Datei</param>
        public Config(string fileName)
        {
            this.fileName = fileName;
            this.Sections = new Sections();
        }

        /// <summary>
        /// Ließ die Konfigurationsdaten
        /// </summary>
        /// <returns>Gibt true zurück wenn das Lesen erfolgreich war</returns>
        public bool Load()
        {
            // Variable für den Rückgabewert
            bool returnValue = true;

            // XmlDocument-Objekt für die Einstellungs-Datei erzeugen
            XmlDocument xmlDoc = new XmlDocument();

            // Datei laden
            try
            {
                xmlDoc.Load(this.fileName);
            }
            catch (IOException ex)
            {
                throw new IOException("Fehler beim Laden der Konfigurationsdatei '" +
                   this.fileName + "': " + ex.Message);

            }
            catch (XmlException ex)
            {
                throw new XmlException("Fehler beim Laden der Konfigurationsdatei '" +
                   this.fileName + "': " + ex.Message, ex);
            }

            // Alle Sektionen durchgehen und die Einstellungen einlesen
            foreach (Section section in this.Sections.Values)
            {
                // Alle Einstellungen der Sektion durchlaufen
                foreach (Setting setting in section.Settings.Values)
                {
                    // Einstellung im XML-Dokument lokalisieren
                    XmlNode settingNode = xmlDoc.SelectSingleNode(
                       "/config/" + section.Name + "/" + setting.Name);
                    if (settingNode != null)
                    {
                        // Einstellung gefunden
                        setting.Value = settingNode.InnerText;
                        setting.WasInFile = true;
                    }
                    else
                    {
                        // Einstellung nicht gefunden
                        setting.Value = setting.DefaultValue;
                        setting.WasInFile = false;
                        returnValue = false;
                    }
                }
            }

            // Ergebnis zurückmelden
            return returnValue;
        }

        /// <summary>
        /// Speichert die Konfigurationsdaten
        /// </summary>
        public void Save()
        {
            // XmlDocument-Objekt für die Einstellungs-Datei erzeugen
            XmlDocument xmlDoc = new XmlDocument();

            // Skelett der XML-Datei erzeugen
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" " +
               "standalone=\"yes\"?><config></config>");

            // Alle Sektionen durchgehen und die Einstellungen schreiben
            foreach (Section section in this.Sections.Values)
            {
                // Element für die Sektion erzeugen und anfügen
                XmlElement sectionElement = xmlDoc.CreateElement(section.Name);
                xmlDoc.DocumentElement.AppendChild(sectionElement);

                // Alle Einstellungen der Sektion durchlaufen
                foreach (Setting setting in section.Settings.Values)
                {
                    // Einstellungs-Element erzeugen und anfügen
                    XmlElement settingElement =
                       xmlDoc.CreateElement(setting.Name);
                    settingElement.InnerText = setting.Value;
                    sectionElement.AppendChild(settingElement);
                }
            }

            // Datei speichern
            try
            {
                xmlDoc.Save(this.fileName);
            }
            catch (IOException ex)
            {
                throw new IOException("Fehler beim Speichern der " +
                   "Konfigurationsdatei '" + this.fileName + "': " + ex.Message);

            }
            catch (XmlException ex)
            {
                throw new XmlException("Fehler beim Speichern der " +
                   " Konfigurationsdatei '" + this.fileName + "': " +
                   ex.Message, ex);
            }
        }
    }
}


