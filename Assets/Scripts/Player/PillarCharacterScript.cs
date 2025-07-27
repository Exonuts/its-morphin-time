using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarCharacterScript : MonoBehaviour
{

    public float torqueForce = 5f;
    public float maxAngularSpeed = 100f; // degrees per second
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
        float input = Input.GetAxisRaw("Horizontal");

        // Apply torque
        rb.AddTorque(-input * torqueForce, ForceMode2D.Force);

        // Clamp spin speed
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularSpeed, maxAngularSpeed);
    }


    // touch danger death
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Killer")){
            BeeManager.Instance.removeBees();
            Destroy(gameObject);
        }
    }

}
