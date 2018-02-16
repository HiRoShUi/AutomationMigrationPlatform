using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.AMP.Starter
{
    public class Program
    {
        static void Main(string[] args)
        {
            Makro.Core.ModuleSystem.StartUp.Execute("ModuleConfig.conf");

            Console.ReadKey();
        }
    }
}
