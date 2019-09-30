using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct nearVertex
{
    public uint[] index;
}
public class SphereNavData : ScriptableObject
{
    public Vector3[] vertices;
    public nearVertex[] nearVertices;
}
