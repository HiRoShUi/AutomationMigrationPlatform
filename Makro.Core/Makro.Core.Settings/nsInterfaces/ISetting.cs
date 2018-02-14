using Makro.Core.Settings.Implementation;
using System.Collections.Generic;

namespace Makro.Core.Settings.nsInterfaces
{
    public interface ISettings
    {
        bool IsCreated();
        void CreateSettingProperties(IList<Property> aiProperties);
        void AddProperty(Property aiProperty);
        void RemoveProperty(string aiKey);
        Property RetrieveProperty(string aiKey);
        bool IsPropertyKnown(string aiKey);
        void LoadSettingProperties();
        void Save();
    }
}