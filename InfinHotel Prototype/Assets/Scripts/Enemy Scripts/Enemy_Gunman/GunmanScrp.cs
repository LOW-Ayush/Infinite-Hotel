using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunmanScrp : MonoBehaviour
{
    public bool dead;
    public float SpawnHealth;
    public Sprite KIAsprite;

    public int AlertLvl;
    public LayerMask targetLayer;
    public LayerMask obstructorLayer;
    public bool LineofSight;
    public float VisionCone;
    public static bool Aware;
    public bool inSight;
    public float Currentangle;
    private enemyshooting fire;

    private float timer;
    private Rigidbody2D rb;

    [SerializeField] private GunmanScrp Gunman_Scrp;
    private RaycastHit2D Seen;
    public static Vector2 pointSeen;
    public bool Closest;

    private NavMeshAgent agent;

    //public float ReactionTime;
    public float turnSpeed;

    void Start()
    {
        EnemyHealth healthmanager = gameObject.GetComponent<EnemyHealth>();
        healthmanager.SetupHealthManager(SpawnHealth, KIAsprite);

        AlertLvl = 1;
        rb = GetComponent<Rigidbody2D>();
        Closest = false;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        fire = gameObject.GetComponentInChildren<enemyshooting>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            FOV();
        }
    }

    private void FOV()
    {
        Vector3 Target = GameObject.Find("Player").GetComponent<Transform>().position;
        Vector2 Direction = (Target - transform.position).normalized;
        float zRotation = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
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
            agent.isStopped = true;
            Quaternion lookRotation = Quaternion.LookRotation(transform.forward, Direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
            fire.Shoot();
        }
        else
        {
            inSight = false;
        }


        //lost sight of player
        if (Aware && !inSight)
        {
            Vector2 direction = pointSeen - new Vector2(transform.position.x, transform.position.y);
            Quaternion lookRotation = Quaternion.LookRotation(transform.forward, direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

            //Checking if they are the closest person
            CheckClosest();

            //if they are the closest go to last seen location
            if (Closest)
            {
                agent.isStopped = false;
                agent.SetDestination(pointSeen);
                AlertLvl = 2;
                if (new Vector2(transform.position.x, transform.position.y) == pointSeen)
                {
                    SearchPatterns();
                }
            }
            //else move to establish line of sight
            else
            {
                agent.isStopped = false;
                agent.SetDestination(pointSeen);
                AlertLvl = 2;

                Vector2 towardsPS = pointSeen - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                float distPS = Vector2.Distance(transform.position, pointSeen);
                //Raycasting for to check for clear line of sight
                if (!Physics2D.Raycast(transform.position, towardsPS, distPS, obstructorLayer))
                {
                    agent.isStopped = true;

                }

            }
        }
    }

    //looking for player
    private void SearchPatterns()
    {
        Debug.Log("Executing search...");   
    }
    private void CheckClosest()
    {
        int iter = 0;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        float[] dists = new float[objs.Length];
        foreach (GameObject obj in objs)
        {
            dists[iter] = Vector2.Distance(obj.transform.position, pointSeen);
            iter++;
        }

        //moving the lowesr value into the front of the array
        float value = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < dists.Length; i++)
        {
            if (dists[i] < value)
            {
                index = i;
                value = dists[i];
            }
        }

        //if they are that value they are the closest
        if (dists[index] == Vector2.Distance(gameObject.transform.position, pointSeen))
        {
            Closest = true;
        }
        else
        {
            Closest = false;
        }
        
    }
    
    public void Death()
    {
        GameObject.Find("Status Marker").SetActive(false);
        gameObject.GetComponent<GunmanScrp>().enabled = false;
    }





    //gizmos
    private void OnDrawGizmos()
    {
        if (pointSeen != null)
        {
            Gizmos.DrawIcon(pointSeen, "Point Seen");
            Gizmos.DrawLine(transform.position, pointSeen);

            if (Closest)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, 0.05f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, 0.05f);
            }
        }
    }
}
