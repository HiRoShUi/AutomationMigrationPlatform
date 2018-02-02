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
        IDatabase Database { get; set; }//The database that will be used
        IDictionary<Type, DataEntry> SelectMany(IQuery aiQuery);//select-query that results into many entries with their type
        IList<DataEntry> SelectManyAsList(IQuery aiQuery);//select-query that results into many entries as a simple List of results
        DataEntry Select(IQuery aiQuery);//select-query that results into one entry
        void Execute(IQuery aiQuery);//insert, update, delete with no results
        DataEntry ExecuteWithResult(IQuery aiQuery);//insert, update, delete with one results
        IDictionary<Type, DataEntry> ExecuteWithManyResults(IQuery aiQuery);//insert, update, delete with many results
        IList<DataEntry> ExecuteWithManyResultsAsList(IQuery aiQuery);//insert, update, delete with many results
    }
}
