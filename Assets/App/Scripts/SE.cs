using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    bool start = false;
    bool end = false;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("SE");
        audioSource.PlayOneShot(sound1);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("water") == 0 && start == false)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(sound2);
            
            start = true;
        }else if (PlayerPrefs.GetInt("spawner") == 0 && end == false)
        {
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.PlayOneShot(sound3);
            end = true;
        }
    }
}
