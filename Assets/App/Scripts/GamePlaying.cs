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

public class  GamePlaying : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MyGamePlayingWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; // ナビゲーションバー表示用
        base.OnEnable();
    }
}

public class MyGamePlayingWidget : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MyGamePlayingStatefulWidget());
    }
}

public class MyGamePlayingStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MyGamePlayingWidgetState();
    }
}

class MyGamePlayingWidgetState : State<MyGamePlayingStatefulWidget>
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(
            home: new Scaffold(
                backgroundColor: Colors.transparent,
                body: new Unity.UIWidgets.widgets.Stack(
                    children: new List<Widget> {
                        new Positioned(
                            top: MediaQuery.of(context).size.width / 128,
                            right: MediaQuery.of(context).size.width / 128,
                            child: new IconButton(
                                color: Colors.grey,
                                iconSize: MediaQuery.of(context).size.width / 16,
                                icon: new Icon(Icons.pause),
                                onPressed: () => {
                                    PlayerPrefs.SetInt("pause", 1);
                                    PlayerPrefs.Save();
                                }
                            )
                        ),
                        new Positioned(
                            top: MediaQuery.of(context).size.height / 16 * 14,
                            bottom: MediaQuery.of(context).size.height / 16,
                            left: MediaQuery.of(context).size.width / 3,
                            right: MediaQuery.of(context).size.width / 3,
                            child: FloatingActionButton.extended(
                                backgroundColor: Colors.blue,
                                label: new Text(
                                    data: "Just Right!",
                                    style: new TextStyle(
                                        color: Colors.white,
                                        fontSize: MediaQuery.of(context).size.width / 16
                                    )
                                ),
                                onPressed: () => {
                                    PlayerPrefs.SetInt("right", 1);
                                    PlayerPrefs.Save();
                                }
                            )
                        )
                    }
                )
            )
        ) ;
    }
}