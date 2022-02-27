using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationConroller : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetFloat("SpeedH", body.velocity.x);
        animator.SetFloat("SpeedV", body.velocity.y);
        switch (animator.GetBool("Idle"))
        {
            case true:
                //print("true");
                if (body.velocity.x <= -0.01 || body.velocity.x >= 0.01)
                    animator.SetBool("Idle", false);
                else if (body.velocity.y <= -0.01 || body.velocity.y >= 0.01)
                    animator.SetBool("Idle", false);
                break;
            case false:
                print(false);
                if ((body.velocity.x >= -0.01 || body.velocity.x <= 0.01) && body.velocity.y >= -0.01 || body.velocity.y <= 0.01)
                    animator.SetBool("Idle", true);
                break;
        }
    }
}
