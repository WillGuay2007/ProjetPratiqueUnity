using System.Collections.Generic;
using UnityEngine;

//Le systeme est construit à partir de références sur internet.

public class A_Star
{
    public static List<Node> FindPath(Node startNode, Node endNode)
    {
        SortedList<(float F, int x, int y), Node> OpenSet = new SortedList<(float F, int x, int y), Node>();
        OpenSet.Add((startNode.F, startNode.GridX, startNode.GridY), startNode);
        List<Node> ClosedSet = new List<Node>();

        startNode.SetG(0);
        startNode.SetH(startNode.GetDistance(endNode));

        while (OpenSet.Count > 0)
        {
            Node current = OpenSet.Values[0];
            OpenSet.RemoveAt(0);
            ClosedSet.Add(current);

            if (current == endNode)
            {
                Node currentPathTile = endNode;
                List<Node> path = new List<Node>();
                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                }
                path.Reverse();
                return path;
            }

            foreach (Node neighbor in current.Neighbors)
            {
                if (!neighbor.IsTraversable) continue;
                if (ClosedSet.Contains(neighbor)) continue;
                bool inSearch = OpenSet.ContainsValue(neighbor);
                float costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetH(neighbor.GetDistance(endNode));
                    neighbor.SetConnection(current);
                    OpenSet.Add((neighbor.F, neighbor.GridX, neighbor.GridY), neighbor);
                }
                else if (costToNeighbor < neighbor.G)
                {
                    OpenSet.Remove((neighbor.F, neighbor.GridX, neighbor.GridY));
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);
                    OpenSet.Add((neighbor.F, neighbor.GridX, neighbor.GridY), neighbor);
                }
            }
        }

        Debug.Log("Could not get A* path.");
        return null;
    }
}
