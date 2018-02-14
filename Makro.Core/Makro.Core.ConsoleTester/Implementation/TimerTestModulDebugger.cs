using Makro.Core.ModuleSystem.HelpClasses;
using Makro.Core.ModuleSystem.Implementation;
using Makro.Core.ModuleSystem.nsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ConsoleTester.Implementation
{
    [Serializable]
    public class TimerTestModulDebugger : BaseTimerBackgroundWorker
    {
        private string name;

        /// <summary>
        /// Here comes the function-code.
        /// </summary>
        /// <returns></returns>
        public override object FunctionImplementation()
        {
            Console.Write("Hello from module " + this.GetType().Name + "! Give me your Name: ");
            name = Console.ReadLine();
            Console.WriteLine("Hello " + name);
            return null;
        }
    }
}
