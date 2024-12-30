
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class bullet_script : MonoBehaviour
{
    public float damage;

    private Transform targeting;
    private Transform player;
    private Rigidbody2D rb;
    public float bulletspeed;
    public Sprite splash;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        targeting = GameObject.Find("Crosshair").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = targeting.position - transform.position;    
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletspeed;
        transform.rotation = player.rotation;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D objecthit)
    { 
        string tag = objecthit.tag;
        if (tag == "Enemy")
        {
            //reduce enemy health
            EnemyHealth enemyHealth = objecthit.GetComponent<EnemyHealth>();
            enemyHealth.ReduceHealth(damage);

            Destroy(gameObject);
        }
        else if (tag == "Obstructor")
        {
            StartCoroutine(SplashEffect());
        }
    }

    IEnumerator SplashEffect()
    {
        rb.velocity = Vector3.zero;
        spriteRenderer.sprite = splash;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
