using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh mesh;
    int resolution;
    int chunkResolution;

    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;
    Vector2 pos;
    public TerrainFace(Mesh mesh, int resolution, Vector3 localUp, Vector2 pos, int chunkResolution)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.pos = pos;
        this.chunkResolution = chunkResolution;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }
    public void ConstructMesh() {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int cnt = (resolution - 1) * (resolution - 1) * 6;
        Vector3[] verticesFix = new Vector3[cnt];
        int[] triangles = new int[cnt];
        int fixIndex = 0;

        for(int y = 0; y < resolution; y++)
        {
            for(int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector3 percent = pos/chunkResolution+new Vector2(x, y) / ((resolution - 1)*chunkResolution);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
                Vector3 pointOnSphere = pointOnUnitCube.normalized;
                vertices[i] = pointOnSphere;
            }
        }
        for (int y = 0; y < resolution - 1; y++)
        {
            for (int x = 0; x < resolution - 1; x++)
            {
                int i = x + y * resolution;
                verticesFix[fixIndex] = vertices[i];
                verticesFix[fixIndex + 1] = vertices[i + resolution + 1];
                verticesFix[fixIndex + 2] = vertices[i + resolution];

                verticesFix[fixIndex + 3] = vertices[i];
                verticesFix[fixIndex + 4] = vertices[i + 1];
                verticesFix[fixIndex + 5] = vertices[i + resolution + 1];
                fixIndex += 6;
            }
        }
        for (int i = 0; i < cnt; i++)
        {
            triangles[i] = i;
        }

        mesh.Clear();
        mesh.vertices = verticesFix;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
