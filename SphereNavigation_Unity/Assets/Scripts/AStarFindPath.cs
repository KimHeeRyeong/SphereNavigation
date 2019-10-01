 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShpereNavigation
{
    public struct Node
    {
        public uint nodeID;
        public float scoreF;//scoreG+scoreH
        public float scoreG;//start-child distance
        public float scoreH;//child-goal distance
        public uint parentNode;

        public Node(uint id, float f, float g, float h, uint parent)
        {
            nodeID = id;
            scoreF = f;
            scoreG = g;
            scoreH = h;
            parentNode = parent;
        }
    }
    public class AStarFindPath : MonoBehaviour
    {
        [SerializeField]
        SphereNavData sphereNavData;

        Vector3[] _vertices;
        nearVertex[] _nearVertices;
        int _vertexCount;

        protected void Awake()
        {
            _vertices = sphereNavData.vertices;
            _vertexCount = sphereNavData.vertexCount;
            _nearVertices = sphereNavData.nearVertices;
        }
        protected List<Vector3> FindPathVectorOrNull(Vector3 startPos, Vector3 goalPos) {
            uint start_id = GetPositionId(startPos);
            uint goal_id = GetPositionId(goalPos);
            List<Vector3> path = FindPathOrNull(start_id, goal_id);
            return path;
        }
        protected List<Vector3> FindPathOrNull(uint start_id, uint goal_id)
        {
            if (start_id == goal_id)
                return null;

            List<Node> openList = new List<Node>();
            List<Node> closeList = new List<Node>();
            List<Vector3> path = new List<Vector3>();
            openList.Clear();
            closeList.Clear();
            path.Clear();

            Node startNode = new Node(start_id, 0, 0, 0, start_id);
            closeList.Add(startNode);
            uint parent = start_id;
            float pScoreG = 0;//parent scoreG
            while (goal_id != parent)
            {
                //add nearNode in openlist
                uint[] nears = _nearVertices[parent].index;
                int nearCount = nears.Length;
                for (int i = 0; i < nearCount; i++)
                {
                    uint id = nears[i];
                    if (closeList.Exists(x => x.nodeID == id))
                        continue;

                    float scoreG = Vector3.Distance(_vertices[id], _vertices[parent]) + pScoreG;
                    float scoreH = Vector3.Distance(_vertices[id], _vertices[goal_id]);
                    float scoreF = scoreG + scoreH;
                    if (openList.Exists(x => x.nodeID == id))
                    {
                        Node openNode = openList.Find(x => x.nodeID == id);
                        if (openNode.scoreF > scoreF)
                            continue;
                        else
                            openList.Remove(openNode);
                    }
                    Node node = new Node(id, scoreF, scoreG, scoreH, parent);
                    openList.Add(node);
                }
                
                int openCount = openList.Count;
                if (openCount == 0)//can't find path
                    return null;

                //get minimum scoreF in openList and add this in closeList
                Node minFNode = openList[0];
                for (int i = 0; i < openCount; i++)
                {
                    if (minFNode.scoreF > openList[i].scoreF)
                        minFNode = openList[i];
                }
                openList.Remove(minFNode);
                closeList.Add(minFNode);
                parent = minFNode.nodeID;
                pScoreG = minFNode.scoreG;
            }

            //set path
            int closeCnt = closeList.Count;
            for (int i = 0; i < closeCnt; i++)
            {
                if (parent == start_id)
                {
                    path.Add(_vertices[start_id]);
                    break;
                }
                Vector3 pos = _vertices[parent];
                path.Add(pos);
                parent = closeList.Find(x => x.nodeID == parent).parentNode;
            }
            path.Reverse();

            return path;//0(start)->last(goal)
        }
        //get id minimum distance with position
        protected uint GetPositionId(Vector3 position)
        {
            float minDis;
            uint id = 0;
            if (_vertexCount == 0)
                return 0;

            minDis = Vector3.Distance(position, _vertices[0]);
            for(uint i = 1; i < _vertexCount; i++){
                if (minDis == 0)
                    break;
                float distance = Vector3.Distance(position, _vertices[i]);
                if (distance<minDis){
                    minDis = distance;
                    id = i;
                }
            }
            return id;
        }
    }
}
