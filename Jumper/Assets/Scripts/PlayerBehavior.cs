
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float xInput;
    private Rigidbody2D rb;

    public int moveSpeed;
    public int MaxSpeed;

    public int bounceForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal") * moveSpeed;

        if (rb.velocity.x > MaxSpeed)
        {
            rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -MaxSpeed)
        {
            rb.velocity = new Vector2(-MaxSpeed, rb.velocity.y);
        }

        float leftBoundry = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float rightBoundry = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        if (transform.position.x < leftBoundry - 1)
            transform.position = new Vector3(rightBoundry, transform.position.y, 0);
        else if (transform.position.x > rightBoundry + 1)
            transform.position = new Vector3(leftBoundry, transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(xInput, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse);
        }
    }
}
