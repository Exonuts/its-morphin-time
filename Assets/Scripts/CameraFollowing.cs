using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    
    private GameObject target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = 10f;
        target = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.z = transform.position.z; // Lock the camera's Z position
            transform.position = smoothedPosition;
        }
    }

    /*
    public IEnumerator minimise() {

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 80; i++) {

            yield return new WaitForSeconds(0.02f);
            Camera.main.orthographicSize -= 0.1f;

        }

    }
    */
}
