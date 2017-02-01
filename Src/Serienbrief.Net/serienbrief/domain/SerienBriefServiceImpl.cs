using Serienbrief.Net.api;
using Serienbrief.Net.serienbrief.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serienbrief.Net.serienbrief.domain
{
    public class SerienBriefServiceImpl
    {
        public const string SUS_NAME = "SuS.Vorname";
        public const string KLASSE_ID = "Klasse.Id";
        public const string LEHRER_NAME = "Lehrer.Name";
        public const string ELTERN_NACHNAME = "Eltern.Nachname";
        public const float GUTE_NOTEN = 5.5f;
        public const float SCHLECHTE_NOTEN = 3.5f;
        public SchuleRepository repo = new SchuleRepository(); //This is fine, no Dependency Injection needed.

        public List<SerienBrief> createElternAbend(string klassenId)
        {
            var briefe = new List<SerienBrief>();
            var klasse = repo.getKlasse(klassenId);

            foreach (var schueler in klasse.Schueler)
            {
                var values = new Dictionary<string, string>();
                values.Add(KLASSE_ID, klassenId);
                values.Add(LEHRER_NAME, klasse.Lehrer.Vorname + " " + klasse.Lehrer.Nachname);
                values.Add(ELTERN_NACHNAME, schueler.ErziehungsberechtigtePersonen[0].Nachname);
                briefe.Add(new SerienBrief(SerienBriefType.ElternAbend, values));
            }
            return briefe;
        }

        public List<SerienBrief> createElternAbendNoten(Dictionary<Schueler, List<float>> noten)
        {
            var briefe = new List<SerienBrief>();
            foreach (var entry in noten)
            {
                float schnitt = calcSchnitt(entry.Value);

                if (schnitt > GUTE_NOTEN)
                {
                    briefe.Add(createGutesElternGeschpraech(entry.Key, schnitt));
                }

                if (schnitt <= SCHLECHTE_NOTEN)
                {
                    briefe.Add(createSchlechtesElternGeschpraech(entry.Key, schnitt));
                }
            }

            return briefe;
        }

        private SerienBrief createGutesElternGeschpraech(Schueler schueler, float schnitt)
        {
            var values = new Dictionary<string, string>();
            string klassenId = schueler.KlassenId;
            var klasse = repo.getKlasse(klassenId);

            values.Add(KLASSE_ID, klassenId);
            values.Add(LEHRER_NAME, klasse.Lehrer.Vorname + " " + klasse.Lehrer.Nachname);
            values.Add(ELTERN_NACHNAME, schueler.ErziehungsberechtigtePersonen[0].Nachname);
            return new SerienBrief(SerienBriefType.ElternGespraechGut, values);
        }

        private SerienBrief createSchlechtesElternGeschpraech(Schueler schueler, float schnitt)
        {
            var values = new Dictionary<string, string>();
            string klassenId = schueler.KlassenId;
            var klasse = repo.getKlasse(klassenId);

            values.Add(KLASSE_ID, klassenId);
            values.Add(LEHRER_NAME, klasse.Lehrer.Vorname + " " + klasse.Lehrer.Nachname);
            values.Add(ELTERN_NACHNAME, schueler.ErziehungsberechtigtePersonen[0].Nachname);
            return new SerienBrief(SerienBriefType.ElternGespraechSchlecht, values);
        }

        public List<SerienBrief> createInformationsAbend()
        {
            var alleKlassen = repo.getAlleKlassen();
            var eltern = new List<ErziehungsberechtigtePerson>();
            var elternbriefe = new List<SerienBrief>();

            var schueler = alleKlassen.SelectMany(k => k.Schueler)
                .ToList();

            foreach (Schueler s in schueler)
            {
                var elternTeil = s.ErziehungsberechtigtePersonen[0];
                if (!eltern.Contains(elternTeil))
                {
                    eltern.Add(elternTeil);
                }
            }

            foreach (ErziehungsberechtigtePerson elternTeil in eltern)
            {
                var values = new Dictionary<string, string>();
                values.Add(ELTERN_NACHNAME, elternTeil.Nachname);
                elternbriefe.Add(new SerienBrief(SerienBriefType.InformationsAbend, values));
            }

            return elternbriefe;
        }


        private float calcSchnitt(List<float> noten)
        {
            if (!noten.Any())
            {
                throw new ArgumentException("Keine Noten vorhanden.");
            }
            return noten.Average();
        }
    }
}
