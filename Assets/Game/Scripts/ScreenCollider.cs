using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hitable"))
        {
            Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
            collidingRB.velocity = Vector3.Reflect(collision.transform.GetComponent<PuckHandler>().velocity, -collision.contacts[0].normal);
        }
    }
}
