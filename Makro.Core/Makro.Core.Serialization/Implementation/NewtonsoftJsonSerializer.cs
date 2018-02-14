using Makro.Core.Serialization.nsInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.Serialization.Implementation
{
    public class NewtonsoftJsonSerializer : ISerializer
    {

        public NewtonsoftJsonSerializer()
        {

        }

        public T Deserialize<T>(string aiSerializationText)
        {
            return (T)JsonConvert.DeserializeObject<T>(aiSerializationText);
        }

        public string Serialize<T>(T aiSerializableObject)
        {
            return JsonConvert.SerializeObject(aiSerializableObject);
        }
    }
}
