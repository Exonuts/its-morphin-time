using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool grounded;
    private Rigidbody2D rb;

    private Animator anim;
    private bool isFacingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // make character face right
        isFacingRight = false;
        flip();
    }

    void Update()
    {
        // float input = Input.GetAxisRaw("Horizontal");
        // rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        float input = Input.GetAxisRaw("Horizontal");

        // Apply velocity continuously based on input
        rb.velocity = new Vector2(input*moveSpeed, rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce*10));
        }

        // Running animation
        if (input != 0){
            anim.SetBool("isRunning",true);
        }else{
            anim.SetBool("isRunning",false);
        }

        // flipping model
        if (input > 0 && !isFacingRight){
            flip();
        }else if(input < 0 && isFacingRight){
            flip();
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

    // flip model
    private void flip(){
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    

    

}
