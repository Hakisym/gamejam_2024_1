using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float force;
    private Vector2 currVel;
    private Vector2 prevPos;
    private float speed;

    void Update()
    {
        MouseFollow();
        CalcVelocity();
    }

    private void CalcVelocity()
    {
        currVel = ((Vector2)transform.position - prevPos) / Time.deltaTime;
        speed = currVel.magnitude;
        speed = Mathf.Clamp(speed, 0, 2);

        prevPos = transform.position;
    }

    private void MouseFollow()
    {
        var pos = Input.mousePosition;
        pos.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitable"))
        {
            var dir = collision.ClosestPoint(collision.transform.position) - (Vector2)transform.position;
            collision.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force * speed, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hitable"))
        {
            Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
            collidingRB.velocity = Vector3.Reflect(collision.transform.GetComponent<PuckHandler>().velocity, -collision.contacts[0].normal);

            var dir = collision.contacts[0].point - (Vector2)transform.position;
            collision.transform.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force * speed, ForceMode2D.Impulse);
        }
    }
}
