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

public class GameClear : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MyGameClearWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; // ナビゲーションバー表示用
        base.OnEnable();
    }
}

public class MyGameClearWidget : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MyGameClearStatefulWidget());
    }
}

public class MyGameClearStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MyGameClearWidgetState();
    }
}

class MyGameClearWidgetState : State<MyGameClearStatefulWidget>
{
    int NumStar()
    {
        int num = 0;
        for (int a = 1; a < 4; a += 1)
        {
            for (int x = 1; x < 11; x += 1)
            {
                num += Data.GetStagedata(a, x).Item2;
            }
        }

        return num;
    }

    public override Widget build(BuildContext context)
    {
        List<Widget> stars = new List<Widget>();
        List<Widget> buttons = new List<Widget>();

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
                    child: new Unity.UIWidgets.widgets.Stack(
                        children: new List<Widget>
                        {
                            new Positioned(
                                bottom: 0,
                                left: 0,
                                right: 0,
                                child: new Row(
                                    mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.spaceBetween,
                                    children: new List<Widget>
                                    {
                                        Unity.UIWidgets.widgets.Image.asset(
                                            "CrackerL",
                                            height: MediaQuery.of(context).size.height / 10
                                            //scale: MediaQuery.of(context).size.height * 2 / MediaQuery.of(context).size.width
                                        ),
                                        Unity.UIWidgets.widgets.Image.asset(
                                            "CrackerR",
                                            height: MediaQuery.of(context).size.height / 10
                                            //scale: MediaQuery.of(context).size.height * 2 / MediaQuery.of(context).size.width
                                        )
                                    }
                                )
                            ),
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
                                        new Unity.UIWidgets.widgets.Container(
                                            width: MediaQuery.of(context).size.width * 30 / 32,
                                            child: new FittedBox(
                                            fit: BoxFit.contain,
                                                child: new Text(
                                                    data: "Congratulations!!",
                                                    style: new TextStyle(
                                                        color: Colors.white
                                                    )
                                                )
                                            )
                                        ),
                                        new Text(
                                            data: "You've cleared all stages in this game\nThank you for playing",
                                            textAlign: TextAlign.center,
                                            style: new TextStyle(
                                                color: Colors.white,
                                                fontSize: MediaQuery.of(context).size.height / 32
                                            )
                                        ),
                                        new Unity.UIWidgets.widgets.Container(
                                            height: MediaQuery.of(context).size.height /16
                                        ),
                                        new Row(
                                            mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                            crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.center,
                                            children: new List<Widget>
                                            {
                                                new Icon(Icons.star, size: MediaQuery.of(context).size.height / 16, color: Colors.yellow),
                                                new Unity.UIWidgets.widgets.Container(),
                                                new Text(
                                                    data: NumStar().ToString()+" / 90",
                                                    style: new TextStyle(
                                                        color: Colors.white,
                                                        fontSize: MediaQuery.of(context).size.height / 16
                                                    )
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
                                        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
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
                backgroundColor: Colors.transparent,
                body: new Unity.UIWidgets.widgets.Stack(
                    children: stack1
                )
            )
        );


    }
}