using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ShowAssemblyMethods
{
    class Program
    {
        static void ShowPublicMethodsFromType(Type type)
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                Console.WriteLine($"-{method.Name}");
            }
        }

        static void ShowProtectedMethodsFromType(Type type)
        {
            foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                Console.WriteLine($"-{method.Name}");
            }
        }

        static void ShowMethodsFromDll(string path = "C:\\DEV\\Assemblies")
        {
            if (Directory.Exists(path))
            {
                var pathDlls = Directory.GetFiles(path, "*.dll"); // Take only *.dll

                foreach (var pathDll in pathDlls)
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(pathDll);
                        foreach (var type in assembly.GetTypes())
                        {
                            Console.WriteLine(type.Name);

                            ShowProtectedMethodsFromType(type);
                            ShowPublicMethodsFromType(type);
                        }
                    }
                    catch (System.BadImageFormatException ex)
                    {
                        Console.WriteLine($"{pathDll} - {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"`{path}` not created");
            }
        }

        static void Main(string[] args)
        {
            ShowMethodsFromDll();

            Console.ReadKey();
        }
    }
}
