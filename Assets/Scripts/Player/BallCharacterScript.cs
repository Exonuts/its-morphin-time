using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCharacterScript : MonoBehaviour
{
    //public float moveSpeed = 5f;
    public float moveForce;
    public float maxSpeed;
    private Rigidbody2D rb;

    // Death and respawn
    public Transform respawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        // float input = Input.GetAxisRaw("Horizontal");
        // rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        float input = Input.GetAxisRaw("Horizontal");

        if (input != 0)
        {
            // Apply force continuously based on input
            rb.AddForce(Vector2.right * input * moveForce, ForceMode2D.Force);

            // Clamp velocity AFTER applying force
            float clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
            rb.velocity = new Vector2(clampedX, rb.velocity.y);
        }
    }
    

    // touch danger death
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Killer")){
            rb.velocity = Vector2.zero;
            rb.position = respawnPoint.position;
        }
    }

}
