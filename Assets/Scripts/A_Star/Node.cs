using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node Connection {  get; private set; }
    public List<Node> Neighbors { get; private set; } = new List<Node>();
    public int GridX { get; private set; }
    public int GridY { get; private set; }
    public float G {  get; private set; }
    public float H { get; private set; }
    public bool IsTraversable { get; private set; }
    public float F => G + H;

    public void SetGridPosition(int x, int y)
    {
        GridX = x;
        GridY = y;
    }
    public void ResetPathData()
    {
        G = 0;
        H = 0;
        Connection = null;
    }
    public void SetConnection(Node node) => Connection = node;
    public void SetIsTraversable(bool isWalkable) => IsTraversable = isWalkable;
    public float GetDistance(Node node)
    {
        int dx = Mathf.Abs(GridX - node.GridX);
        int dy = Mathf.Abs (GridY - node.GridY);

        int straight = 10;
        int diagonal = 14;

        return diagonal * Mathf.Min(dx, dy) + straight * Mathf.Abs(dx - dy);
    }
    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;
}
