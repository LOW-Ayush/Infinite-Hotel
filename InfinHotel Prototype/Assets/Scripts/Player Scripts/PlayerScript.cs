using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CircleCollider2D hitBox;
    private SpriteRenderer VisRep;
    private Vector3 offset;

    private void Start()
    {
        transform.position = (transform.parent.position);
    }

    void FixedUpdate()
    {

        transform.forward = Vector3.forward;
        Vector3 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Rotation = CursorPos - transform.position;

        //turn based on cursor aim
        float Zrotation = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, Zrotation);


    }
}
