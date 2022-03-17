using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationConroller : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    Rigidbody2D body;
    Animator animator;

    float threshold = 0.001f;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
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
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                    Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                    animator.SetBool("Idle", false);
                /*if (body.velocity.x <= -threshold || body.velocity.x >= threshold)
                    animator.SetBool("Idle", false);
                else if (body.velocity.y <= -threshold || body.velocity.y >= threshold)
                    animator.SetBool("Idle", false);
                    */
                break;
            case false:
                print(false);
                //if ((body.velocity.x >= -threshold || body.velocity.x <= threshold) && body.velocity.y >= -threshold || body.velocity.y <= 0.01)
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) &&
                    !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                    animator.SetBool("Idle", true);
                break;
        }
    }
    public void RunAttack()
    {
        playerMovement.CanMove = false;
        playerAttack.CanAttack = false;
        animator.SetBool("Attack", true);
    }
    public void EndAttack()
    {
        playerMovement.CanMove = true;
        playerAttack.CanAttack = true;
        animator.SetBool("Attack", false);
    }
}
