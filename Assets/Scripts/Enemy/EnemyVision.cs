using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    EnemyBehaviour body;
    GameObject sightOffset;
    Vector3 here;
    Vector3 there;
    Vector3 lastKnownPos;
    public Vector3 FuturePos { get { return futurePos; } }
    Vector3 futurePos;

    int wallMask;
    void Start()
    {
        body = GetComponentInParent<EnemyBehaviour>();
        sightOffset = GetComponentInChildren<EnemyLocator>().gameObject;
        //indicator1.transform.position = sightOffset.transform.position;
        wallMask = LayerMask.NameToLayer("Wall");
        //print(wallMask);
    }

    // Update is called once per frame
    void Update()
    {
        here = transform.position;
        there = body.player.transform.position;

        //indicator1.transform.position = sightOffset.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(sightOffset.transform.position, (there - here), Vector3.Distance(here, there));
        //print(transform.position + " // " + (here + (there - here) * 0.2f) + " // " +
        //  "\n" + (there - here) + " Distance 1 " + Vector3.Distance(there, here) + " distance 2 " + Vector3.Distance((here + (there - here) * 0.3f), here));

        //indicator1.transform.position = here;
        if (hit.collider != null)
        {
            //indicator2.transform.position = hit.point;

            if (hit.collider.gameObject.tag == "Player")
            {
                if (body.State != EnemyState.Follow)
                {
                    body.ChangeState(EnemyState.Follow);
                    return;
                }
                else
                {
                    lastKnownPos = there;
                    //indicator3.transform.position = lastKnownPos;
                    futurePos = FuturePosition(1);
                    //indicator4.transform.position = futurePos;
                }
            }
            if (hit.collider.gameObject.tag == "Wall" && body.State == EnemyState.Follow)
            {
                body.ChangeState(EnemyState.Search);
                print("Bonk");
                //body.Bonk();
            }
        }

    }

    public bool LookForWall(Vector3 point, Vector3 from)
    {

        RaycastHit2D hit = Physics2D.Raycast(from, (point - from), Vector3.Distance(from, point), 1 << wallMask);
        //        print("he");
        if (hit.collider != null)
        {
            print("ping");
            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Wall")
                return true;
            else
                return false;
        }
        return false;
    }
    public Vector2 LookForWallNormal(Vector2 point, Vector2 from)
    {
        //print("ha");
        RaycastHit2D hit = Physics2D.Raycast(from, (point - from), Vector3.Distance(from, point), 1 << wallMask);

        if (hit.collider != null)
        {
            //            print(hit.normal);
            //            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Wall")
            {
                futurePos = (hit.normal * 0.5f) + hit.point;
                return futurePos;
            }
            else
                return Vector2.zero;
        }
        return Vector2.zero;
    }
    public bool LookForPlayer(Vector3 point)
    {
        RaycastHit2D hit = Physics2D.Raycast(sightOffset.transform.position, (point - here), Vector3.Distance(here, point));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
                return true;
            else
                return false;
        }
        return false;
    }
    Vector2 FuturePosition(float time)
    {
        Vector2 bodyPos = body.playerBody.position;
        float rDrag = Mathf.Clamp01(1.0f - (body.playerBody.drag * Time.fixedDeltaTime));
        Vector2 velocityPerFrame = body.playerBody.velocity;
        for (int i = 0; i < time / Time.fixedDeltaTime; i++)
        {
            velocityPerFrame *= rDrag;
            bodyPos += (velocityPerFrame * Time.fixedDeltaTime);
        }
        return bodyPos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            //body.Bonk();

        }
    }
}