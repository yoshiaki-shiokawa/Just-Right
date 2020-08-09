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

public class GamePause : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MyGamePauseWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; 
        base.OnEnable();
    }
}

public class MyGamePauseWidget : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MyGamePauseStatefulWidget());
    }
}

public class MyGamePauseStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MyGamePauseWidgetState();
    }
}

class MyGamePauseWidgetState : State<MyGamePauseStatefulWidget>
{
    public override Widget build(BuildContext context)
    {
        string[] text = new string[] { "Beginner", "Intermediate", "Elite" };

        List<Widget> stack = new List<Widget>
        {
            new Positioned(
                top: MediaQuery.of(context).size.height /4,
                left: MediaQuery.of(context).size.width / 32,
                right: MediaQuery.of(context).size.width / 32,
                bottom: MediaQuery.of(context).size.height /4,
                child: new Card(
                    elevation: 10,
                    color: Colors.lightBlue,
                    child: new Unity.UIWidgets.widgets.Stack(
                        children: new List<Widget>
                        {
                            new Positioned(
                                top: 0,
                                left: 0,
                                right: 0,
                                bottom: 0,
                                child: new Column(
                                    mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                    crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.center,
                                    children: new List<Widget>
                                    {
                                        new Text(
                                            data: text[int.Parse(PlayerPrefs.GetString("Stage")[0].ToString())-1],
                                            style: new TextStyle(
                                                color: Colors.white,
                                                fontSize: MediaQuery.of(context).size.height / 16
                                            )
                                        ),
                                        new Text(
                                            data: PlayerPrefs.GetString("Stage").Remove(0,1),
                                            style: new TextStyle(
                                                color: Colors.white,
                                                fontSize: MediaQuery.of(context).size.height / 16
                                            )
                                        ),
                                        new Unity.UIWidgets.widgets.Container(height: MediaQuery.of(context).size.height / 16),
                                        new Row(
                                            mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                            crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.center,
                                            children: new List<Widget>
                                            {
                                                new IconButton(
                                                    icon: new Icon(Icons.refresh, color: Colors.white),
                                                    iconSize: MediaQuery.of(context).size.height / 16,
                                                    onPressed: () => {
                                                        Time.timeScale = 1f;
                                                        PlayerPrefs.SetInt("refresh", 1);
                                                        PlayerPrefs.Save();
                                                    }
                                                ),
                                                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                                                new IconButton(
                                                    icon: new Icon(Icons.menu, color: Colors.white),
                                                    iconSize: MediaQuery.of(context).size.height / 16,
                                                    onPressed: () => {
                                                        Time.timeScale = 1f;
                                                        PlayerPrefs.SetInt("menu", 1);
                                                        PlayerPrefs.Save();
                                                    }
                                                ),
                                                new Unity.UIWidgets.widgets.Container(width: MediaQuery.of(context).size.height / 16),
                                                new IconButton(
                                                    icon: new Icon(Icons.settings, color: Colors.white),
                                                    iconSize: MediaQuery.of(context).size.height / 16,
                                                    onPressed: () => {
                                                        Time.timeScale = 1f;
                                                        PlayerPrefs.SetInt("setting", 1);
                                                        PlayerPrefs.Save();
                                                    }
                                                )
                                            }
                                        )
                                    }
                                )
                            ),
                            new Positioned(
                                top: MediaQuery.of(context).size.width / 128,
                                right: MediaQuery.of(context).size.width / 128,
                                child: new IconButton(
                                    icon: new Icon(Icons.close, color: Colors.white),
                                    iconSize: MediaQuery.of(context).size.height / 32,
                                    onPressed: () => {
                                        Time.timeScale = 1f;
                                        PlayerPrefs.SetInt("pause", 0);
                                        PlayerPrefs.Save();
                                    }
                                )
                            )
                        }
                    )
                    
                )
            )
        };

        return new MaterialApp(
            home: new Scaffold(
                backgroundColor: Colors.white70,
                body: new Unity.UIWidgets.widgets.Stack(
                    children: stack
                )
            )
        );

    }
}