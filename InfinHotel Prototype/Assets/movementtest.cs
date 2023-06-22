using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementtest : MonoBehaviour
{
    public float MoveSpeed;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Player = GameObject.Find("Player").GetComponent<Transform>().position;
        Movetolocation(Player);
    }
    private void Movetolocation(Vector3 location)
    {
        //go to player's last known location
        float Distance = Vector3.Distance(transform.position, location);
        if (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, location, MoveSpeed * Time.deltaTime);
        }

    }
}
