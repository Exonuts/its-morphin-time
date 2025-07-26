using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarLowHorizontalColliderScript : MonoBehaviour {
    public bool isTouchingWall = false;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Wall")) {
            isTouchingWall = true;
            Debug.Log("PillarLowHorizontalCollider isTouchingWall = true");
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Wall")) {
            isTouchingWall = false;
            Debug.Log("PillarLowHorizontalCollider isTouchingWall = false");
        }
    }
}
