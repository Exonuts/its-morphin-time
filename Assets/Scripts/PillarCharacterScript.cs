using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarCharacterScript : MonoBehaviour
{

    public float torqueForce = 5f;
    public float maxAngularSpeed = 100f; // degrees per second
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");

        // Apply torque
        rb.AddTorque(-input * torqueForce, ForceMode2D.Force);

        // Clamp spin speed
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularSpeed, maxAngularSpeed);
    }

}
