using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{

    public GameObject score_object = null;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 current = Physics2D.gravity;
        float angle = Vector2.Angle(new Vector2(x: 0, y: -20), current);
        this.GetComponent<Text>().text = $"x: {Input.acceleration.x}\ny: {Input.acceleration.y}\nz: {Input.acceleration.z}\nangle: {angle}";
    }
}
