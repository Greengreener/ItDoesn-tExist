using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health { get => health; }
    float health = 40;
    bool death;
    public GameObject head;

    void Start()
    {
        head = GetComponentInParent<EnemyBehaviour>().gameObject;
    }
    void Update()
    {
        this.transform.position = head.transform.position;
    }
    public void DamageZombie(float inputDamage)
    {
        health -= inputDamage;
        CheckDeath();
    }
    void CheckDeath()
    {
        if (health <= 0)
        {
            death = true; // for animation
            KillSelf();
        }
    }
    void KillSelf()
    {
        Destroy(head);
    }
}
