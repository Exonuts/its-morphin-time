using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheckerGroupScript : MonoBehaviour {

    public List<GameObject> playerPrefabs = new(4);
    private Transform playerTransform; // saved transform of player character

    PillarVerticalColliderScript pvcs;
    PillarHorizontalColliderScript phcs;
    PillarLowHorizontalColliderScript plhcs;
    BoxColliderScript bcs;
    InventoryManagement invMgr;

    [Header("Pillar spawn offset when upright")]
    public float verticalPillarYOffset = 20f; // tweak in Inspector
    public float pillarLowOffset = -10f; // incorporate

    void Start(){
        invMgr = GetComponentInChildren<InventoryManagement>();
        pvcs = GetComponentInChildren<PillarVerticalColliderScript>();
        phcs = GetComponentInChildren<PillarHorizontalColliderScript>();
        plhcs = GetComponentInChildren<PillarLowHorizontalColliderScript>();
        bcs = GetComponentInChildren<BoxColliderScript>();
    }
    void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // finds player
        if (player != null) { // if there is a player
            playerTransform = player.transform; // the transform variable becoems that of the players
            Collider2D playerCollider = player.GetComponent<Collider2D>(); // find players 2dcollider
            if (playerCollider != null) { // if there is one
                transform.position = playerCollider.bounds.center; // move the collider detectors to COM of player
            } else {
                transform.position = player.transform.position; // else move colider detectors to player center..
            }
        }
    }
    public bool CanSpawn(int newFormIndex) {
        bool cansSpawn = false; // until can spawn it cant
        Quaternion spawnRotation = Quaternion.identity; // i dont know what this does
        Vector2 spawnPos = transform.position; // i dont know what this does
        if (newFormIndex == 2) { // if trying to spawn pillar
            if (!pvcs.isTouchingWall) { // check if can spawn upright
                spawnRotation = Quaternion.identity; // if can, prepare to spawn upright
                cansSpawn = true; // set can spawn to true
            } else if (!phcs.isTouchingWall) { // else if touching wall horizontally
                spawnRotation = Quaternion.Euler(0, 0, 90); // prepare to spawn
                cansSpawn = true; // can spawn  =true
            } else if (!plhcs.isTouchingWall) { // else if touching wall lower horizontally
                spawnRotation = Quaternion.Euler(0, 0, 90); // prepare to spawn
                spawnPos.y += pillarLowOffset; // i really dont know what this does
                cansSpawn = true; // camnspawn = true
            } else { // if it cant spawn in any direction
                cansSpawn = false; // cant spawn
            }
        } else if (newFormIndex == 1){ // if trying to spawn box
            if (!bcs.isTouchingWall){
                cansSpawn = true;
            }
        } else { // if its not a pillar or box
            if (!invMgr.isTouchingWall) { // if its a smaller object (balon ball player)
                spawnRotation = Quaternion.identity; // ????
                cansSpawn = true; // can spawn
            }
        } 

        /* Do we need this? because i think this kinda cooks it a bit
        if (newFormIndex == 2 && spawnRotation == Quaternion.identity) { // if pillar and spawn rotation (???)
            spawnPos.y += verticalPillarYOffset; 
            cansSpawn = true; // CAN SPAWN?
        }
        */

        if (!cansSpawn) { // if cant spawn
            return false; // do nothing and return false, other script will take care of warnings
        } else if (cansSpawn && playerTransform != null){ // if can spawn and player exists already (it should)
            Destroy(playerTransform.gameObject); //DELETE THE PLAYER
            GameObject newPlayer = Instantiate(playerPrefabs[newFormIndex], spawnPos, spawnRotation); //SPAWN THE NEW PLAYER
            newPlayer.tag = "Player"; // GIVE IT THE TAG PLAYER
            playerTransform = newPlayer.transform; // CHANGE OLD PLAYER TRANSFORM TO PLAYER
            return true; // IT CAN SPAWN (OBVIOUSLY)
        } else { // IF IT CANT SPAWN
            return false; // WE ALREADY COVERED THIS BUT C# DOESNT SEEM TO THINK SO XDDDD
        }
    }
}