using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    GameObject[] doorObjects;
    public Transform[] DoorsTransforms;
    private void Start()
    {
        doorObjects = GameObject.FindGameObjectsWithTag("Door");
        List<Transform> tempDoors = new List<Transform>();
        foreach (GameObject ele in doorObjects)
        {
            tempDoors.Add(ele.transform);
        }
        DoorsTransforms = tempDoors.ToArray();
    }

}
