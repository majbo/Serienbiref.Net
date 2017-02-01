using System;

namespace Serienbrief.Net.api
{
    public class Lehrer : Person
    {
        public Lehrer(string vorname, string nachname, DateTime geburtstag, string klassenId)
            : base (vorname, nachname, geburtstag)
        {
            KlassenId = klassenId;
        }

        public string KlassenId { get; private set; }

        public override string ToString()
        {
            return "Lehrer{" +
                    "name='" + Vorname + " " + Nachname + '\'' +    '}';
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            if (!base.Equals(obj)) return false;

            Lehrer lehrer = (Lehrer)obj;

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
