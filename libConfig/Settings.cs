using System.Collections.Generic;

namespace libConfig
{
    /// <summary>
    /// Auflistung zum Speichern mehrerer Einstellungen
    /// </summary>
    public class Settings : Dictionary<string, Setting>
    {
        /// <summary>
        /// Fügt der Auflistung eine neue Einstellung hinzu
        /// </summary>
        /// <param name="settingName">Der Name der Einstellung</param>
        /// <param name="defaultValue">Der Defaultwert für das Lesen</param>
        public void Add(string settingName, string defaultValue)
        {
            this.Add(settingName,
               new Setting(settingName, defaultValue));
        }
    }
}
