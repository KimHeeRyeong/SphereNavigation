using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WindowSphereData : EditorWindow
{
    Mesh mesh;
    SphereNavData data;
    float nearBoundary;
    [MenuItem("Window/Sphere Navigation")]
    static void Init()
    {
        WindowSphereData window =
            (WindowSphereData)EditorWindow.GetWindow(typeof(WindowSphereData));
    }
    private void OnGUI()
    {
        mesh = (Mesh)EditorGUILayout.ObjectField("Mesh",mesh,typeof(Mesh), true);
        data = (SphereNavData)EditorGUILayout.ObjectField("SphereNavData", data, typeof(SphereNavData), true);
        
        if (GUILayout.Button("Bake"))
        {
            if (mesh != null && data != null)
            {
                data.ClearData();
                SetNoDuplicateVertices();
                SetNearVertices();
                EditorUtility.SetDirty(data);
                Debug.Log("success to setting data ");
            }
            else
                Debug.Log("**object null exception** => fail to setting data ");
        }
    }
    void SetNoDuplicateVertices() {
        List<Vector3> vert_noDp = new List<Vector3>();
        vert_noDp.Clear();

        int verCnt = mesh.vertexCount;
        for (int i = 0; i < verCnt; i++)
        {
            Vector3 point = mesh.vertices[i];
            if (vert_noDp.Exists(x => x == point))
                continue;

            vert_noDp.Add(point);
        }
        data.vertexCount = vert_noDp.Count;
        data.vertices = vert_noDp.ToArray();
    }
    void SetNearVertices() {
        int verCnt = data.vertexCount;
        List<Vector3> vertices = new List<Vector3>(data.vertices);
        nearVertex[] nears = new nearVertex[verCnt];

        int triCnt = mesh.triangles.Length;
        for (int i = 0; i < triCnt; i += 3)
        {
            //get triangle vertex index in data.vertices
            //data vertex index != mesh vertex index(because of remove duplicates)
            uint[] v = new uint[3];
            for(int j= 0; j < 3; j++)
            {
                v[j] = (uint)mesh.triangles[i + j];//triangles[i] = vertex index
                Vector3 vec = mesh.vertices[v[j]];
                v[j] = (uint)vertices.FindIndex(x => x == vec);
            }

            List<uint> index = new List<uint>();
            for(int j = 0; j < 3; j++)
            {
                index.Clear();
                if (nears[v[j]].index != null)
                    index = new List<uint>(nears[v[j]].index);
                for (int k = 0; k < 3; k++) {
                    if (k == j)
                        continue;
                    if (!index.Exists(x => x == v[k]))
                        index.Add(v[k]);
                }
                nears[v[j]].index = index.ToArray();
            }
        }
        data.nearVertices = nears;
    }
}
