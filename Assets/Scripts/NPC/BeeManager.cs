using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{

    public static BeeManager Instance { get; set; }
    public LayerMask target;
    public bool beesSummoned = false;

    public GameObject audioManager;
    private AudioManager actualAudio;
    public AudioClip beeSound;

    public GameObject beeFolder;

    private List<GameObject> bees = new List<GameObject>();

    public float searchRadius = 4f;

    void Awake() {

        if(Instance != null && Instance != this) {

            Destroy(gameObject);
        } else {

            Instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        actualAudio = audioManager.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Collider2D isInProximity = Physics2D.OverlapCircle(transform.position,searchRadius,target);
        bool isInProximity = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // Do something with tagged object
                isInProximity = true;
            }
        }

        if(isInProximity) {

            if(!beesSummoned) {

                beesSummoned = true;
                actualAudio.PlaySFX(beeSound);
                for (int i = 0; i < 10; i++) {

                    GameObject beeToAdd = Instantiate(Resources.Load<GameObject>("Bee"),new Vector3(13.21f,1.4f,0f),Quaternion.Euler(0f,0f,0f));
                    float scale = Random.Range(0.3f,0.7f);
                    beeToAdd.transform.localScale = new Vector3(scale,scale,scale);
                    string targetLayerName = "Killer";
                    int layer = LayerMask.NameToLayer(targetLayerName);
                    bees.Add(beeToAdd);
                    beeToAdd.layer = layer;
                    beeToAdd.transform.SetParent(beeFolder.transform);
                }

            }

        }
    }

    public void removeBees() {

        Debug.Log("Removing");
        for (int i = 0; i < bees.Count; i++) {

            Debug.Log("Removed Bee");
            Destroy(bees[i]);

        }
        beesSummoned = false;

    }
}
