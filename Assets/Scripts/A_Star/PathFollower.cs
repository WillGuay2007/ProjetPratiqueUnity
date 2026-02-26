using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private GridBuilder m_gridBuilder;
    [SerializeField] private Tilemap m_tilemap;
    [SerializeField] private float m_speed = 3f;
    private Vector3 m_previousPosition;
    public Vector3 delta => transform.position - m_previousPosition;
    private Coroutine m_followCoroutine;

    public void MoveTo(Transform target)
    {
        var path = PathCompiler.CompilePath(transform, target, m_gridBuilder);
        if (path == null) return;

        //Debug.Log("Path compilation success"); //Pour debugger
        if (m_followCoroutine != null)
        {
            StopCoroutine(m_followCoroutine);
            m_followCoroutine = null;
        }
        m_followCoroutine = StartCoroutine(Follow(path));
    }

    private IEnumerator Follow(List<Node> path)
    {
        foreach (var node in path)
        {
            Vector3 goal = NodeToWorld(node);

            while (Vector3.Distance(transform.position, goal) > 0.05f)
            {
                m_previousPosition = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, goal, m_speed * Time.deltaTime);
                yield return null;
            }
            transform.position = goal;
        }
    }

    private Vector3 NodeToWorld(Node node)
    {
        Vector3Int cell = new Vector3Int(node.GridX, node.GridY, 0);
        return m_tilemap.CellToWorld(cell) + m_tilemap.cellSize / 2f;
    }
}