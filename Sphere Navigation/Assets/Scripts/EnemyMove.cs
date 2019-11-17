using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    public LayerMask terrainLayer;
    public BuildNavMesh buildNavMesh;
    NavMeshAgent agent;
    bool destination = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void LateUpdate()
    {
        if (IsOnNavMesh())
        {
            agent.enabled = true;
            if (!destination)
            {
                NavMeshTriangulation navMeshTri = NavMesh.CalculateTriangulation();
                Vector3[] vertices = navMeshTri.vertices;
                int cnt = vertices.Length;
                agent.SetDestination(vertices[Random.Range(0, cnt)]);
                destination = true;
            }
            else if (!agent.hasPath||agent.isStopped)
            {
                destination = false;
            }
        }
        else
        {
            destination = false;
            agent.enabled = false;
        }

    }
    bool IsOnNavMesh()
    {
        if (buildNavMesh.GetNavmeshBounds().Contains(transform.position)) {
            return true;
        }
        return false;
    }
}
