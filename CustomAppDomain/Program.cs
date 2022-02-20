using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CustomAppDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Custom AppDomain *****");

            AppDomain appDomain = AppDomain.CurrentDomain;
            appDomain.ProcessExit += (o, s) =>
            {
                Logger("Default AD unloaded!");
            };

            ListAllAssembliesInAppDomain(appDomain);

            MakeNewAppDomain();
            Console.ReadLine();
        }

        static void MakeNewAppDomain()
        {
            AppDomain secondAppDomain = AppDomain.CreateDomain("SecondAppDomain");
            secondAppDomain.DomainUnload += (o, s) =>
            {
                Logger("The second AppDomain has been unloaded!");
            };

            try
            {
                secondAppDomain.Load("CarLibrary");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            ListAllAssembliesInAppDomain(secondAppDomain);

            AppDomain.Unload(secondAppDomain);
        }

        static void ListAllAssembliesInAppDomain(AppDomain appDomain)
        {
            var assemblies = from asm in appDomain.GetAssemblies()
                             orderby asm.GetName().Name
                             select asm;

            Console.WriteLine($"\n***** Here are the assemblies loaded in {appDomain.FriendlyName} *****");

            foreach (Assembly asm in assemblies)
            {
                Console.WriteLine($" => Name: {asm.GetName().Name}\n\tVersion: {asm.GetName().Version}");
            }
        }

        static void Logger(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
