using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerAnimationConroller animCont;

    public bool CanAttack { set => canAttack = value; }
    bool canAttack;

    void Start()
    {
        animCont = FindObjectOfType<PlayerAnimationConroller>();
        CanAttack = true;
    }
    void Update()
    {
        AttackDetection();
    }
    void AttackDetection()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            animCont.RunAttack();
        }

        else return;
    }
}