using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ciricletestscript : MonoBehaviour
{
    private GameObject[] objects;
    private bool startOn;
    public bool isClosest;
    private labGlobal global;
    public GameObject target;
    public Vector2 targetloc;

    // Start is called before the first frame update
    void Start()
    {
        startOn = true;
        isClosest = false;

        target = GameObject.Find("Triangle");
    }

    // Update is called once per frame
    void Update()
    {
        objects = GameObject.FindGameObjectsWithTag("Enemy");
        targetloc = target.transform.position;
        Vector3 direction = targetloc - new Vector2(transform.position.x, transform.position.y);
        float Zrotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        global = gameObject.GetComponentInParent<labGlobal>();
        float dist = Vector2.Distance(transform.position, target.transform.position);
        if (global.closest == dist)
        {
            isClosest = true;
            transform.rotation = Quaternion.Euler(0, 0, Zrotation);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.04f);
        }
        else
        {
            isClosest = false;
            transform.rotation = Quaternion.Euler(0, 0, Zrotation);
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up);

        if (startOn)
        {
            foreach (GameObject enemy in objects)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, enemy.transform.position);
            }
        }

        if (isClosest)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}
