using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float force = 10f;
    public float moveSpeed = 10f;
    public float smoothTime = 0.3f;
    public float coefficientOfRestitution = 0.8f;

    private Vector2 currVel;
    private Vector2 prevPos;
    private Vector2 smoothVelocity;
    private float magnitude;


    void Update()
    {
        MouseFollow();
        CalcVelocity();
    }

    private void MouseFollow()
    {
        var pos = Input.mousePosition;
        pos.z = 10f;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = Vector2.SmoothDamp(transform.position, mousePos, ref smoothVelocity, smoothTime, moveSpeed);
    }

    private void CalcVelocity()
    {
        currVel = ((Vector2)transform.position - prevPos) / Time.deltaTime;
        magnitude = currVel.magnitude;
        magnitude = Mathf.Lerp(0, 1, magnitude/moveSpeed);

        prevPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hitable"))
        {
            //bounce
            Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
            Vector2 newVelocity = Vector3.Reflect(collision.transform.GetComponent<PuckHandler>().velocity, -collision.contacts[0].normal);

            // Apply coefficient of restitution
            newVelocity *= coefficientOfRestitution;
            collidingRB.velocity = newVelocity;

            //hit the ball
            var dir = collision.contacts[0].point - (Vector2)transform.position;
            collision.transform.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force * magnitude, ForceMode2D.Impulse);
        }
    }
}
