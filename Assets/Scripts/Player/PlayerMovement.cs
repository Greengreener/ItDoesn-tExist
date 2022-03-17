using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove { set => canMove = value; }
    bool canMove;
    float speed;
    Rigidbody2D rigidBody2D;


    void Start()
    {
        speed = 2.7f;
        canMove = true;
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MovementDetection();
    }
    void MovementDetection()
    {
        rigidBody2D.velocity = new Vector2(0, 0);
        if (!canMove)
            return;
        else
        {
            rigidBody2D.velocity = new Vector2(0, 0);

            if (Input.GetKey(KeyCode.W))
            {
                rigidBody2D.velocity += new Vector2(0, speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rigidBody2D.velocity += new Vector2(0, -speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidBody2D.velocity += new Vector2(speed, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rigidBody2D.velocity += new Vector2(-speed, 0);
            }
        }
    }
}
