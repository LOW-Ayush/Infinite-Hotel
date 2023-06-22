using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : MonoBehaviour
{
    public GameObject bullet;
    public Transform firedFrom;
    public bool canFire;
    private float timer;
    public float fireRate;
    void Update()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, firedFrom.position, Quaternion.identity);
        }
    }
}
