using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour{

    // Physics
    public float moveSpeed = 1;
    public float jumpForce = 1;
    private Rigidbody2D rb;

    // Jump detection
    public Vector2 raycastBoxSize;
    public float raycastDistance;
    public LayerMask terrainLayer;

    // Death and respawn
    public Transform respawnPoint;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        float input = Input.GetAxis("Horizontal");

        // Apply velocity continuously based on input
        rb.velocity = new Vector2(input*moveSpeed, rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded()){
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce*100));
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
}
