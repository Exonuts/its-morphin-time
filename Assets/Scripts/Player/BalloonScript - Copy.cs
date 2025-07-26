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
    private float decceleratingVelocity = 0f;

    public bool isDead = false;
    public bool regenerating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        // float input = Input.GetAxisRaw("Horizontal");
        // rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        Collider2D hit = Physics2D.OverlapCircle(transform.position,checkRadius,killer);

        float input = Input.GetAxisRaw("Horizontal");
        
        float clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);

        if(hit != null || Input.GetKey(KeyCode.Space)) {

            transform.localScale = new Vector3(0.3f,0.3f,0.3f);
            isDead = true;
            decceleratingVelocity = clampedX;
        }

        if(!isDead) {

            if (input != 0)
            {
                // Apply force continuously based on input
                rb.AddForce(Vector2.right * input * moveForce, ForceMode2D.Force);

                // Clamp velocity AFTER applying force
                rb.velocity = new Vector2(clampedX, rb.velocity.y);
            }
            
        } else {

            rb.velocity = new Vector2(decceleratingVelocity,-5f);
            decceleratingVelocity *= 0.98f;

            if(transform.position.y < -6f) {

                if(!regenerating) {

                    StartCoroutine(regenerate());
                }
            
            }
        }
        
    }

    public IEnumerator regenerate() {

        regenerating = true;
        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector3(1f,1f,1f);
        transform.position = new Vector3(-32f,-3f,0f);
        isDead = false;
        regenerating = false;

    }
    

    

}
