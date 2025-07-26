using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlayerSpawnerScript : MonoBehaviour
{
    public GameObject pillarPrefab;
    public Vector2 spawnPoint = Vector2.zero; // Set this directly in the Inspector

    void Start()
    {
        StartRun();
    }

    public void StartRun()
    {
        // Remove any existing player
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
        if (existingPlayer != null) {
            Destroy(existingPlayer);
        }

        // Spawn the pillar player at the specified position
        GameObject newPlayer = Instantiate(pillarPrefab, spawnPoint, Quaternion.identity);
        newPlayer.tag = "Player";
    }
}
