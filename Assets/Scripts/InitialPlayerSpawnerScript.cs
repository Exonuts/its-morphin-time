using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialPlayerSpawnerScript : MonoBehaviour
{
    public GameObject toSpawn;
    public Vector2 spawnPoint;
    public Collider2D playerCollider;

    void Start()
    {
        DontDestroyOnLoad(this);
        /* Remove any existing player
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
        if (existingPlayer != null) {
            Destroy(existingPlayer);
        } */
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }

    public void StartRun() {
        // Spawn the form at the specified position
        GameObject newPlayer = Instantiate(toSpawn, spawnPoint, Quaternion.identity);
        newPlayer.tag = "Player";
    }

    void Update(){
        if (SceneManager.GetActiveScene().buildIndex == 1){
            Debug.Log("asdhubyahsjd");
        } else {
            GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
            if (existingPlayer == null) {
                StartRun();
            }
        }
    }
}
