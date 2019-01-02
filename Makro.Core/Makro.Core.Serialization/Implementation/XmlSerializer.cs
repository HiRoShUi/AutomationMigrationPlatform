using Makro.Core.Serialization.nsInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xml = System.Xml.Serialization;

namespace Makro.Core.Serialization.Implementation
{
    public class XmlSerializer : ISerializer
    {
        public T Deserialize<T>(string aiSerializationText)
        {
            Object ergObj = null;

            try
            {
                //create a serializer for the type 
                Xml.XmlSerializer serializer = new Xml.XmlSerializer(typeof(T));

                MemoryStream memStream = new MemoryStream();
                var writer = new StreamWriter(memStream);
                writer.Write(aiSerializationText);
                writer.Flush();
                memStream.Position = 0;

                // Load the object saved above by using the Deserialize function
                ergObj = (Object)serializer.Deserialize(memStream);

                // Clean the memoryStream.
                memStream.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("It occured an error: " + ex.Message);
            }

            return (T)ergObj;
        }

        public string Serialize<T>(T aiSerializableObject)
        {
            var serializer = new Xml.XmlSerializer(aiSerializableObject.GetType());

            var memStream = new MemoryStream();

            TextWriter sWriter = new StreamWriter(memStream);

            serializer.Serialize(sWriter, aiSerializableObject);

            Trace.WriteLine("Serialized the object successful.");

            sWriter.Close();

            return Encoding.ASCII.GetString(memStream.ToArray());
        }
    }
}
