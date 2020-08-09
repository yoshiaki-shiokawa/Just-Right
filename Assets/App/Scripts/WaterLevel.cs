using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Metaball_liquid"))
        {
            PlayerPrefs.SetInt("water", 1);
            PlayerPrefs.Save();
        }
    }
}
