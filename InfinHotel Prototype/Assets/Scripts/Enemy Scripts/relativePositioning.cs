using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class relativePositioning : MonoBehaviour
{
    private Vector2 direction;
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Enemy"))
        {
            Vector2 cramp = collision.transform.position(Space.Self);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.up);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (-transform.up));
    }
}
