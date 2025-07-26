using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour

{



    public Collider2D pillarVerticalCollider;

    [Header("Player Prefabs (Index: 0 = Key 1, 1 = Key 2, etc)")]
    public List<GameObject> playerPrefabs = new List<GameObject>(4);

    [Header("Index of pillar form in playerPrefabs list")]
    public int pillarFormIndex = 1;


    private Transform playerTransform;
    private int currentFormIndex = -1; // None to start



    public PillarVerticalColliderScript pillarVerticalColliderScript;
    public PillarHorizontalColliderScript pillarHorizontalColliderScript;
    public SmallCharacterColliderScript smallCharacterColliderScript;

    public PillarLowHorizontalColliderScript pillarLowHorizontalColliderScript;

    [Header("Pillar spawn offset when upright")]
    public float verticalPillarYOffset = 20f; // tweak in Inspector

    public float pillarLowOffset = -10f; // incorporate



    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;

            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                transform.position = playerCollider.bounds.center;
            }
            else
            {
                transform.position = player.transform.position;
            }




            // Handle input keys 1–4
            if (Input.GetKeyDown(KeyCode.Alpha1)) TryTransform(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) TryTransform(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) TryTransform(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) TryTransform(3);



        }


    void TryTransform(int newFormIndex)
        {
            if (newFormIndex == currentFormIndex)
            {
                Debug.Log("Already in that form.");
                return;
            }

            if (newFormIndex < 0 || newFormIndex >= playerPrefabs.Count || playerPrefabs[newFormIndex] == null)
            {
                Debug.LogWarning("Invalid prefab or missing reference for form index: " + newFormIndex);
                return;
            }

            bool isBlocked = true;
            Quaternion spawnRotation = Quaternion.identity;

            
            // Spawn the new form
            Vector2 spawnPos = transform.position;



            if (newFormIndex == pillarFormIndex)
            {
                if (!pillarVerticalColliderScript.isTouchingWall)
                {
                    isBlocked = false;
                    spawnRotation = Quaternion.identity; // Upright
                }
                else if (!pillarHorizontalColliderScript.isTouchingWall)
                {
                    isBlocked = false;
                    spawnRotation = Quaternion.Euler(0, 0, 90); // Rotated 90°
                }
                else if (!pillarLowHorizontalColliderScript.isTouchingWall)
                {
                    isBlocked = false;
                    spawnRotation = Quaternion.Euler(0, 0, 90); // Rotated 90°

                    spawnPos.y += pillarLowOffset;
                }

            }
            else
            {
                if (!smallCharacterColliderScript.isTouchingWall)
                {
                    isBlocked = false;
                    spawnRotation = Quaternion.identity;
                }
            }

            if (isBlocked)
            {
                Debug.Log("Cannot transform — all directions obstructed.");
                return;
            }

            if (newFormIndex == pillarFormIndex && spawnRotation == Quaternion.identity)
            {
                spawnPos.y += verticalPillarYOffset;
            }


            if (playerTransform != null)
                Destroy(playerTransform.gameObject);

            GameObject newPlayer = Instantiate(playerPrefabs[newFormIndex], spawnPos, spawnRotation);
            newPlayer.tag = "Player";
            playerTransform = newPlayer.transform;
            currentFormIndex = newFormIndex;

    }





    }

    }
