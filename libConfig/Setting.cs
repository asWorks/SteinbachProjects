
namespace libConfig
{
    /// <summary>
    /// Verwaltet eine Einstellung
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Der Name der Einstellung
        /// </summary>
        public string Name;

        /// <summary>
        /// Der Wert der Einstellung
        /// </summary>
        public string Value;

        /// <summary>
        /// Der Defaultwert für das Lesen
        /// </summary>
        public string DefaultValue;

        /// <summary>
        /// Gibt an, ob die Einstellung
        /// in der Datei gefunden wurde
        /// </summary>
        public bool WasInFile;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name der Einstellung</param>
        /// <param name="defaultValue">Der Defaultwert für das Lesen</param>
        public Setting(string name, string defaultValue)
        {
            this.Name = name;
            this.DefaultValue = defaultValue;
        }
    }

}
