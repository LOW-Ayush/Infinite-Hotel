
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth;
    public Slider healthbar;

    private CircleCollider2D hitBox;
    private SpriteRenderer VisRep;
    private Vector3 offset;
    public Vector2 playerNormal;

    private void Start()
    {
        transform.position = (transform.parent.position);

        MaxHealth = 4;
        CurrentHealth = MaxHealth;
        healthbar.maxValue = MaxHealth;
        healthbar.value = CurrentHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("player damaged");


        //damage effects

        //if health 0, then dead
        if (CurrentHealth == 0)
        {
            Debug.Log("dead... :(");
        }
    }

    void FixedUpdate()
    {

        //represent health on health bar
        healthbar.value = CurrentHealth;

        transform.forward = Vector3.forward;
        Vector3 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Rotation = CursorPos - transform.position;

        //turn based on cursor aim
        float Zrotation = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, Zrotation);

        //facing direction represented as a normal
        playerNormal = new Vector2(Rotation.x,Rotation.y);
        playerNormal = playerNormal.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, playerNormal);
    }
}
