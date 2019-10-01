using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShpereNavigation;

public class MoveController : MonoBehaviour
{
    SphereNavAgent agent;
    public Transform[] pos;
    int now = 0;
    int posCnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<SphereNavAgent>();
        agent.SetDestination(pos[0].position);
        now = 0;
        posCnt = pos.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.GetGoal()) {
            if (now < posCnt - 1)
            {
                agent.SetDestination(pos[++now].position);
            }
            else
            {
                now = 0;
                agent.SetDestination(pos[0].position);
            }
        }
    }
}
