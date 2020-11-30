using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common.Reflection
{
    public class AssemblyTypeScanner<T> where T : class
    {
        public static IEnumerable<Type> FindMatchingTypesInAssemblies(string pathToScan, bool recursiveScan)
        {
            var toReturn = new List<Type>();
            foreach (var assemblyFileName in
                Directory.EnumerateFiles(pathToScan, "*.dll",
                        recursiveScan ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .Union(
                        Directory.EnumerateFiles(pathToScan, "*.exe",
                            recursiveScan ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)))
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFileName);
                }
                catch
                {
                }
                if (assembly == null) continue;
                assembly = Assembly.LoadFrom(assemblyFileName);
                try
                {
                    toReturn.AddRange(assembly.ExportedTypes.Where(x =>
                        (
                            x.IsSubclassOf(typeof(T))
                            ||
                            x.GetInterface(typeof(T).FullName) != null
                        )
                        && !x.IsAbstract
                        && !x.IsInterface
                    ));
                }
                catch
                {
                }
            }
            return toReturn;
        }
    }
}