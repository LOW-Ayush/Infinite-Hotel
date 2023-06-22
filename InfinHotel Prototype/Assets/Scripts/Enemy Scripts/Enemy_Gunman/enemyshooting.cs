using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshooting : MonoBehaviour
{
    public GameObject bullet;
    public bool canFire;
    private float timer;
    public float fireRate;

    //burst mode options set so that we cab use this script for different weapons. CANT GET IT TO WORK YET!!
    /*
    public bool burst;
    public float burstrate;
    public float burstTimer;
     */
    void Update()
    {

    }

    public void Shoot()
    {
            //firing
            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer > fireRate)
                {
                    canFire = true;
                    timer = 0;
                }
                if (canFire)
                {
                    canFire = false;
                    Instantiate(bullet, transform.position, Quaternion.identity);
                }
            }
    }
}
