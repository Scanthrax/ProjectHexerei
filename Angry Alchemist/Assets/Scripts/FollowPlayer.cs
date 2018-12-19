using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        this.transform.position = player.transform.position;
        this.transform.position += Vector3.back;
    }
}
