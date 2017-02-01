using System;

namespace Serienbrief.Net.api
{
    public class ErziehungsberechtigtePerson : Person
    {
        public ErziehungsberechtigtePerson(string vorname, string nachname, DateTime geburtstag)
            : base(vorname, nachname, geburtstag)
        {

        }

        public override string ToString()
        {
            return "ErziehungsberechtigtePerson{" +
                "name='" + Vorname + " " + Nachname + '\'' + "}";
        }
    }
}
