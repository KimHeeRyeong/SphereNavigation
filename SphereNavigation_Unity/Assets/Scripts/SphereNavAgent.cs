using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShpereNavigation
{

    public class SphereNavAgent : AStarFindPath
    {
        public float speed;

        bool _goal;
        uint _goalID;
        float distanceLimit = 0.1f; 
        List<Vector3> path;
        private new void Awake()
        {
            base.Awake();
            _goalID = 1000;
            _goal = true;
            path = null;
        }
        private void FixedUpdate()
        {
            if (!_goal)
            {
                if (path.Count == 0)
                {
                    _goal = true;
                    return;
                }
                Vector3 moveTo = path[0];
                Vector3 dir = moveTo - transform.position;
                dir.Normalize();

                transform.position += dir * speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, moveTo) < distanceLimit)
                    path.RemoveAt(0);
                
            }
        }
        public void SetDestination(Vector3 moveTo)
        {
            uint goal_id = GetPositionId(moveTo);
            if (goal_id == _goalID)
                return;
            uint start_id = GetPositionId(transform.position);

            path = FindPathOrNull(start_id, goal_id);
            if (path != null)
            {
                goal_id = _goalID;
                _goal = false;
            }   
        }
        public bool GetGoal()
        {
            return _goal;
        }
    }
}
