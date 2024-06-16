using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private double stopDist;
    private float fallBack;
    private string coreSize;
    public string behaviour;

    //detection
    private float visionAngle;
    private float visionRange;
    public LayerMask targetLayer;
    public LayerMask obstructorLayer;
    public static bool Aware;
    private bool inSight;
    private Vector2 pointSeen;

    private AngelWeapon weapon;

    private void Start()
    {
        //reset values
        Aware = false;


        //retrieve core size
        coreSize = GetComponentInParent<AngelCore>().coreSize;


        //determine behaviour
        int num;
        num = Random.Range(1, 4);
        switch (num)
        {
            case 1: //avoidance. stay at max weapon range. no melee
                behaviour = "avoidant";
                stopDist = weapon.Range;

                break;

            case 2: //balanced. fall back if harmed. jump in and out if melee.
                behaviour = "balanced";
                stopDist = 1;
                break;

            case 3: //full aggresion
                behaviour = "aggresive";
                stopDist = 0.5;
                fallBack = 0;
                break ;
        }
    }


    void FixedUpdate()
    {
        //vision and detection
        Vector3 Target = GameObject.Find("Player").GetComponent<Transform>().position;
        Vector2 Direction = (Target - transform.position).normalized;

        //Raycasting for to check for clear line of sight within range and vision cone
        if (!Physics2D.Raycast(transform.position, Direction, visionRange, obstructorLayer) && Vector3.Angle(Direction, transform.up) < visionAngle)
        {
            //enemy is aware of the player
            Aware = true;
            inSight = true;

            //store last seen location
            RaycastHit2D Seen = Physics2D.Raycast(transform.position, Direction, targetLayer);
            pointSeen = new Vector2(Seen.transform.position.x, Seen.transform.position.y);

            weapon.Attack();
            EngagePlayer();

            
        }
        else
        {
            inSight = false;
        }


        //lost sight of player
        if (Aware && !inSight)
        {
            switch (behaviour)
            {
                case "avoidant":

                default:
                    break;
            }
        }
    }
        
    public void EngagePlayer()
    {
        switch (behaviour)
        {
            case "avoidant":
                if (weapon.melee)
                {
                    //move in and out of combat
                }
                else
                {
                    //attack from max range
                }
                break;
            case "balanced":
                if (weapon.melee)
                {
                    //
                }
                else
                {

                }
                break;
            case "aggresive":
                if (weapon.melee)
                {

                }
                else
                {

                }
                break;

            default:
                Debug.Log("Error: EngagePlayer(). " + gameObject.name);
                break;
        }
    }

    public void SetVisionCone(float angle, float range)
    {
        visionAngle = angle;
        visionRange = range;
    }
}
