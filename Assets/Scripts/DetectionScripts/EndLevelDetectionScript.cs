using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelDetectionScript : MonoBehaviour
{
    Collider2D col2D;

    // Start is called before the first frame update
    void Start() {
        col2D = this.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D trigger){
        if (trigger.gameObject.tag == "Player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
    }
}
