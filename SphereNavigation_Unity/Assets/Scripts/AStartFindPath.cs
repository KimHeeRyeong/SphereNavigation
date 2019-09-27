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
    public class AStartFindPath : MonoBehaviour
    {
        [SerializeField]
        SphereNavData sphereNavData;

        List<Node> _openList;
        List<Node> _closeList;
        

        Vector3[] _vertices;
        nearVertex[] _nearVertices;
        int _vertexCount;

        private void Awake()
        {
            _vertices = sphereNavData.vertices;
            _vertexCount = _vertices.Length;
            _nearVertices = sphereNavData.nearVertices;
        }
        List<Vector3> FindPath(Vector3 startPos, Vector3 goalPos) {
            uint start_id = GetPositionId(startPos);
            uint goal_id = GetPositionId(goalPos);

            if (start_id==goal_id)
                return null;

            List<Vector3> path =  new List<Vector3>();
            path.Clear();

            Node startNode = new Node(start_id, 0,0,0,start_id);
            _closeList.Add(startNode);
            uint parent = start_id;
            float pScoreG = 0;//parent scoreG
            while ()
            {
                uint[] nears = _nearVertices[parent].index;
                int nearCount = nears.Length;
                for (int i = 0;i<nearCount;i++)
                {
                    uint id = nears[nearCount];
                    if (_closeList.Exists(x=>x.nodeID==id))
                        continue;
                    float scoreG = Vector3.Distance(_vertices[id], _vertices[parent])+pScoreG;
                    float scoreH = Vector3.Distance(_vertices[id], _vertices[goal_id]);
                    float scoreF = scoreG + scoreH;
                    if (_openList.Exists(x => x.nodeID == id))
                    {
                        Node openNode = _openList.Find(x => x.nodeID == id);
                        if (openNode.scoreF > scoreF)
                            continue;
                        else
                            _openList.Remove(openNode);
                    }
                    Node node = new Node(id, scoreF, scoreG, scoreH, parent);
                    _openList.Add(node);
                }
                
                int openCount = _openList.Count;
                //openList 갯수가 0이라면?
                if (openCount == 0)
                {
                    break;
                }
                //_openList 내부 가장 작은 scoreF값 구하기
                Node minFNode = _openList[0];
                for(int i = 0; i < openCount; i++)
                {
                    if (minFNode.scoreF > _openList[i].scoreF)
                        minFNode = _openList[i];
                }
            }

            _closeList.Clear();
            _openList.Clear();

            return path;
        }
        //get id minimum distance with position
        uint GetPositionId(Vector3 position)
        {
            float minDis;
            uint id = 0;

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
