using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float dirX, dirY;
    public float speed = 5f;
    public Joystick joystick;
    private Rigidbody2D rb;
    public Animator anim;
    public Vector2 movement;
    public float hf =0.0f;
    public float vf =0.0f;
    public Transform sword;
    

    void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<joystick>().joystickstick;
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        dirX = joystick.Horizontal * speed;
        dirY = joystick.Vertical * speed;
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        movement = movement.normalized;
        hf = movement.x >0.01f? movement.x : movement.x <-0.01f?1:0;
        vf = movement.y >0.01f? movement.y : movement.y <-0.01f?1:0;
        if(movement.x <-0.01f)
        {
            this.gameObject.transform.localScale =new Vector3(-1,1,1);
        }
        else
        {
            this.gameObject.transform.localScale =new Vector3(1,1,1);
        }
        anim.SetFloat("Horizontal", hf);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", vf);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY);
    }

}
