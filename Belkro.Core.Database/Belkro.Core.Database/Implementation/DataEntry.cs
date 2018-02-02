using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belkro.Core.Database.Implementation
{
    public class DataEntry
    {
        public DataEntry()
        {

        }

        public DataEntry(string aiName, object aiValue)
        {
            Name = aiName;
            Value = aiValue;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
