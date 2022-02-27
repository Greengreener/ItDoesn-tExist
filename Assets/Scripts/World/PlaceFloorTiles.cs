using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFloorTiles : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform startPos;
    Vector3 pos;
    int rowLength = 40;
    int columHeight = 20;

    // Start is called before the first frame update
    void Start()
    {

        for (int c = 1; c < columHeight; c++)
        {
            pos = startPos.position;
            pos -= new Vector3(0, c * 0.64f);
            for (int r = 0; r < rowLength; r++)
            {
                pos = startPos.position;
                pos -= new Vector3(0, c * 0.64f);
                pos += new Vector3((r * 0.64f), 0, 0);
                Instantiate(tilePrefab, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
