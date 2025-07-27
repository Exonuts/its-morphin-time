using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlayerSpawnerScript : MonoBehaviour
{
    public GameObject toSpawn;
    public Vector2 spawnPoint;

    void Start()
    {
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
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
        if (existingPlayer == null) {
            StartRun();
        }
    }
}
