using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct nearVertex
{
    public uint[] index;
}
public class SphereNavData : ScriptableObject
{
    public Vector3[] vertices;
    public int vertexCount;
    public nearVertex[] nearVertices;
    
    public void ClearData() {
        vertices = null;
        vertexCount = 0;
        nearVertices = null;
    }
}
