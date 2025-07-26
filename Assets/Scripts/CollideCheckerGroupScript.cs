using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheckerGroupScript : MonoBehaviour {

    public List<GameObject> playerPrefabs = new(4);

    public int pillarFormIndex = 1; // index of pillar in prefabs

    private Transform playerTransform;
    private int currentFormIndex = -1; // None to start

    PillarVerticalColliderScript pvcs;
    PillarHorizontalColliderScript phcs;
    PillarLowHorizontalColliderScript plhcs;
    InventoryManagement invMgr;

    [Header("Pillar spawn offset when upright")]
    public float verticalPillarYOffset = 20f; // tweak in Inspector
    public float pillarLowOffset = -10f; // incorporate

    void Start(){
        invMgr = GetComponentInChildren<InventoryManagement>();
        pvcs = GetComponentInChildren<PillarVerticalColliderScript>();
        phcs = GetComponentInChildren<PillarHorizontalColliderScript>();
        plhcs = GetComponentInChildren<PillarLowHorizontalColliderScript>();
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
    
    void TryTransform(int newFormIndex) {
        bool isBlocked = true;
        Quaternion spawnRotation = Quaternion.identity;
        // Spawn the new form
        Vector2 spawnPos = transform.position;
        if (newFormIndex == pillarFormIndex) {
            if (!pvcs.isTouchingWall) {
                isBlocked = false;
                spawnRotation = Quaternion.identity; // Upright
            } else if (!phcs.isTouchingWall) {
                isBlocked = false;
                spawnRotation = Quaternion.Euler(0, 0, 90); // Rotated 90°
            } else if (!plhcs.isTouchingWall) {
                isBlocked = false;
                spawnRotation = Quaternion.Euler(0, 0, 90); // Rotated 90°
                spawnPos.y += pillarLowOffset;
            }
        } else {
            if (!invMgr.isTouchingWall) {
                isBlocked = false;
                spawnRotation = Quaternion.identity;
            }
        }
        if (isBlocked) {
            Debug.Log("Cannot transform — all directions obstructed.");
            return;
        }
        if (newFormIndex == pillarFormIndex && spawnRotation == Quaternion.identity) {
            spawnPos.y += verticalPillarYOffset;
        }
        if (playerTransform != null){
            Destroy(playerTransform.gameObject);

            GameObject newPlayer = Instantiate(playerPrefabs[newFormIndex], spawnPos, spawnRotation);
            newPlayer.tag = "Player";
            playerTransform = newPlayer.transform;
            currentFormIndex = newFormIndex;
        }
    }
}