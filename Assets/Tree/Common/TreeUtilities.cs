using System;
using System.Collections.Generic;
using System.Linq;


namespace TreeUtilities.Common {
    public static class TreeUtilities {
        /// <summary>
        /// Get all descendant nodes in a tree via depth first traversal. 
        /// </summary>
        public static IEnumerable<T> Descendants<T>(this T root, Func<T, IEnumerable<T>> children) {
            Stack<T> nodes = new Stack<T>(new[] {root});
            while (nodes.Any()){
                T node = nodes.Pop();
                yield return node;
                foreach (var n in children(node))
                    nodes.Push(n);
            }
        }

        /// <summary>
        /// Get all descendant nodes in a tree via depth first traversal. 
        /// </summary>
        public static IEnumerable<T> DescendantsBreadthFirst<T>(this T root, Func<T, IEnumerable<T>> children) {
            var q = new Queue<T>();
            q.Enqueue(root);
            while (q.Count > 0){
                T current = q.Dequeue();
                yield return current;
                foreach (var child in children(current))
                    q.Enqueue(child);
            }
        }

        /// <summary>
        /// Get all nodes in a potentially cyclic graph. Depth first traversal.
        /// </summary>
        public static IEnumerable<T> Closure<T>(this T root, Func<T, IEnumerable<T>> children) {
            var seen = new HashSet<T>();
            var stack = new Stack<T>();
            stack.Push(root);

            while (stack.Count != 0){
                T item = stack.Pop();
                if (seen.Contains(item))
                    continue;
                seen.Add(item);
                yield return item;
                foreach (var child in children(item))
                    stack.Push(child);
            }
        }
    }
}