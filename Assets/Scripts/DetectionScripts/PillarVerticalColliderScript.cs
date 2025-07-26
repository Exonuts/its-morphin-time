using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarVerticalColliderScript : MonoBehaviour {
    public bool isTouchingWall = false;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Wall")) {
            isTouchingWall = true;
            //Debug.Log("PillarVerticalCollider isTouchingWall = true");
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Wall")) {
            isTouchingWall = false;
            //Debug.Log("PillarVerticalCollider isTouchingWall = false");
        }
    }
}
