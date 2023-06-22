using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScrp : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 movement;
    private RaycastHit2D contact;
    private CircleCollider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponentInChildren<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = (Input.GetAxisRaw("Horizontal") * moveSpeed);
        float y = (Input.GetAxisRaw("Vertical") * moveSpeed);

        //reset the movement to the new data
        movement = new Vector3(x,y,0);


        //checks for phsyical contact with anything, if null it allows movement in that axis.
        contact = Physics2D.CircleCast(transform.position, hitbox.radius, new Vector2(movement.x, 0), Mathf.Abs(movement.x * Time.deltaTime), LayerMask.GetMask("Actors", "Obstructors"));
        if (contact.collider == null)
        {
            //movement
            transform.Translate(movement.x * Time.deltaTime, 0, 0);
        }
        contact = Physics2D.CircleCast(transform.position, hitbox.radius, new Vector2(0, movement.y), Mathf.Abs(movement.y * Time.deltaTime), LayerMask.GetMask("Actors", "Obstructors"));
        if (contact.collider == null)
        {
            //movement
            transform.Translate(0, movement.y * Time.deltaTime, 0);
        }
    }
}
