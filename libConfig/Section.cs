
namespace libConfig
{
    /// <summary>
    /// Verwaltet eine Einstellungs-Sektion
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Der Name der Sektion
        /// </summary>
        public string Name;

        /// <summary>
        /// Die Einstellungen der Sektion
        /// </summary>
        public Settings Settings;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="sectionName">Der Name der Sektion</param>
        public Section(string sectionName)
        {
            this.Name = sectionName;
            this.Settings = new Settings();
        }
    }
}
