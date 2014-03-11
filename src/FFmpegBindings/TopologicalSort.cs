using System;
using System.Collections.Generic;

namespace CppSharp
{
    internal static class TopologicalSort
    {
        /// <summary>
        ///     Topological sort (least dependent first)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        public static IEnumerable<T> TSort<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> dependencies)
        {
            var sorted = new List<T>();
            var visited = new HashSet<T>();

            foreach (T item in source)
                Visit(item, visited, sorted, dependencies);

            return sorted;
        }

        private static void Visit<T>(T item, HashSet<T> visited, List<T> sorted,
            Func<T, IEnumerable<T>> dependencies)
        {
            if (visited.Contains(item)) return;

            visited.Add(item);

            foreach (T dep in dependencies(item))
                Visit(dep, visited, sorted, dependencies);

            sorted.Add(item);
        }
    }
}