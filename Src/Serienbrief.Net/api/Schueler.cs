using System;
using System.Collections.Generic;

namespace Serienbrief.Net.api
{
    public class Schueler : Person
    {
        public Schueler(string vorname, string nachname, DateTime geburtstag, 
            string klassenId, List<ErziehungsberechtigtePerson> erziehungsberechtigtePersonen)
            : base (vorname, nachname, geburtstag)
        {
            KlassenId = klassenId;
            ErziehungsberechtigtePersonen = erziehungsberechtigtePersonen;
        }

        public string  KlassenId { get; private set; }
        public List<ErziehungsberechtigtePerson> ErziehungsberechtigtePersonen { get; private set; }

        public override string ToString()
        {
            return "Schueler{" +
                    "name='" + Vorname + " " + Nachname + '\'' +    '}';
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            if (!base.Equals(obj)) return false;

            Schueler lehrer = (Schueler)obj;

            return KlassenId != null ? KlassenId.Equals(lehrer.KlassenId) : lehrer.KlassenId == null;
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 31 * result + (KlassenId != null ? KlassenId.GetHashCode() : 0);
            return result;
        }
    }
}
