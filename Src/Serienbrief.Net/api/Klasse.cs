using System.Collections.Generic;

namespace Serienbrief.Net.api
{
    public class Klasse
    {
        public Klasse(string klasseId, Lehrer lehrer, List<Schueler> schueler)
        {
            KlasseId = klasseId;
            Lehrer = lehrer;
            Schueler = schueler;
        }

        public string KlasseId { get; private set; }
        public Lehrer Lehrer { get; private set; }
        public List<Schueler> Schueler { get; private set; }


        public override string ToString()
        {
            return "Klasse{" +
              "KlasseId='" + KlasseId + '\'' +
              ", lehrer=" + Lehrer +
              ", schueler=" + Schueler + '}';
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            Klasse klasse = (Klasse)obj;

            return KlasseId != null ? KlasseId.Equals(klasse.KlasseId) : klasse.KlasseId == null;
        }

        public override int GetHashCode()
        {
            return KlasseId != null ? KlasseId.GetHashCode() : 0;
        }
    }
}
