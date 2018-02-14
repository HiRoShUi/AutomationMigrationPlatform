using Makro.Core.Serialization.Implementation;
using Makro.Core.Serialization.nsInterfaces;
using Makro.Core.Settings.Implementation.Exceptions;
using Makro.Core.Settings.nsInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.Settings.Implementation
{
    public class JsonFileSettings : ISettings
    {
        protected ISerializer _serializer;
        protected string _path;
        IList<Property> _properties;

        public JsonFileSettings(string aiPath, ISerializer aiSerializer)
        {
            _serializer = aiSerializer;
            _path = aiPath;
            _properties = new List<Property>();
        }

        public void CreateSettingProperties(IList<Property> aiProperties = null)
        {
            if (String.IsNullOrEmpty(_path))
                throw new FilePathNotSetException();

            if (aiProperties == null)
                aiProperties = new List<Property>();

            _properties = aiProperties;
        }

        public void AddProperty(Property aiProperty)
        {
            if (_properties == null)
                throw new PropertiesNotInitializedOrLoadedException();

            _properties.Add(aiProperty);
        }

        public bool IsPropertyKnown(string aiKey)
        {
            if (_properties == null)
                throw new PropertiesNotInitializedOrLoadedException();

            return RetrieveProperty(aiKey) == null  ? false : true;
        }

        public void RemoveProperty(string aiKey)
        {
            if (_properties == null)
                throw new PropertiesNotInitializedOrLoadedException();

            var property = RetrieveProperty(aiKey);
            if (property == null)
                return;

            _properties.Remove(property);
        }


        public bool IsCreated()
        {
            if (_properties == null)
                return false;
            if (_properties.Count > 0)
                return true;
            return false;
        }

        public void LoadSettingProperties()
        {
            if (String.IsNullOrEmpty(_path))
                throw new FilePathNotSetException();

            if (!File.Exists(_path))
                throw new FileNotFoundException("Could not find the settings-file!",_path);

            _properties = _serializer.Deserialize<IList<Property>>(File.ReadAllText(_path));
        }

        public Property RetrieveProperty(string aiKey)
        {
            return _properties.FirstOrDefault(entry => entry.Key == aiKey);
        }


        public void Save()
        {
            if (String.IsNullOrEmpty(_path))
                throw new FilePathNotSetException();
            var jsonSettings = _serializer.Serialize(_properties);
            File.WriteAllText(_path,jsonSettings);
        }

    }
}
