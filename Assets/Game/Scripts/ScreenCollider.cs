using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    public float coefficientOfRestitution = 0.8f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hitable"))
        {
            //bounce
            Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
            Vector2 newVelocity = Vector3.Reflect(collision.transform.GetComponent<PuckHandler>().velocity, -collision.contacts[0].normal);

            // Apply coefficient of restitution
            newVelocity *= coefficientOfRestitution;
            collidingRB.velocity = newVelocity;
        }
    }
}
