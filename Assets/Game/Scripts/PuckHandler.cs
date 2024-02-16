using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckHandler : MonoBehaviour
{
    public Vector2 velocity;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        velocity = rb.velocity;
    }
}
