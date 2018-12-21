using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToNav : MonoBehaviour
{

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            var temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            temp = new Vector3(temp.x, 0, temp.z);
            print(temp);
            agent.SetDestination(temp);
        }
    }
}
