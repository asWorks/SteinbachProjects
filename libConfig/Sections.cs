using System.Collections.Generic;

namespace libConfig
{
    /// <summary>
    /// Auflistung zur Speicherung von Sektionen
    /// </summary>
    public class Sections : Dictionary<string, Section>
    {
        /// <summary>
        /// Fügt der Auflistung ein neues Section-Objekt hinzu
        /// </summary>
        /// <param name="name">Der Name der Sektion</param>
        public void Add(string name)
        {
            this.Add(name, new Section(name));
        }
    }

}
