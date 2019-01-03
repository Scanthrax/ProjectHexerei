using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToNav : MonoBehaviour
{

    NavMeshAgent agent;

    public AudioSource source;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            agent.isStopped = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            agent.isStopped = false;
        }

        if(Input.GetMouseButtonUp(0))
        {
            PotionSystem.instance.thrownPotion = false;
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
