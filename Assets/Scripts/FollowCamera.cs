using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    void FixedUpdate()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }
}