using System.Collections.Generic;
using UnityEngine;

//Le systeme est construit à partir de références sur internet.

public class A_Star
{
    public static List<Node> FindPath(Node startNode, Node endNode)
    {
        List<Node> OpenSet = new List<Node>() { startNode };
        List<Node> ClosedSet = new List<Node>();

        startNode.SetG(0);
        startNode.SetH(startNode.GetDistance(endNode));

        while (OpenSet.Count > 0)
        {
            Node current = OpenSet[0];
            foreach(Node node in OpenSet)
            {
                if (node.F < current.F || node.F == current.F && node.H < current.H) current = node;
            }
            ClosedSet.Add(current);
            OpenSet.Remove(current);

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
                bool inSearch = OpenSet.Contains(neighbor);
                float costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetH(neighbor.GetDistance(endNode));
                        OpenSet.Add(neighbor);
                    }
                }
            }
        }

        Debug.Log("Could not get A* path.");
        return null;
    }
}
