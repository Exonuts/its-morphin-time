using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    //public float moveSpeed = 5f;
    public float moveForce;
    public float maxSpeed;
    private Rigidbody2D rb;

    public LayerMask killer;
    public float checkRadius = 0.4f;

    bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // float input = Input.GetAxisRaw("Horizontal");
        // rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        Collider2D hit = Physics2D.OverlapCircle(transform.position,checkRadius,killer);

        if(hit != null) {

            isDead = true;
        }

        float input = Input.GetAxisRaw("Horizontal");
        float clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);

        if(!isDead) {

            if (input != 0)
            {
                // Apply force continuously based on input
                rb.AddForce(Vector2.right * input * moveForce, ForceMode2D.Force);

                // Clamp velocity AFTER applying force
                rb.velocity = new Vector2(clampedX, rb.velocity.y);
            }
            
        } else {

            transform.localScale = new Vector3(0.3f,0.3f,0.3f);
            rb.velocity = new Vector2(0f,-5f);
        }
        
    }
    

    

}
