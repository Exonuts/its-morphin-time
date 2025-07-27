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

    public float searchRadius = 7f;

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
        Collider2D isInProximity = Physics2D.OverlapCircle(transform.position,searchRadius,target);

        if(isInProximity != null) {

            if(!beesSummoned) {

                beesSummoned = true;
                actualAudio.PlaySFX(beeSound);
                for (int i = 0; i < 100; i++) {

                    Debug.Log("Summoned");
                    GameObject beeToAdd = Instantiate(Resources.Load<GameObject>("Bee"),new Vector3(13.21f,1.4f,0f),Quaternion.Euler(0f,0f,0f));
                    float scale = Random.Range(0.02f,0.1f);
                    beeToAdd.transform.localScale = new Vector3(scale,scale,scale);
                    string targetLayerName = "Killer";
                    int layer = LayerMask.NameToLayer(targetLayerName);
                    beeToAdd.layer = layer;
                    beeToAdd.transform.SetParent(beeFolder.transform);
                }

            }

        }
    }
}
