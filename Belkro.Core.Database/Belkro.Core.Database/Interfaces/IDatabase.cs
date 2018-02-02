using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belkro.Core.Database.Interfaces
{
    public interface IDatabase
    {
        void Connect(string aiConnectionString, string aiUsername, string aiPassword);
        void Disconnect();


    }
}
