using Belkro.Core.Database.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belkro.Core.Database.Interfaces
{
    public interface IDatabaseExecuter
    {
        IDictionary<Type, DataEntry> SelectMany(IDatabase aiDatabase, IQuery aiQuery);
        IList<DataEntry> SelectManyAsList(IDatabase aiDatabase, IQuery aiQuery);
        DataEntry Select(IDatabase aiDatabase, IQuery aiQuery);
    }
}
