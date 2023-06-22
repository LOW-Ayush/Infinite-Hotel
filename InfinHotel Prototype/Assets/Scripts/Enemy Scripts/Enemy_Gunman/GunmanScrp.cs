using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunmanScrp : MonoBehaviour
{
    public int AlertLvl;
    public LayerMask targetLayer;
    public LayerMask obstructorLayer;
    public bool LineofSight;
    public float VisionCone;
    public bool Aware;
    public bool inSight;
    public float Currentangle;

    private float timer;
    private Rigidbody2D rb;
    public float MoveSpeed;

    //private RaycastHit2D lastknown;
    private RaycastHit2D Seen;
    public Vector2 pointSeen;
    private Vector2 LastKnownLoc;
    public bool Closest;



    //public float ReactionTime;
    //public float turnSpeed;
    //public bool Visible;

    void Start()
    {
        AlertLvl = 1;
        rb = GetComponent<Rigidbody2D>();
        Closest = false;
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
            //Debug.Log("unobstructed raycast");
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
            //Debug.Log("has seen player");

            //enemy is aware of the player
            Aware = true;
            AlertLvl= 3;
            inSight = true;

            Seen = Physics2D.Raycast(transform.position, Direction, Distance, targetLayer);
            pointSeen = new Vector2(Seen.transform.position.x, Seen.transform.position.y);
            tag.Replace("Enemy", "FoundPlayer");

            //turn and shoot at player as they are within sight
            rb.MoveRotation(Zrotation); 
            gameObject.BroadcastMessage("Shoot");
            //Debug.Log("fire");

            //lastknown = Physics2D.Raycast(transform.position, Direction, Distance, targetLayer);

        }
        else
        {
            inSight = false;
            gameObject.tag = "Enemy";
        }

        //Look to see if other enemys have spotted player
        if (GameObject.FindWithTag("FoundPlayer") != null)
        {
            Aware = true;
        }


        //move
        if (Aware && !inSight)
        {
            GameObject enemySpot = GameObject.FindWithTag("FoundPlayer");
            //LastKnownLoc = enemySpot.GetComponent<GunmanScrp>().pointSeen;

            Investigate(pointSeen);
            /*
            else
            {
                StandBy(GoHere);
            }*/
        }
       
    }
    
    private void Investigate(Vector2 location)
    {
        AlertLvl = 2;
        //go to player's last known location
        float Distance = Vector2.Distance(transform.position, location);
        if (Distance > 0)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, location, MoveSpeed * Time.deltaTime);
        }
        if (Distance == 0 && !inSight)
        {
            //look around for player (THERE MUST BE CLEANER WAY TO DO THIS)
            timer += Time.deltaTime;
            if (timer > 2)
            {
                rb.MoveRotation(rb.rotation + 90);
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer > 2)
            {
                rb.MoveRotation(rb.rotation - 90);
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer > 2)
            {
                rb.MoveRotation(rb.rotation - 90);
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer > 2)
            {
                rb.MoveRotation(rb.rotation + 90);
                timer = 0;
            }
            //ultimately this just makes the fella look right, forward, then left, then forward again.
        }

    }

    private void StandBy(Vector2 location)
    {

    }
}
