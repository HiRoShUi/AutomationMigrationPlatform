using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.Serialization.nsInterfaces
{
    public interface ISerializer
    {
        string Serialize<T>(T aiSerializableObject);
        T Deserialize<T>(string aiSerializationText);
    }
}
