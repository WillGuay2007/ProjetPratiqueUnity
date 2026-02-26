using System.Collections.Generic;
using UnityEngine;

public static class PathCompiler
{
    public static List<Node> CompilePath(Transform startTransform, Transform endTransform, GridBuilder gridBuilder)
    {
        if (gridBuilder == null)
        {
            Debug.Log("No grid builder bruh");
            return null;
        }

        gridBuilder.ResetAllNodes();

        Node startNode = gridBuilder.GetNodeFromWorld(startTransform.position);
        Node endNode = gridBuilder.GetNodeFromWorld(endTransform.position);

        if (startNode == null || endNode == null)
        {
            Debug.Log("Start or end node are outside of the A* grid.");
            return null;
        }

        if (!startNode.IsTraversable || !endNode.IsTraversable)
        {
            Debug.Log("Start or end node are not traversable");
            return null;
        }

        return A_Star.FindPath(startNode, endNode);
    }
}
