using System;

namespace Serienbrief.Net.api
{
    public class Person
    {
        public Person(string vorname, string nachname, DateTime geburtstag)
        {
            Vorname = vorname;
            Nachname = nachname;
            Geburtstag = geburtstag;
        }

        public string Vorname { get; private set; }

        public string Nachname { get; private set; }

        public DateTime Geburtstag { get; private set; }

        public override string ToString()
        {
            return "Person{" +
                       "vorname='" + Vorname + '\'' +
                       ", nachname='" + Nachname + '\'' +
                       ", geburtstag=" + Geburtstag + '}';
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            Person person = (Person)obj;

            if (!Vorname.Equals(person.Vorname)) return false;
            if (!Nachname.Equals(person.Nachname)) return false;
            return Geburtstag.Equals(person.Geburtstag);
        }

        public override int GetHashCode()
        {
            int result = Vorname.GetHashCode();
            result = 31 * result + Nachname.GetHashCode();
            result = 31 * result + Geburtstag.GetHashCode();
            return result;
        }
    }
}
