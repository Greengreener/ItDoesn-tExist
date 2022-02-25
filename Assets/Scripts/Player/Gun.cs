using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject player;
    Quaternion gunRotation;
    Camera cam;


    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Quaternion gunRotation = Quaternion.AngleAxis(gunAngle, Vector3.forward);
        this.transform.rotation = gunRotation;
    }
}
