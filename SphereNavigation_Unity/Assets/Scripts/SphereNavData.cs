using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShpereFindPath
{
    public struct nearVertex {
        uint[] index;
    }
    public class SphereNavData : ScriptableObject
    {
        public Vector3[] vertices;
        public int vertexCount;
        public nearVertex[] nearVertices;
    }
}
