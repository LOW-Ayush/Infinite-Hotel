using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private Transform targeting;
    private Rigidbody2D rb;
    public float bulletspeed;
    private float timer;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        targeting = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = targeting.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletspeed;
        float Zrotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, Zrotation);

        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //out of bounds = destroyed
        Vector2 Distance = transform.position - targeting.position;
        if (Distance.magnitude > 5)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D objecthit)
    {
        if (objecthit.gameObject.CompareTag("Player") == true)
        {
            //player takes damage
            PlayerScript player = objecthit.GetComponent<PlayerScript>();
            player.TakeDamage(damage);


            //bullet is destroyed
            Destroy(gameObject);
        }
        if (objecthit.gameObject.CompareTag("Obstructor") == true)
        {
            //wall impact effect
            Destroy(gameObject);
        }
    }
}
