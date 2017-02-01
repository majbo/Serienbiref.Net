using Serienbrief.Net.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serienbrief.Net.serienbrief.domain
{
    public class SchuleRepository
    {
        private readonly List<Schueler> sus = new List<Schueler>();
        private readonly List<Lehrer> lehrer = new List<Lehrer>();

        public SchuleRepository()
        {
            initSuS();
            initLehrer();
        }

        private void initSuS()
        {
            DateTime date = new DateTime(1965, 1, 1);
            sus.Add(new Schueler("Frank", "Furt", new DateTime(1987, 1, 17), "Sek A",
                                 new List<ErziehungsberechtigtePerson> { new ErziehungsberechtigtePerson("Hans", "Furt", date),
                                                    new ErziehungsberechtigtePerson("Marta", "Furt", date) }));
            sus.Add(new Schueler("Ho Lee", "Fuk", new DateTime(1987, 6, 25), "Sek B",
                                 new List<ErziehungsberechtigtePerson> { new ErziehungsberechtigtePerson("Hans", "Fuk", date),
                                                    new ErziehungsberechtigtePerson("Marta", "Fuk", date) }));
            sus.Add(new Schueler("Karl", "Rasur", new DateTime(1987, 6, 2), "Sek A",
                                 new List<ErziehungsberechtigtePerson> { new ErziehungsberechtigtePerson("Hans", "Rasur", date),
                                                    new ErziehungsberechtigtePerson("Marta", "Rasur",date) }));

            var eltern = new List<ErziehungsberechtigtePerson> { new ErziehungsberechtigtePerson("Hans", "Imal", date),
                                                                               new ErziehungsberechtigtePerson("Marta", "Imal", date) };
            sus.Add(new Schueler("Max", "Imal", new DateTime(1987, 10, 8), "Sek A", eltern));
            sus.Add(new Schueler("Nina", "Imal", new DateTime(1987, 7, 9), "Sek B", eltern));

            sus.Add(new Schueler("Wolf", "Gang", new DateTime(1987, 5, 4), "Sek B",
                                 new List<ErziehungsberechtigtePerson> { new ErziehungsberechtigtePerson("Hans", "Gang", date),
                                                    new ErziehungsberechtigtePerson("Marta", "Gang", date) }));
        }

        private void initLehrer()
        {
            lehrer.Add(new Lehrer("Anette", "Halbestunde", new DateTime(1965, 11, 18), "Sek A"));
            lehrer.Add(new Lehrer("Hans", "Noetig", new DateTime(1965, 7, 15), "Sek B"));
        }


        public Klasse getKlasse(string klasse)
        {
            var sus = this.sus
                .Where(s => s.KlassenId.Equals(klasse))
                .ToList();

            var lehrer = this.lehrer
                .FirstOrDefault(l => l.KlassenId.Equals(klasse));

            if (lehrer == null)
            {
                throw new ArgumentException($"Kein Lehrer gefunden fuer: {klasse}");
            }

            return new Klasse(klasse, lehrer, sus);
        }

        public List<Klasse> getAlleKlassen()
        {
            return sus.Select(s => s.KlassenId)
                .Distinct()
                .Select(k => getKlasse(k))
                .ToList();
        }
    }
}
