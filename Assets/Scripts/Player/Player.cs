using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour{

    // Physics
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;

    // Jump detection
    public Vector2 raycastBoxSize;
    public float raycastDistance;
    public LayerMask terrainLayer;

    // Model and animation
    private Animator anim;
    private bool isFacingRight;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // make character face right
        isFacingRight = false;
        flip();
    }

    void Update(){
        float input = Input.GetAxis("Horizontal");

        // Apply velocity continuously based on input
        rb.velocity = new Vector2(input*moveSpeed, rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded()){
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce*240));
        }

        // Running and jumping animation
        if (input != 0){
            anim.SetBool("isRunning",true);
        }else{
            anim.SetBool("isRunning",false);
        }
        anim.SetBool("isJumping",!isGrounded());

        // flipping model
        if (input > 0 && !isFacingRight){
            flip();
        }else if(input < 0 && isFacingRight){
            flip();
        }
    }

    // grounded detection raycast method
    public bool isGrounded(){
        if(Physics2D.BoxCast(transform.position, raycastBoxSize, 0, -transform.up, raycastDistance, terrainLayer)){
            return true;
        }else{
            return false;
        }
    }

    // grounded detection visualisation
    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position-transform.up*raycastDistance, raycastBoxSize);
    }

    // grounded detection collison method
/*    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Ground")){
            Vector3 normal = other.GetContact(0).normal;
            if(normal == Vector3.up){
                grounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.CompareTag("Ground")){
            grounded = false;
        }
    } */

    // flip model
    private void flip(){
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

}
