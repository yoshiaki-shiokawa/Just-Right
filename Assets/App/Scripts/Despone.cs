using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despone : MonoBehaviour
{
    void Update()
    {
        float dist = Vector3.Distance(new Vector3(0, 0, 0), transform.position);
        if (dist > 12.0f)
        {
            Destroy(gameObject);
        }
    }
}
