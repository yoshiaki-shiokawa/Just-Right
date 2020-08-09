using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource BGM;

    private float _sofar;
    private static bool created = false;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // this must be a duplicate from a scene reload - DESTROY!
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BGM.volume = PlayerPrefs.GetFloat("BGM");
    }
}
