using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShpereFindPath{

    public class SphereNavAgent : MonoBehaviour
    {
        [SerializeField]
        SphereNavData sphereNavData;

        public Vector3[] vertices { get=> sphereNavData.vertices; }
        public int vertexCount { get => sphereNavData.vertexCount; }

        public Vertex[] getVertices() {
            return 
        }
    }
}
