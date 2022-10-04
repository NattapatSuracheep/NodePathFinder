using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Node
    {
        public int nodeId;
        public int[] connectedNode;
    }

    internal class NodePath
    {
        public NodePath(int nodeId)
        {
            path.Add(nodeId);
        }
        public NodePath(List<int> nodes)
        {
            path = new List<int>(nodes);
        }

        public List<int> path = new List<int>();
    }


    public static class Extensions
    {
        public static string ToStringList(this List<int> list)
        {
            var text = "[";
            string.Concat(list);
            for (var i = 0; i < list.Count; i++)
            {
                var prevText = i > 0 ? $"{text}, " : text;
                text = $"{prevText}{list[i]}";
            }
            text = $"{text}]";
            return text;
        }
    }
}
