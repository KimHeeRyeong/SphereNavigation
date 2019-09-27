using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShpereFindPath
{
    public struct Node
    {
        public uint NodeID;
        public float FScore;//G+H
        public float GScore;//start-child distance
        public float HScore;//child-goal distance
        public uint ParentNode;
    }
    
    public class FindPath : MonoBehaviour
    {
        //vertex information
        public SphereMeshData data;

        Vertex[] vertices;

        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();

        public LineRenderer lineRenderer;
        public int size;

        public uint startPoint = 0;
        public uint goalPoint = 10;

        //load sphere vertex data
        private void Awake()
        {
            vertices = data.vertices.ToArray();
        }
        private void SetVertexData()
        {
            //get vertices
            Vector3[] vert= new Vector3[(size + 1) * (size + 1)];
            for (int y = 0, i = 0; y <= size; y++)
            {
                for (int x = 0; x <= size; x++, i++)
                {
                    vert[i] = new Vector3(x, y);
                }
            }

            vertices = new Vertex[(size + 1) * (size + 1)];
            for (int y = 0, i = 0; y <= size; y++)
            {
                for (int x = 0; x <= size; x++, i++)
                {
                    vertices[i].pos = vert[i];
                    SetNearVerticesData(i);
                }
            }
        }

        private void SetNearVerticesData(int i)
        {
        }
        
    }

}