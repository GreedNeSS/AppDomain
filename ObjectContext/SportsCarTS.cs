using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace ObjectContext
{
    [Synchronization]
    class SportsCarTS : ContextBoundObject
    {
        public SportsCarTS()
        {
            Context context = Thread.CurrentContext;

            Console.WriteLine($"\n{this.ToString()} object in context {context.ContextID}\n");

            foreach (IContextProperty contProp in context.ContextProperties)
            {
                Console.WriteLine($"=> Context Property: {contProp.Name}");
            }
        }
    }
}
