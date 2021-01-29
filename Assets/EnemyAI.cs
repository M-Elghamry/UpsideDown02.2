using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyMovement2D EnemyMovementScript;
    public Transform EnemyTransform;
    public Collider2D EnemyCollider;
    public GameObject PointA;
    public GameObject PointB;

    private GameObject PlayerReference;
    private bool InPlayerRange = false;

    public float MovementSpeed;
    public float PlayerChaseSpeed;
    public float JumpSpeed;
    
    private string CurrentPoint = "PointA";
    private Vector2 DirectionA;
    private Vector2 DirectionB;

    private bool TouchingSomething = false;

    private void Awake()
    {
        RecalculateWaypointDirections();
    }

    public void RecalculateWaypointDirections() //recalculates the directions for both waypoints
    {
        DirectionA = new Vector2(GetDirection(EnemyTransform.position, PointA.GetComponent<Transform>().position).x, Vector2.zero.y);
        DirectionB = new Vector2(GetDirection(EnemyTransform.position, PointB.GetComponent<Transform>().position).x, Vector2.zero.y);
    }
    
    void Update()
    {
        if (Random.Range(0, 500) == 1 && TouchingSomething) // randomly lets the enemy jump
        {
            EnemyMovementScript.Jump(JumpSpeed);
        }
        

        if(CurrentPoint == "PointA") //sets directional velocity depending on the active chased object (PointA, PointB, Player)
        {
            EnemyMovementScript.SetHorizontalMovement(DirectionA, MovementSpeed);
        }

        if (CurrentPoint == "PointB")
        {
            EnemyMovementScript.SetHorizontalMovement(DirectionB, MovementSpeed);
        }

        if(CurrentPoint == "Player")
        {
            EnemyMovementScript.SetHorizontalMovement(new Vector2(PlayerReference.GetComponent<Transform>().position.x - EnemyTransform.position.x, Vector2.zero.y), PlayerChaseSpeed);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) //sets the currently chased point to "Player" (if touching player), which will be used in the Update method
        {
            PlayerReference = collision.gameObject;
            InPlayerRange = true; //tells the script that object is in the range of the player
            CurrentPoint = "Player";
        }

        if (!InPlayerRange) //only triggered if the object isnt currently chasing the player
        {
            if (collision.gameObject == PointA) //if the object touches one of the Point A or B colliders it starts to move in the direction of the other point
            {
                CurrentPoint = "PointB";
                EnemyMovementScript.SetHorizontalMovement(Vector2.zero, 0);
                RecalculateWaypointDirections();
            }
            else if (collision.gameObject == PointB)
            {
                CurrentPoint = "PointA";
                EnemyMovementScript.SetHorizontalMovement(Vector2.zero, 0);
                RecalculateWaypointDirections();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //sets the InPlayerRange var to false if not in the trigger of the player
    {
        if (collision.CompareTag("Player"))
            InPlayerRange = false;
    }




    private Vector2 GetDirection(Vector2 EnemyPosition, Vector2 PointPosition) //get normalized direction from one point to another
    {
        return (new Vector2(PointPosition.x - EnemyPosition.x, PointPosition.y - EnemyPosition.y)).normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision) //checking for collisions with other enemys and chnaging velocity on collision
    {
        TouchingSomething = true;

        if (collision.collider.tag == "Enemy")
        {
            if (CurrentPoint == "PointA")
            {
                CurrentPoint = "PointB";
                EnemyMovementScript.SetHorizontalMovement(Vector2.zero, 0);
            }
            else if (CurrentPoint == "PointB")
            {
                CurrentPoint = "PointA";
                EnemyMovementScript.SetHorizontalMovement(Vector2.zero, 0);
            }
        }
        
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        TouchingSomething = false;
    }



}
