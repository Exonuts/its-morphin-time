using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour
{

    public LayerMask target;
    public LayerMask obstacle;
    public GameObject beehive;
    private Rigidbody2D rb;
    private float speed;

    public float searchRadius = 10f;
    public float objectSearchRadius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        beehive = GameObject.Find("Beehive");
        speed = Random.Range(0.3f,3f);

    }

    // Update is called once per frame
    void Update()
    {
        
        Collider2D isInProximity = Physics2D.OverlapCircle(transform.position,searchRadius,target);
        Collider2D approachingObject = Physics2D.OverlapCircle(transform.position,objectSearchRadius,obstacle);

        if(isInProximity != null) {

            GameObject objectTarget = isInProximity.gameObject;

            Vector2 direction = objectTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(objectTarget.transform.position.x < transform.position.x) {

                transform.rotation = Quaternion.Euler(0, 0, angle+180);
            } else {

                transform.rotation = Quaternion.Euler(0, 180, angle);
                
            }
            transform.position = Vector3.MoveTowards(transform.position, objectTarget.transform.position, speed * Time.deltaTime);

            if(approachingObject != null) {

                Vector3 directionAway = transform.position - approachingObject.gameObject.transform.position;
                Vector3 targetPosition = transform.position + directionAway.normalized;
                transform.position = Vector3.MoveTowards(transform.position,targetPosition,speed * Time.deltaTime);

            }

            BalloonScript b = objectTarget.GetComponent<BalloonScript>();

            if(b.regenerating == true) {

                BeeManager.Instance.beesSummoned = false;
                Destroy(gameObject);
            }

        } else {

            Vector2 direction = beehive.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if(beehive.transform.position.x < transform.position.x) {

                transform.rotation = Quaternion.Euler(0, 0, angle+180);
            } else {

                transform.rotation = Quaternion.Euler(0, 180, angle);
                
            }
           
            transform.position = Vector3.MoveTowards(transform.position, beehive.transform.position, speed * Time.deltaTime);
            
            if(approachingObject != null) {

                Vector3 directionAway = transform.position - approachingObject.gameObject.transform.position;
                Vector3 targetPosition = transform.position + directionAway.normalized;
                transform.position = Vector3.MoveTowards(transform.position,targetPosition,speed * Time.deltaTime);

            }

        }

    }
}
