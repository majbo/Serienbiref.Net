using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serienbrief.Net.serienbrief.api
{
    public class SerienBrief
    {
        private readonly Dictionary<string, string> values;

        public SerienBrief(SerienBriefType type, Dictionary<string, string> values)
        {
            this.values = values;
            Type = type;
        }

        public string getValue(string property)
        {
            return values[property];
        }

        public SerienBriefType Type { get; private set; }
    }
}
