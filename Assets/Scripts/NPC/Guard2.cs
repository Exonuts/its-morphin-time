using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard2 : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rb;
    private Transform destination;
    
    public float speed;
    public float waitTime;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        destination = pointB.transform;

    }

    // Update is called once per frame
    void Update() {
        if(destination ==  pointB.transform){
            rb.velocity = new Vector2(speed, 0);
        } else {
            rb.velocity = new Vector2(-speed, 0);
        }
        if(Vector2.Distance(transform.position, destination.position) < 1 && destination == pointB.transform){
            destination = pointA.transform;
        }
        if(Vector2.Distance(transform.position, destination.position) < 1 && destination == pointA.transform){
            destination = pointB.transform;
        }
    }


    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
