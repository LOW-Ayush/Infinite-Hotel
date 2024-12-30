using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firedFrom;
    private bool resetFire;
    private float timer;
    public float fireRate;
    public int bulletCount;
    public int magCap;


    void Update()
    {
        if (bulletCount < magCap)
        {
            if (!resetFire)
            {
                timer += Time.deltaTime;
                if (timer > fireRate)
                {
                    resetFire = true;
                    timer = 0;
                }
            }
        }
        else if (bulletCount >= magCap) 
        {
            StartCoroutine(Reload(magCap));
        }
        

        if (Input.GetMouseButtonDown(0) && resetFire)
        {
            resetFire = false;
            Instantiate(bullet, firedFrom.position, Quaternion.identity);
            bulletCount++;
        }

        if (!Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload(magCap));
        }
    }
    IEnumerator Reload(int count)
    {
        bulletCount = count;
        yield return null;
    }
}
