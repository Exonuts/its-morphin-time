using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHorizontalColliderScript : MonoBehaviour {
    public bool isTouchingWall = false;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Wall")) {
            isTouchingWall = true;
            //Debug.Log("PillarHorizontalCollider isTouchingWall = true");
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Wall")) {
            isTouchingWall = false;
            //Debug.Log("PillarHorizontalCollider isTouchingWall = false");
        }
    }
}
