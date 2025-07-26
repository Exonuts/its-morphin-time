using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip[] backgroundMusicSelection = new AudioClip[4];
    List<AudioClip> soundEffects = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        
        musicSource.clip = backgroundMusicSelection[Random.Range(0,4)];
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!musicSource.isPlaying && musicSource.time > 0) {

            musicSource.clip = backgroundMusicSelection[Random.Range(0,4)];
            musicSource.Play();
        }

    }

    public void PlaySFX(AudioClip clip) {

        SFXSource.PlayOneShot(clip);

    }
}
