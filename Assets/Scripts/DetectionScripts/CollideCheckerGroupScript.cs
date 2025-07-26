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

    public bool canBox;
    public bool canSmall;
    public bool canPillarUp;
    public bool canPillarLR;

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
        if (pvcs.isTouchingWall){
            canPillarUp = false;
        } else {
            canPillarUp = true;
        }
        if (phcs.isTouchingWall || plhcs.isTouchingWall){
            canPillarLR = false;
        } else {
            canPillarLR = true;
        }
        if (bcs.isTouchingWall){
            canBox = false;
        } else {
            canBox = true;
        }
        if (invMgr.isTouchingWall){
            canSmall = false;
        } else {
            canSmall = true;
        }
    }
    public bool CanSpawn(int newFormIndex) {
        bool cansSpawn = false; // until confirmed can spawn it cant

        Quaternion spawnRotation = Quaternion.identity; 
        Vector2 spawnPos = transform.position; 

        switch (newFormIndex){
            case 1: // if spawning box
                if (canBox){
                    cansSpawn = true;
                }
                break;
            case 2: // if pillar
                if (canPillarUp){
                    spawnRotation = Quaternion.identity; // if can, prepare to spawn upright
                    cansSpawn = true; // set can spawn to true
                } else if (!phcs.isTouchingWall){
                    spawnRotation = Quaternion.Euler(0, 0, 90); // prepare to spawn
                    cansSpawn = true; // can spawn  =true
                } else if (!plhcs.isTouchingWall){
                    spawnRotation = Quaternion.Euler(0, 0, 90); // prepare to spawn
                    spawnPos.y += pillarLowOffset; 
                    cansSpawn = true; // camnspawn = true
                }
                break;
            case 0:
            case 3:
            case 4:
                if (canSmall){
                    cansSpawn = true;
                }
                break;
        }
        // unless given otherwise by the swithc statement, you cannot spawn
        
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