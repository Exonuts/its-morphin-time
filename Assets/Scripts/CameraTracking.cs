using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    
    private GameObject target;
    public float smoothSpeed;
    public float size;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = size;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.z = transform.position.z; // Lock the camera's Z position
            if(smoothedPosition.y < 0){
                smoothedPosition.y = 0;
            }
            transform.position = smoothedPosition;
        }
    }
}
