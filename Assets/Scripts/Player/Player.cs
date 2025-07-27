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

    // Death and respawn
    public Transform respawnPoint;
    public Transform endingPoint;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // make character face right
        isFacingRight = false;
        flip();
    }

    void Update(){
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        endingPoint = GameObject.FindGameObjectWithTag("EndingPoint").transform;
        float input = Input.GetAxis("Horizontal");

        // Apply velocity continuously based on input
        rb.velocity = new Vector2(input*moveSpeed, rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded()){
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce*100));
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

        // fall death
        if (rb.position.y <= -7){
            rb.velocity = Vector2.zero;
            rb.position = respawnPoint.position;
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

    // touch danger death
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Killer")){
            rb.velocity = Vector2.zero;
            rb.position = respawnPoint.position;
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
