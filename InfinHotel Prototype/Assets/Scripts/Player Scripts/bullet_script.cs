using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{
    private Transform targeting;
    private Transform player;
    private Rigidbody2D rb;
    public float bulletspeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        targeting = GameObject.Find("Crosshair").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = targeting.position - transform.position;    
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletspeed;
        transform.rotation = player.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D objecthit)
    {
        if (objecthit.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else if (objecthit.gameObject.CompareTag("Obstructor"))
        {
            Destroy(gameObject);
        }
    }

}
