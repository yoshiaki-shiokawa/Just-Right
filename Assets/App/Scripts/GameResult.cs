using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.SceneManagement;
using System.ComponentModel;
using System;

public class GameResult : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MyGameResultWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; // ナビゲーションバー表示用
        base.OnEnable();
    }
}

public class MyGameResultWidget : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MyGameResultStatefulWidget());
    }
}

public class MyGameResultStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MyGameResultWidgetState();
    }
}

class MyGameResultWidgetState : State<MyGameResultStatefulWidget>
{
    public override Widget build(BuildContext context)
    {
        List<Widget> stars = new List<Widget>();
        List<Widget> buttons = new List<Widget>();
        
        int boundary = 50;
        int record = 0;
        int star = 0;
        int newstar = 0;

        string newrecord = "";

        switch (PlayerPrefs.GetString("Stage")[0])
        {
            case '1':
                boundary = Data.Read().Beginner[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][0];
                star = Data.Read().Beginner[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][1];
                record = Data.Read().Beginner[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][2];
                break;
            case '2':
                boundary = Data.Read().Intermediate[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][0];
                star = Data.Read().Intermediate[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][1];
                record = Data.Read().Intermediate[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][2];
                break;
            case '3':
                boundary = Data.Read().Elite[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6)) - 1][0];
                star = Data.Read().Elite[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6)) - 1][1];
                record = Data.Read().Elite[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6)) - 1][2];
                break;
        }

        if (Math.Abs(PlayerPrefs.GetInt("newRecord") - boundary) < 5)
        {
            stars = new List<Widget>
                {
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.yellow),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.yellow),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.yellow)
                };
            buttons = new List<Widget>
            {
                new IconButton(
                    icon: new Icon(Icons.refresh, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                    }
                ),
                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                new IconButton(
                    icon: new Icon(Icons.menu, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
                    }
                ),
                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                new IconButton(
                    icon: new Icon(Icons.arrow_right, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        if (PlayerPrefs.GetString("Stage").Remove(0,6) == "10")
                        {
                            PlayerPrefs.SetString("Stage", (int.Parse(PlayerPrefs.GetString("Stage")[0].ToString())+1).ToString() + "Stage1");
                            PlayerPrefs.Save();
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                        else
                        {
                            PlayerPrefs.SetString("Stage", PlayerPrefs.GetString("Stage").Remove(6)+(int.Parse(PlayerPrefs.GetString("Stage").Remove(0,6))+1).ToString()) ;
                            PlayerPrefs.Save();
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                    }
                )
            };
            newstar = 3;
        }
        else if (Math.Abs(PlayerPrefs.GetInt("newRecord") - boundary) < 10)
        {
            stars = new List<Widget>
                {
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.yellow),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.yellow),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.grey)
                };
            buttons = new List<Widget>
            {
                new IconButton(
                    icon: new Icon(Icons.refresh, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                    }
                ),
                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                new IconButton(
                    icon: new Icon(Icons.menu, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
                    }
                ),
                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                new IconButton(
                    icon: new Icon(Icons.arrow_right, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        if (PlayerPrefs.GetString("Stage").Remove(0,6) == "10")
                        {
                            PlayerPrefs.SetString("Stage", (int.Parse(PlayerPrefs.GetString("Stage")[0].ToString())+1).ToString() + "Stage1");
                            PlayerPrefs.Save();
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                        else
                        {
                            PlayerPrefs.SetString("Stage", PlayerPrefs.GetString("Stage").Remove(6)+(int.Parse(PlayerPrefs.GetString("Stage").Remove(0,6))+1).ToString()) ;
                            PlayerPrefs.Save();
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                    }
                )
            };
            newstar = 2;
        }
        else if (Math.Abs(PlayerPrefs.GetInt("newRecord") - boundary) < 15)
        {
            stars = new List<Widget>
                {
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.yellow),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.grey),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.grey)
                };
            buttons = new List<Widget>
            {
                new IconButton(
                    icon: new Icon(Icons.refresh, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                    }
                ),
                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                new IconButton(
                    icon: new Icon(Icons.menu, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
                    }
                ),
                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                new IconButton(
                    icon: new Icon(Icons.arrow_right, color: Colors.white),
                    iconSize: MediaQuery.of(context).size.height / 16,
                    onPressed: () => {
                        Time.timeScale = 1f;
                        if (PlayerPrefs.GetString("Stage").Remove(0,6) == "10")
                        {
                            PlayerPrefs.SetString("Stage", (int.Parse(PlayerPrefs.GetString("Stage")[0].ToString())+1).ToString() + "Stage1");
                            PlayerPrefs.Save();
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                        else
                        {
                            PlayerPrefs.SetString("Stage", PlayerPrefs.GetString("Stage").Remove(6)+(int.Parse(PlayerPrefs.GetString("Stage").Remove(0,6))+1).ToString()) ;
                            PlayerPrefs.Save();
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                    }
                )
            };
            newstar = 1;
        }
        else
        {
            stars = new List<Widget>
                {
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.grey),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.grey),
                    new Unity.UIWidgets.widgets.Container(),
                    new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.grey)
                };

            if (star != 0)
            {
                buttons = new List<Widget>
                {
                    new IconButton(
                        icon: new Icon(Icons.refresh, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                    ),
                    new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                    new IconButton(
                        icon: new Icon(Icons.menu, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
                        }
                    ),
                    new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                    new IconButton(
                        icon: new Icon(Icons.arrow_right, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            if (PlayerPrefs.GetString("Stage").Remove(0,6) == "10")
                            {
                                PlayerPrefs.SetString("Stage", (int.Parse(PlayerPrefs.GetString("Stage")[0].ToString())+1).ToString() + "Stage1");
                                PlayerPrefs.Save();
                                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                            }
                            else
                            {
                                PlayerPrefs.SetString("Stage", PlayerPrefs.GetString("Stage").Remove(6)+(int.Parse(PlayerPrefs.GetString("Stage").Remove(0,6))+1).ToString()) ;
                                PlayerPrefs.Save();
                                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                            }
                        }
                    )
                };
            }
            else
            {
                buttons = new List<Widget>
                {
                    new IconButton(
                        icon: new Icon(Icons.refresh, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                    ),
                    new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                    new IconButton(
                        icon: new Icon(Icons.menu, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
                        }
                    ),
                    new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                    new IconButton(
                        icon: new Icon(Icons.arrow_right, color: Colors.grey),
                        iconSize: MediaQuery.of(context).size.height / 16
                    )
                };
            }
        }

        if (Math.Abs(PlayerPrefs.GetInt("newRecord") - boundary) < Math.Abs(record - boundary))
        {
            newrecord = "New!";

            Data.SetScore(int.Parse(PlayerPrefs.GetString("Stage")[0].ToString()), int.Parse(PlayerPrefs.GetString("Stage").Remove(0,6)), PlayerPrefs.GetInt("newRecord"));
        }

        if (newstar > star)
        {
            Data.SetStar(int.Parse(PlayerPrefs.GetString("Stage")[0].ToString()), int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6)), newstar);
        }

        if (PlayerPrefs.GetString("Stage") == "3Stage10")
        {
            buttons = new List<Widget>
                {
                    new IconButton(
                        icon: new Icon(Icons.refresh, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                        }
                    ),
                    new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                    new IconButton(
                        icon: new Icon(Icons.menu, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
                        }
                    ),
                    new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                    new IconButton(
                        icon: new Icon(Icons.arrow_right, color: Colors.white),
                        iconSize: MediaQuery.of(context).size.height / 16,
                        onPressed: () => {
                            Time.timeScale = 1f;
                            SceneManager.LoadSceneAsync("ClearScene", LoadSceneMode.Single);
                        }
                    )
                };
        }

        List<Widget> stack1 = new List<Widget>
        {
            new Positioned(
                top: MediaQuery.of(context).size.height /4,
                left: MediaQuery.of(context).size.width / 32,
                right: MediaQuery.of(context).size.width / 32,
                bottom: MediaQuery.of(context).size.height /4,
                child: new Card(
                    elevation: 10,
                    color: Colors.lightBlue,
                    child: new Column(
                        mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                        crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.center,
                        children: new List<Widget>
                        {
                            new Text(
                                data: "Result",
                                style: new TextStyle(
                                    color: Colors.white,
                                    fontSize: MediaQuery.of(context).size.height / 16
                                )
                            ),
                            new Row(
                                mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.center,
                                children: stars
                            ),
                            new Text(
                                data: newrecord,
                                style: new TextStyle(
                                    color: Colors.yellowAccent,
                                    fontSize: MediaQuery.of(context).size.height / 32
                                )
                            ),
                            new Text(
                                data: PlayerPrefs.GetInt("newRecord").ToString()+" %",
                                style: new TextStyle(
                                    color: Colors.white,
                                    fontSize: MediaQuery.of(context).size.height / 16
                                )
                            ),
                            new Row(
                                mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.center,
                                children: buttons
                            )
                        }
                    )
                )
            )
        };


        return new MaterialApp(
            home: new Scaffold(
                backgroundColor: Colors.transparent,
                body: new Unity.UIWidgets.widgets.Stack(
                    children: stack1
                )
            )
        );
        
        
    }
}