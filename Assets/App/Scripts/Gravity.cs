using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gravity : MonoBehaviour
{
    Vector2 origin = new Vector2(x: 0, y: -20);

    void Start()
    {
        Initiallise();
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("spawner") == 0)
        {
            ChangeGyro();
        }
    }

    void Initiallise()
    {
        Physics2D.gravity = origin;
    }

    void ChangeGyro()
    {
        Vector2 current = Physics2D.gravity;
        Vector2 accel = Input.acceleration;
        current.x = accel.x * 20;
        current.y = accel.y * 20;

        Physics2D.gravity = current;
    }

    void Change(double angle)
    {
        double y = Math.Cos(angle*Math.PI/180) * -20;
        double x = Math.Sin(angle * Math.PI / 180) * -20;

        Vector2 vector = new Vector2((float)x, (float)y);

        Physics2D.gravity = vector;
        //Debug.Log(Read());
    }

    double Read()
    {
        Vector2 current = Physics2D.gravity;
        return 180 * Math.Atan2(current.y/-20, current.x/20 ) / Math.PI - 90;
    }

}
