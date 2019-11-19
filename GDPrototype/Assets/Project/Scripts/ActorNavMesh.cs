using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorNavMesh : MonoBehaviour
{
    public GameObject goalOne;
    public GameObject goalTwo;
    Vector3 goalOneLoc;
    Vector3 goalTwoLoc;
    NavMeshAgent myNav = null;
    public int goal = 0;

    // Start is called before the first frame update
    void Start()
    {
        myNav = this.gameObject.GetComponent<NavMeshAgent>();
        goalOneLoc = goalOne.transform.position;
        goalTwoLoc = goalTwo.transform.position;
        myNav.destination = goalOneLoc;
        myNav.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myNav.remainingDistance == 0 && goal == 0)
        {
            myNav.destination = goalTwoLoc;
            myNav.isStopped = false;

            if (myNav.remainingDistance == 0)
            {
                goal = 1;
            }
        }
        else if (myNav.remainingDistance == 0 && goal == 1)
        {
            myNav.destination = goalOneLoc;
            myNav.isStopped = false;

            goal = 0;
        }
    }
}
