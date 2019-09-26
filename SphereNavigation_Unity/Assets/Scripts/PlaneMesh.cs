using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class PlaneMesh : MonoBehaviour
{
    public int xSize, ySize;
    Vector3[] vertecis;
    Mesh mesh;
    void Awake()
    {
        StartCoroutine(Generate());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (vertecis == null)
        {
            return;
        }

        int cnt = vertecis.Length;
        for(int i = 0; i < cnt; i++)
        {
            Gizmos.DrawSphere(vertecis[i], 0.1f);
        }
        
    }
    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertecis = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertecis.Length];
        for (int y = 0, i = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertecis[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)(x / xSize), (float)(y / ySize));
            }
        }
        mesh.vertices = vertecis;
        mesh.uv = uv;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
        yield return wait;
    }
}
