using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCharacterColliderScript : MonoBehaviour
{
    public bool isTouchingWall = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = true;
            Debug.Log("SmallCharacterCollider isTouchingWall = true");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = false;
            Debug.Log("SmallCharacterCollider isTouchingWall = false");
        }
    }
}
