using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunmanScrp : MonoBehaviour
{
    public int AlertLvl;
    public LayerMask targetLayer;
    public LayerMask obstructorLayer;
    public bool LineofSight;
    public float VisionCone;
    public static bool Aware;
    public bool inSight;
    public float Currentangle;

    private float timer;
    private Rigidbody2D rb;

    [SerializeField] private GunmanScrp Gunman_Scrp;
    private RaycastHit2D Seen;
    public static Vector2 pointSeen;
    public bool Closest;

    private NavMeshAgent agent;

    //public float ReactionTime;
    //public float turnSpeed;
    //public bool Visible;

    void Start()
    {
        AlertLvl = 1;
        rb = GetComponent<Rigidbody2D>();
        Closest = false;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FOV();
    }

    private void FOV()
    {
        Vector3 Target = GameObject.Find("Player").GetComponent<Transform>().position;
        Vector3 Direction = Target - transform.position;
        float Zrotation = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        float Distance = Vector2.Distance(transform.position, Target);

        //Raycasting for to check for clear line of sight
        if (!Physics2D.Raycast(transform.position, Direction, Distance, obstructorLayer))
        {
            LineofSight = true;
        }
        else
        {
            LineofSight = false;
        }

        //turning to face player if within range and have line of sight
        Currentangle = Vector3.Angle(Direction, transform.up);
        if (Vector3.Angle(Direction, transform.up) < VisionCone && LineofSight)
        {
            //enemy is aware of the player
            Aware = true;
            AlertLvl = 3;
            inSight = true;

            Seen = Physics2D.Raycast(transform.position, Direction, Distance, targetLayer);
            pointSeen = new Vector2(Seen.transform.position.x, Seen.transform.position.y);

            //turn and shoot at player as they are within sight
            rb.MoveRotation(Zrotation);
            agent.isStopped = true;
            gameObject.BroadcastMessage("Shoot");

        }
        else
        {
            inSight = false;
        }


        //lost sight of player
        if (Aware && !inSight)
        {
            Vector2 direction = pointSeen - new Vector2(transform.position.x, transform.position.y);
            transform.up = direction;
            agent.isStopped = false;
            agent.SetDestination(pointSeen);
            AlertLvl = 2;
            if (new Vector2(transform.position.x, transform.position.y) == pointSeen)
            {
                SearchPatterns();
            }
        }
    }

    //going to last seen location
    private void Investigate(Vector2 location)
    {
       
    }

    //looking for player
    private void SearchPatterns()
    {
        Debug.Log("Executing search...");   
    }

    private void OnDrawGizmos()
    {
        if (pointSeen != null)
        {
            Gizmos.DrawIcon(pointSeen, "Point Seen");
            Gizmos.DrawLine(transform.position, pointSeen);
        }
    }
}
