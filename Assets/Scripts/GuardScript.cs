using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour
{

    public float moveSpeed;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        
        StartCoroutine(WalkLeft(3));
        StartCoroutine(WalkRight(3));
    }

    IEnumerator WalkLeft(int distance)
    {
        float moved = 0f;
        Vector3 originalPosition = transform.position;

        // Face left
        spriteRenderer.flipX = false;

        while (moved < distance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.Translate(Vector3.left * step);
            moved += step;
            yield return null;
        }
    }

    IEnumerator WalkRight(int distance)
    {
        float moved = 0f;
        Vector3 originalPosition = transform.position;

        // Flip sprite to face right
        spriteRenderer.flipX = true;

        while (moved < distance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.Translate(Vector3.right * step);
            moved += step;
            yield return null;
        }
    }


    


}
