using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    private Rigidbody2D rb;
    public LayerMask target;
    public Sprite[] birdAnimations;
    public float searchRadius = 20f;
    private bool idle = false;
    private bool needsNewAnimation = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = birdAnimations[0];
        transform.position = new Vector3(-7.91f,-4.7f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D isInProximity = Physics2D.OverlapCircle(transform.position,searchRadius,target);
        if(isInProximity != null) {

            rb.gravityScale = 0;

            if(needsNewAnimation) {

                StartCoroutine(changeAnimation());
                
            }
            idle = false;
            GameObject objectTarget = isInProximity.gameObject;

            Vector2 direction = objectTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = Vector3.MoveTowards(transform.position, objectTarget.transform.position, 5f * Time.deltaTime);

            BalloonScript b = objectTarget.GetComponent<BalloonScript>();

            if(b.regenerating == true) {

                idle = true;
                needsNewAnimation = true;
                transform.position = new Vector3(-7.91f,-4.7f,0f);
            }

        } else {

            if(!idle) {

                needsNewAnimation = true;
                GetComponent<SpriteRenderer>().sprite = birdAnimations[0];
                rb.gravityScale = 1;
                StartCoroutine(moveRandomly());
                idle = true;
            }

        }

        if(transform.position.y < -6f) {

            Vector3 curr = transform.position;
            curr.y = -6f;
            transform.position = curr;
        }
    }

    public IEnumerator moveRandomly() {

        yield return new WaitForSeconds(1f);
        Vector3 currPos = transform.position;
        float totalDist = Random.Range(-0.5f,0.5f);

        if(totalDist < 0f) {

            transform.rotation = Quaternion.Euler(0f,180f,0f);
        } else {

            transform.rotation = Quaternion.Euler(0f,0f,0f);
        }

        for (int i = 0; i < 10; i++) {

            currPos.x += (totalDist);
            transform.position = currPos;
            yield return new WaitForSeconds(0.2f);
        }
        idle = false;

    }

    public IEnumerator changeAnimation() {

        needsNewAnimation = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = birdAnimations[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = birdAnimations[0];
        needsNewAnimation = true;
    }
}
