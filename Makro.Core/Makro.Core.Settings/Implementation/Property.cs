using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.Settings.Implementation
{
    public class Property
    {
        public Property(string aiKey, object aiValue, string aiDescription)
        {
            Key = aiKey;
            Value = aiValue;
            Description = aiDescription;
            Created = DateTime.Now;
        }

        public DateTime Created { get; }
        public string Description { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
