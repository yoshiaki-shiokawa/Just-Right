using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGame : MonoBehaviour
{
    bool started;
    bool spawner;
    bool paused;

    string stage;

    int original;

    GameObject[] Water;
    GameObject Spawner;
    GameObject Task;
    GameObject Playing;
    GameObject Pause;

    MonoBehaviour spawner_script;

    void Start()
    {
        started = false;
        spawner = true;
        paused = false;
        stage = PlayerPrefs.GetString("Stage");

        GameObject obj = (GameObject)Resources.Load("Container/"+stage);
        Instantiate(obj);

        PlayerPrefs.SetInt("started", 0);
        PlayerPrefs.SetInt("spawner", 1);
        PlayerPrefs.SetInt("water", 0);
        PlayerPrefs.SetInt("newRecord", 0);
        PlayerPrefs.SetInt("right", 0);
        PlayerPrefs.SetInt("refresh", 0);
        PlayerPrefs.SetInt("menu", 0);
        PlayerPrefs.SetInt("setting", 0);
        PlayerPrefs.SetInt("pause", 0);
        PlayerPrefs.Save();

        Spawner = GameObject.FindGameObjectWithTag("Spawner");

        Task = GameObject.Find("Task");
        Playing = GameObject.Find("Playing");
        Pause = GameObject.Find("Pause");

        Playing.SetActive(false);
        Pause.SetActive(false);
    }

    void Paused()
    {
        Time.timeScale = 0f;
        paused = true;
        Pause.SetActive(true);
    }

    void UnPaused()
    {
        Pause.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("water") == 1 && started == false)
        {
            started = true;
            PlayerPrefs.SetInt("started", 1);
            PlayerPrefs.Save();

            spawner_script = Spawner.GetComponent<MonoBehaviour>();
            spawner_script.enabled = false;

            Playing.SetActive(true);
            Task.SetActive(false);
        }

        if (started && spawner)
        {
            UnityEngine.Transform myTransform = Spawner.transform;

            Vector3 pos = myTransform.position;
            pos.y += 0.1f;  

            myTransform.position = pos;

            if (pos.y > 7.5)
            {
                Destroy(Spawner);
                spawner = false;
                PlayerPrefs.SetInt("spawner", 0);
                PlayerPrefs.Save();

                Water = GameObject.FindGameObjectsWithTag("Metaball_liquid");
                original = Water.Length;
            }
        }

        if (PlayerPrefs.GetInt("pause") == 1 && paused == false)
        {
            Paused();
        }else if (PlayerPrefs.GetInt("pause") == 0 && paused == true)
        {
            UnPaused();
        }

        if (PlayerPrefs.GetInt("refresh") == 1)
        {
            PlayerPrefs.SetInt("refresh", 0);
            PlayerPrefs.Save();
            Pause.SetActive(false);
            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
        }

        if (PlayerPrefs.GetInt("menu") == 1)
        {
            PlayerPrefs.SetInt("menu", 0);
            PlayerPrefs.Save();
            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
        }

        if (PlayerPrefs.GetInt("setting") == 1)
        {
            PlayerPrefs.SetInt("setting", 0);
            PlayerPrefs.SetString("SettingScene", "Game");
            PlayerPrefs.Save();
            SceneManager.LoadSceneAsync("SettingScene", LoadSceneMode.Single);
        }

        if (PlayerPrefs.GetInt("right") == 1 && started)
        {
            Water = GameObject.FindGameObjectsWithTag("Metaball_liquid");
            PlayerPrefs.SetInt("newRecord", (int)Water.Length* 100/original);
            PlayerPrefs.Save();
            SceneManager.LoadSceneAsync("ResultScene", LoadSceneMode.Single);
        }
    }
}
