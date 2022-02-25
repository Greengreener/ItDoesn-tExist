using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    Camera cam;
    GameObject player;
    [SerializeField] float smoothing;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, player.transform.position + new Vector3(0, 0, -10), smoothing);
    }
}
