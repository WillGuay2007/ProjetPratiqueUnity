using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private int m_width;
    [SerializeField] private int m_height;
    [SerializeField] private Vector2Int m_gridOrigin;
    [SerializeField] private Tilemap m_groundTilemap;
    [SerializeField] private Tilemap m_killTilemap;

    public Node[,] Grid {  get; private set; }

    private void Awake()
    {
        GenerateGrid();
        AssignNeighbors();
    }

    void GenerateGrid()
    {
        Grid = new Node[m_width, m_height];
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                int worldX = x + m_gridOrigin.x;
                int worldY = y + m_gridOrigin.y;

                Node node = new Node();
                node.SetGridPosition(worldX, worldY);

                bool traversable = !(m_groundTilemap.HasTile(new Vector3Int(worldX, worldY, 0)) || m_killTilemap.HasTile(new Vector3Int(worldX, worldY, 0)));
                node.SetIsTraversable(traversable);

                Grid[x, y] = node;
            }
        }
    }

    void AssignNeighbors()
    {
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Node node = Grid[x, y];
                node.Neighbors.Clear();

                if (x > 0) node.Neighbors.Add(Grid[x - 1, y]);
                if (x < m_width - 1) node.Neighbors.Add(Grid[x + 1, y]);
                if (y > 0) node.Neighbors.Add(Grid[x, y - 1]);
                if (y < m_height - 1) node.Neighbors.Add(Grid[x, y + 1]);
                if (x > 0 && y > 0) node.Neighbors.Add(Grid[x - 1, y - 1]);
                if (x < m_width - 1 && y > 0) node.Neighbors.Add(Grid[x + 1, y - 1]);
                if (x > 0 && y < m_height - 1) node.Neighbors.Add(Grid[x - 1, y + 1]);
                if (x < m_width - 1 && y < m_height - 1) node.Neighbors.Add(Grid[x + 1, y + 1]);
            }
        }
    }

    public void ResetAllNodes()
    {
        foreach(Node node in Grid) node.ResetPathData();
    }

    public Node GetNodeFromWorld(Vector3 worldPos)
    {
        Vector3Int cell = m_groundTilemap.WorldToCell(worldPos);

        int x = cell.x - m_gridOrigin.x;
        int y = cell.y - m_gridOrigin.y;

        if (x < 0 || y < 0 || x >= m_width || y >= m_height)
        {
            print("Grid node outside of bounds. Could not get node.");
            return null;
        }
        return Grid[x, y];
    }
}
