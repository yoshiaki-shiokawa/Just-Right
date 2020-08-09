using RSG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Screen.fullScreen = false;
        try
        {
            Data.Read();
        }
        catch
        {
            Data.Initiallise();
        }

        (float, float) volume = Data.GetVolume();
        PlayerPrefs.SetFloat("BGM", volume.Item1);
        PlayerPrefs.SetFloat("SE", volume.Item2);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
