using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour

{



    public Collider2D pillarVerticalCollider;

    [Header("Player Prefabs (Index: 0 = Key 1, 1 = Key 2, etc)")]
    public List<GameObject> playerPrefabs = new List<GameObject>(4);

    private Transform playerTransform;
    private int currentFormIndex = -1; // None to start


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

            //Collider2D checkCollider = (newFormIndex == 1) ? pillarCollider : littleCollider;

            // Check if the collider overlaps with any walls
            // Check if any child script has isTouchingWall == true
            bool isBlocked = false;
            foreach (var checker in GetComponentsInChildren<MonoBehaviour>())
            {
                var type = checker.GetType();
                var field = type.GetField("isTouchingWall");
                if (field != null && field.FieldType == typeof(bool))
                {
                    bool value = (bool)field.GetValue(checker);
                    if (value)
                    {
                        isBlocked = true;
                        break;
                    }
                }
            }
            // Physics2D.OverlapCollider(checkCollider, new ContactFilter2D
            // {
            //     layerMask = wallLayer,
            //     useLayerMask = true
            // }, new List<Collider2D>()) > 0;

            if (isBlocked)
            {
                Debug.Log("Cannot transform — space is obstructed.");
                return;
            }

            // Save the intended spawn position
            Vector2 spawnPos = transform.position;


            // Destroy current player
            if (playerTransform != null)
            {
                Destroy(playerTransform.gameObject);
            }

            // Spawn the new player prefab
            GameObject newPlayer = Instantiate(playerPrefabs[newFormIndex], spawnPos, Quaternion.identity);
            newPlayer.tag = "Player";

            // Update references
            playerTransform = newPlayer.transform;
            currentFormIndex = newFormIndex;
        }




    }

    // [Header("Player Prefabs (Index: 0 = Key 1, 1 = Key 2, etc)")]
    // public List<GameObject> playerPrefabs = new List<GameObject>(4);

    // [Header("Collider References")]
    // public Collider2D pillarCollider;     // Used only for pillar (form 2)
    // public Collider2D littleCollider;     // Used for other forms

    // [Header("Settings")]
    // public LayerMask wallLayer;

    // private Transform playerTransform;
    // private int currentFormIndex = -1; // None to start

    // void Update()
    // {
    //     // Track the current player every frame
    //     GameObject player = GameObject.FindGameObjectWithTag("Player");
    //     if (player != null)
    //     {
    //         playerTransform = player.transform;
    //         transform.position = playerTransform.position;

    //         // Handle input keys 1–4
    //         if (Input.GetKeyDown(KeyCode.Alpha1)) TryTransform(0);
    //         if (Input.GetKeyDown(KeyCode.Alpha2)) TryTransform(1);
    //         if (Input.GetKeyDown(KeyCode.Alpha3)) TryTransform(2);
    //         if (Input.GetKeyDown(KeyCode.Alpha4)) TryTransform(3);
    //     }
    // }

    // void TryTransform(int newFormIndex)
    // {
    //     if (newFormIndex == currentFormIndex)
    //     {
    //         Debug.Log("Already in that form.");
    //         return;
    //     }

    //     if (newFormIndex < 0 || newFormIndex >= playerPrefabs.Count || playerPrefabs[newFormIndex] == null)
    //     {
    //         Debug.LogWarning("Invalid prefab or missing reference for form index: " + newFormIndex);
    //         return;
    //     }

    //     Collider2D checkCollider = (newFormIndex == 1) ? pillarCollider : littleCollider;

    //     // Check if the collider overlaps with any walls
    //     bool isBlocked = Physics2D.OverlapCollider(checkCollider, new ContactFilter2D
    //     {
    //         layerMask = wallLayer,
    //         useLayerMask = true
    //     }, new List<Collider2D>()) > 0;

    //     if (isBlocked)
    //     {
    //         Debug.Log("Cannot transform — space is obstructed.");
    //         return;
    //     }

    //     // Save the intended spawn position
    //     Vector3 spawnPos = checkCollider.bounds.center;

    //     // Destroy current player
    //     if (playerTransform != null)
    //     {
    //         Destroy(playerTransform.gameObject);
    //     }

    //     // Spawn the new player prefab
    //     GameObject newPlayer = Instantiate(playerPrefabs[newFormIndex], spawnPos, Quaternion.identity);
    //     newPlayer.tag = "Player";

    //     // Update references
    //     playerTransform = newPlayer.transform;
    //     currentFormIndex = newFormIndex;
    // }
}
