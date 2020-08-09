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

public class GameTask : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MyGameTaskWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; // ナビゲーションバー表示用
        base.OnEnable();
    }
}

public class MyGameTaskWidget : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MyGameTaskStatefulWidget());
    }
}

public class MyGameTaskStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MyGameTaskWidgetState();
    }
}

class MyGameTaskWidgetState : State<MyGameTaskStatefulWidget>
{
    public override Widget build(BuildContext context)
    {
        int boundary = 0;

        switch (PlayerPrefs.GetString("Stage")[0])
        {
            case '1':
                boundary = Data.Read().Beginner[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][0];
                break;
            case '2':
                boundary = Data.Read().Intermediate[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6))-1][0];
                break;
            case '3':
                boundary = Data.Read().Elite[int.Parse(PlayerPrefs.GetString("Stage").Remove(0, 6)) - 1][0];
                break;
        }

        List<Widget> texts = new List<Widget>
        {
            new Text(
                data: "Task",
                style: new TextStyle(
                    color: Colors.white,
                    fontSize: MediaQuery.of(context).size.height / 16
                )
            ),
            new Unity.UIWidgets.widgets.Container(),
            new Text(
                data: boundary.ToString()+" %",
                style: new TextStyle(
                    color: Colors.white,
                    fontSize: MediaQuery.of(context).size.height / 8
                )
            )
        };

        if (PlayerPrefs.GetString("Stage") == "1Stage1")
        {
            texts = new List<Widget>
            {
                new Text(
                    data: "Task",
                    style: new TextStyle(
                        color: Colors.white,
                        fontSize: MediaQuery.of(context).size.height / 16
                    )
                ),
                new Unity.UIWidgets.widgets.Container(),
                new Text(
                    data: boundary.ToString()+" %",
                    style: new TextStyle(
                        color: Colors.white,
                        fontSize: MediaQuery.of(context).size.height / 8
                    )
                ),
                Unity.UIWidgets.widgets.Image.asset(
                    "HowTo",
                    height: MediaQuery.of(context).size.height / 6
                )
            };
        }

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
                    child: new Column(
                        mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                        crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.center,
                        children: texts
                    )
                )
            )
        };

        return new MaterialApp(
            home: new Scaffold(
                backgroundColor: Colors.transparent,
                body: new Unity.UIWidgets.widgets.Stack(
                    children: stack
                )
            )
        );
        
    }
}