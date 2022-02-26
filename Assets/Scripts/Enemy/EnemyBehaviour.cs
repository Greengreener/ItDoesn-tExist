using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D playerBody;

    Collider2D face;
    Rigidbody2D body;
    EnemyVision eyes;

    Doors worldDoors;

    float speed;
    float attackDistance;

    Vector3 curPos;
    Vector3 playerPos;
    Vector3 targetPos;

    //bool separated;
    bool canMove;
    bool canAttack;

    public EnemyState State { get { return state; } }
    EnemyState state;

    public GameObject nextPos;

    void Start()
    {
        player = FindObjectOfType<PlayerAttack>().gameObject;
        playerBody = player.GetComponent<Rigidbody2D>();
        attackDistance = 0.75f;
        speed = 1f;
        face = this.GetComponentsInChildren<Collider2D>()[1];
        body = GetComponent<Rigidbody2D>();
        eyes = GetComponentInChildren<EnemyVision>();
        worldDoors = FindObjectOfType<Doors>();
        canMove = true;

    }
    //Fix door nodes
    void FixedUpdate()
    {
        curPos = transform.position;
        playerPos = player.transform.position;

        switch (state)
        {
            case EnemyState.Wander:
                if (canMove == true)
                {
                    targetPos = WanderPos();
                    canMove = false;
                }
                if (Vector3.Distance(targetPos, curPos) <= 0.2f && canMove == false)
                    canMove = true;
                MoveVector(targetPos);
                break;
            case EnemyState.Search:
                MoveVector(targetPos);
                if (Vector3.Distance(targetPos, curPos) <= 0.2f)
                    switch (eyes.LookForPlayer(playerPos))
                    {
                        case true:
                            state = EnemyState.Follow;
                            break;
                        case false:
                            if (targetPos == eyes.FuturePos)
                                ChangeState(EnemyState.Wander);
                            else
                            {
                                Vector2 temp = targetPos;
                                targetPos = eyes.FuturePos;
                                Vector3 norm = eyes.LookForWallNormal(targetPos, temp);
                                targetPos = norm;
                            }
                            break;
                    }
                break;
            case EnemyState.Follow:
                targetPos = player.transform.position;
                MoveVector(GetOffsetVector(curPos, targetPos, attackDistance));
                RotateFace(playerPos);
                break;
        }

        //Float check
        if (body.velocity.x <= -0.01f ||
            body.velocity.y <= -0.01f ||
            body.velocity.x >= 0.01f ||
            body.velocity.y >= 0.01f)
            body.velocity = Vector2.zero;
    }
    void MoveVector(Vector3 targetVector)
    {
        transform.position = Vector3.MoveTowards(curPos, targetVector, speed * Time.deltaTime);
    }
    Vector3 GetOffsetVector(Vector3 inputVector, Vector3 targetVector, float distance)
    {
        Vector3 direction = inputVector - targetVector;
        Vector3 targetPos = (direction.normalized) * distance;
        return targetPos + targetVector;
    }
    void RotateFace(Vector3 inputPos)
    {
        Vector2 playerPosV2 = inputPos - curPos;
        float faceAngle = Mathf.Atan2(playerPosV2.y, playerPosV2.x) * Mathf.Rad2Deg;
        Quaternion faceRotation = Quaternion.AngleAxis(faceAngle, Vector3.forward);
        face.gameObject.transform.rotation = faceRotation;
    }
    Vector3 CheckClosestDoorway() // to player
    {
        Transform[] temp = worldDoors.DoorsTransforms;
        float dis = Vector3.Distance(temp[0].position, playerPos);
        int id = 0;
        for (int i = 0; i < temp.Length; i++)
        {
            if (Vector3.Distance(temp[i].position, playerPos) < dis)
            {
                dis = Vector3.Distance(temp[i].position, playerPos);
                id = i;
            }
        }
        return temp[id].position;
    }
    /*
    public void Bonk()
    {
        separated = true;
        targetPos = CheckClosestDoorway();
    }
    */
    Vector3 WanderPos()
    {
        Vector3 curPos = this.transform.position;
        float randDisX = Random.Range(1, 5);
        float randDisY = Random.Range(1, 5);
        Vector3 newPosition = new Vector3(Random.Range(curPos.x - randDisX, curPos.x + randDisX), Random.Range(curPos.y - randDisY, curPos.y + randDisY));
        RotateFace(newPosition);
        nextPos.transform.position = newPosition;
        if (eyes.LookForWall(newPosition, transform.position) == true)
        {
            nextPos.transform.position = eyes.LookForWallNormal(newPosition, transform.position);
            newPosition = eyes.LookForWallNormal(newPosition, transform.position);
            if (eyes.LookForWall(newPosition, transform.position) == true)
                return eyes.LookForWallNormal(newPosition, transform.position);
            else
                return newPosition;
        }
        else
            return newPosition;
    }

    public void ChangeState(EnemyState inputState)
    {
        switch (inputState)
        {
            case EnemyState.Wander:
                print("State now wander");
                state = EnemyState.Wander;
                speed = 0.5f;
                break;
            case EnemyState.Search:
                print("State now search");
                state = EnemyState.Search;
                speed = 3.25f;
                break;
            case EnemyState.Follow:
                print("State now follow");
                state = EnemyState.Follow;
                speed = 3.5f;
                break;
            case EnemyState.Attack:
                print("State now attack");
                state = EnemyState.Attack;
                break;
        }
    }
    //void OnTriggerExit2D(Collider2D other)
    //{ if (other.gameObject.tag == "Enemy") canMove = true; }'

}
public enum EnemyState
{
    Wander,
    Search,
    Follow,
    Attack
}