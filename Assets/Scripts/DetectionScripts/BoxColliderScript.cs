using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderScript : MonoBehaviour
{
    public bool isTouchingWall = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = true;
            //Debug.Log("box touching wall");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = false;
            //Debug.Log("box not touching wall");
        }
    }
}
