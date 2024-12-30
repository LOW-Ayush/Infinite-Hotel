using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class GunmanScrp : MonoBehaviour
{
    public float SpawnHealth;
    public Sprite KIAsprite;

    public int AlertLvl;
    public LayerMask targetLayer;
    public LayerMask obstructorLayer;
    public bool LineofSight;
    public float VisionCone;
    public static bool Aware;
    public bool inSight;

    public float reactionTime;
    private float timer;
    static public bool startle;
    private enemyshooting fire;

    [SerializeField] private GunmanScrp Gunman_Scrp;
    private RaycastHit2D Seen;
    public static Vector2 pointSeen;
    public bool Closest;
    static public bool search;
    public bool check;
    private Vector2 searchPoint;

    private NavMeshAgent agent;


    //public float ReactionTime;
    public float turnSpeed;

    void Start()
    {
        EnemyHealth healthmanager = gameObject.GetComponent<EnemyHealth>();
        healthmanager.SetupHealthManager(SpawnHealth, KIAsprite);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        fire = gameObject.GetComponentInChildren<enemyshooting>();

        //reset all values
        Closest = false;
        AlertLvl = 1;
        Aware = false;
        pointSeen = new Vector2(0,0);
        startle = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 Target = GameObject.Find("Player").GetComponent<Transform>().position;
        Vector2 Direction = (Target - transform.position).normalized;
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

        //become aware of the player if within vision cone
        if (Vector3.Angle(Direction, transform.up) < VisionCone && LineofSight)
        {
            //enemy is aware of the player
            Aware = true;
            AlertLvl = 3;
            inSight = true;
            search = false;

            Seen = Physics2D.Raycast(transform.position, Direction, Distance, targetLayer);
            pointSeen = new Vector2(Seen.transform.position.x, Seen.transform.position.y);

            //engage player
            timer += Time.deltaTime;
            if (startle)
            {
                if (timer > 0.7f)
                {
                    AgroState(Direction);
                    startle = false;
                }
            }
            else if (timer > reactionTime)
            {
                AgroState(Direction);
            }
        }
        else
        {
            inSight = false;
        }


        //lost sight of player
        if (Aware && !inSight && !search)
        {
            ChaseState();
        }

        if (search)
        {
            agent.isStopped = false;
            if (!check)
            {
                searchPoint = GenPoint();
                Debug.Log("Executing search...");
                check = true;
            }
            if (check)
            {
                Vector2 local; local = (searchPoint - new Vector2(transform.position.x, transform.position.y)).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(transform.forward, local);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
                agent.SetDestination(searchPoint);
            }
            if (new Vector2(transform.position.x, transform.position.y) == searchPoint)
            {
                check = false;
            }
        }
    }


    private void AgroState(Vector2 Direction)
    {
        agent.isStopped = true;
        Quaternion lookRotation = Quaternion.LookRotation(transform.forward, Direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        fire.Shoot();
    }

    private void ChaseState()
    {

        Vector2 direction = pointSeen - new Vector2(transform.position.x, transform.position.y);
        Quaternion lookRotation = Quaternion.LookRotation(transform.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        check = false;

        //Checking if they are the closest person
        CheckClosest(pointSeen);

        //if they are the closest go to last seen location
        if (Closest)
        {
            agent.isStopped = false;
            agent.SetDestination(pointSeen);
            AlertLvl = 2;
            if ( Vector2.Distance(new Vector2(transform.position.x, transform.position.y), pointSeen) < 0.03f)
            {
                search = true;
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

    private Vector2 GenPoint()
    {
        //generate random position within bounds 
        Vector2 Rpoint;
        Vector2 loc;
        Rpoint = transform.position + Random.insideUnitSphere;
        NavMesh.SamplePosition(Rpoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas);
        loc = hit.position;
        return loc;
    }

    private void CheckClosest( Vector2 checkPos)
    {
        int iter = 0;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        float[] dists = new float[objs.Length];
        foreach (GameObject obj in objs)
        {
            dists[iter] = Vector2.Distance(obj.transform.position, checkPos);
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
        gameObject.tag = ("Corpse");
        gameObject.layer = 12;
        agent.isStopped = true;
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(searchPoint, 0.02f);
        
    }
}
