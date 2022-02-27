using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public GameObject body;
    Animator animator;

    void Start()
    {
        //body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //print(body.transform.rotation.z);
        animator.SetFloat("SpeedHor", body.transform.rotation.z);
    }
}
