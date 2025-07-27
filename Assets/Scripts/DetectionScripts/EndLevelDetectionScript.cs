using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelDetectionScript : MonoBehaviour
{
    Collider2D col2D;
    GameObject player;

    // Start is called before the first frame update
    void Start() {
        col2D = this.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D trigger){
        if (trigger.gameObject.tag == "Player"){
            Destroy(player);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
    }
}
