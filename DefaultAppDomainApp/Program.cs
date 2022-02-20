using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace DefaultAppDomainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Default AppDomain Example *****");

            AppDomain appDomain = AppDomain.CurrentDomain;

            InitDAD(appDomain);
            DisplayDADStats(appDomain);
            ListAllAssembliesInAppDomain(appDomain);
        }

        static void DisplayDADStats(AppDomain appDomain)
        {
            Console.WriteLine($"Name of this domain: {appDomain.FriendlyName}");
            Console.WriteLine($"ID of domain in this process: {appDomain.Id}");
            Console.WriteLine($"Is this the default domain?: {appDomain.IsDefaultAppDomain()}");
            Console.WriteLine($"Base directory of this domain: {appDomain.BaseDirectory}");
        }

        static void ListAllAssembliesInAppDomain(AppDomain appDomain)
        {
            var assemblies = from asm in appDomain.GetAssemblies()
                             orderby asm.GetName().Name
                             select asm;

            Console.WriteLine($"\n***** Here are the assemblies loaded in {appDomain.FriendlyName} *****");

            foreach (Assembly asm  in assemblies)
            {
                Console.WriteLine($" => Name: {asm.GetName().Name}\n\tVersion: {asm.GetName().Version}");
            }
        }

        static void InitDAD(AppDomain appDomain)
        {
            appDomain.AssemblyLoad += (o, s) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n{s.LoadedAssembly.GetName().Name} has been loaded!\n");
                Console.ResetColor();
             };
           
        }
    }
}
