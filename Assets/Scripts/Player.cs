using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //public float moveSpeed = 5f;
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    private bool grounded;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // float input = Input.GetAxisRaw("Horizontal");
        // rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        float input = Input.GetAxisRaw("Horizontal");

        // Apply force continuously based on input
        rb.AddForce(Vector2.right * input * moveForce, ForceMode2D.Force);

        // Clamp velocity AFTER applying force
        float clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(clampedX, rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce*10));
        }

        // Animation
        if (input != 0){
            anim.SetBool("isRunning",true);
        }else{
            anim.SetBool("isRunning",false);
        }
    }

    // grounded detection
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Ground")){
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.CompareTag("Ground")){
            grounded = false;
        }
    }
    

    

}
