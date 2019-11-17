using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(1, 10)]
    public int chunkResolution = 3;
    [Range(2,256)]
    public int resolution = 10;

    [SerializeField]Material material;
    [SerializeField,HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    private void OnValidate()
    {
        Initialized();
        GenerateMesh();
    }
    void Initialized()
    {
        int objCnt = 6 * chunkResolution*chunkResolution;
        int filterCnt = meshFilters.Length;

        if (meshFilters == null)
        {
            meshFilters = new MeshFilter[objCnt];
        } 
        else if (filterCnt < objCnt)
        {
            MeshFilter[] temp = new MeshFilter[objCnt];
            for(int i = 0; i < filterCnt; i++)
            {
                temp[i] = meshFilters[i];
            }
            meshFilters = temp;
        }
        else if (filterCnt > objCnt)
        {
            MeshFilter[] temp = new MeshFilter[objCnt];
            for (int i = 0; i < objCnt; i++)
            {
                temp[i] = meshFilters[i];
            }
            for(int i = objCnt; i < filterCnt; i++)
            {
                if(meshFilters[i]!=null)
                    DestroyImmediate(meshFilters[i].gameObject);
            }
            meshFilters = temp;
        }
        terrainFaces = new TerrainFace[objCnt];

        Vector3[] directions ={ Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        string[] directionsName = { "up", "down", "left", "right", "forward", "back" };
        for(int i = 0; i < 6; i++)
        {
            for (int y = 0; y < chunkResolution; y++)
            {
                for (int x = 0; x < chunkResolution; x++)
                {
                    int index = x 
                        + y * chunkResolution 
                        + i * chunkResolution * chunkResolution;
                    if (meshFilters[index] == null)
                    {
                        GameObject meshObj = new GameObject("mesh"+directionsName[i]+x.ToString()+y.ToString());
                        meshObj.transform.parent = transform;

                        meshObj.AddComponent<MeshRenderer>();
                        meshFilters[index] = meshObj.AddComponent<MeshFilter>();
                        
                        Mesh mesh = new Mesh();
                        meshFilters[index].sharedMesh = mesh;
                        meshObj.AddComponent<MeshCollider>().sharedMesh = mesh;
                    }
                    else
                    {   //rename
                        meshFilters[index].gameObject.name = "mesh" + directionsName[i] + x.ToString() + y.ToString();
                    }
                    //set material
                    if (material == null)
                        material = new Material(Shader.Find("Standard"));
                    meshFilters[index].GetComponent<MeshRenderer>().sharedMaterial = material;
                    
                    terrainFaces[index] = new TerrainFace(
                        meshFilters[index].sharedMesh
                        , resolution
                        , directions[i]
                        ,new Vector2(x,y)
                        ,chunkResolution);
                }
            }
        }
    }

    void GenerateMesh() {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }
}
