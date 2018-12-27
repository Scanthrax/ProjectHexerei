using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToNav : MonoBehaviour
{

    NavMeshAgent agent;

    AudioSource source;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }

        #region Left Mouse button hold
        if (Input.GetMouseButton(0))
        {
            #region Set agent destination
            var temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            temp = new Vector3(temp.x, 0, temp.z);
            agent.SetDestination(temp);
            #endregion
        }
        #endregion

        #region If the agent is walking
        if (agent.velocity.magnitude > 0)
        {
            #region play walking sound every 25 frames
            if (Time.frameCount % 25 == 0)
            {
                source.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.WalkGravel);
                source.Play();
            }
            #endregion
        }
        #endregion
    }
}
