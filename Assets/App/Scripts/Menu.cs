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
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.foundation;

public class Menu : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MyMenuWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; // ナビゲーションバー表示用
        base.OnEnable();
    }
}

public class MyMenuWidget : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MyMenuStatefulWidget());
    }
}

public class MyMenuStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MyMenuWidgetState();
    }
}

class MyMenuWidgetState : State<MyMenuStatefulWidget>
{
    private Widget Button(int level, int x)
    {
        List<Widget> children = new List<Widget>();

        switch (Data.GetStagedata(level, x).Item2)
        {
            case 0:
                children = new List<Widget> {
                    new Icon(Icons.star, color: Colors.grey),
                    new Icon(Icons.star, color: Colors.grey),
                    new Icon(Icons.star, color: Colors.grey),
                    new Text(
                        data: "Stage " + x.ToString(),
                        style: new TextStyle(fontSize: MediaQuery.of(context).size.height / 32, color: Colors.white)
                    )
                };
                break;
            case 1:
                children = new List<Widget> {
                    new Icon(Icons.star, color: Colors.yellow),
                    new Icon(Icons.star, color: Colors.grey),
                    new Icon(Icons.star, color: Colors.grey),
                    new Text(
                        data: "Stage " + x.ToString(),
                        style: new TextStyle(fontSize: MediaQuery.of(context).size.height / 32, color: Colors.white)
                    )
                };
                break;
            case 2:
                children = new List<Widget> {
                    new Icon(Icons.star, color: Colors.yellow),
                    new Icon(Icons.star, color: Colors.yellow),
                    new Icon(Icons.star, color: Colors.grey),
                    new Text(
                        data: "Stage " + x.ToString(),
                        style: new TextStyle(fontSize: MediaQuery.of(context).size.height / 32, color: Colors.white)
                    )
                };
                break;
            case 3:
                children = new List<Widget> {
                    new Icon(Icons.star, color: Colors.yellow),
                    new Icon(Icons.star, color: Colors.yellow),
                    new Icon(Icons.star, color: Colors.yellow),
                    new Text(
                        data: "Stage " + x.ToString(),
                        style: new TextStyle(fontSize: MediaQuery.of(context).size.height / 32, color: Colors.white)
                    )
                };
                break;
            default:
                children = new List<Widget> {
                    new Icon(Icons.star, color: Colors.grey),
                    new Icon(Icons.star, color: Colors.grey),
                    new Icon(Icons.star, color: Colors.grey),
                    new Text(
                        data: "Stage " + x.ToString(),
                        style: new TextStyle(fontSize: MediaQuery.of(context).size.height / 32, color: Colors.white)
                    )
                };
                break;
        }
            
        if ( x == 1 && level == 1)
        {
            return new Padding(
                child: new Container(
                    height: MediaQuery.of(context).size.height / 16,
                    child: new FittedBox(
                        child: new RaisedButton(
                            shape: new StadiumBorder(),
                            color: Colors.lightBlue,
                            onPressed: () =>
                            {
                                PlayerPrefs.SetString("Stage", level.ToString() + "Stage" + x.ToString());
                                PlayerPrefs.Save();
                                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                            },
                            child: new Row(
                                children: children
                            )
                        )
                    )
                ),
                padding: EdgeInsets.all(MediaQuery.of(context).size.height / 96)
            );
        }else if (x == 1 && level != 1)
        {
            if (Data.GetStagedata(level - 1, 10).Item2 != 0)
            {
                return new Padding(
                    child: new Container(
                        height: MediaQuery.of(context).size.height / 16,
                        child: new FittedBox(
                            child: new RaisedButton(
                                shape: new StadiumBorder(),
                                color: Colors.lightBlue,
                                onPressed: () =>
                                {
                                    PlayerPrefs.SetString("Stage", level.ToString() + "Stage" + x.ToString());
                                    PlayerPrefs.Save();
                                    SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                                },
                                child: new Row(
                                    children: children
                                )
                            )
                        )
                    ),
                    padding: EdgeInsets.all(MediaQuery.of(context).size.height / 96)
                );
            }
            else
            {
                return new Padding(
                    child: new Container(
                        height: MediaQuery.of(context).size.height / 16,
                        child: new FittedBox(
                            child: new RaisedButton(
                                shape: new StadiumBorder(),
                                color: Colors.lightBlue,
                                child: new Row(
                                    children: children
                                )
                            )
                        )
                    ),
                    padding: EdgeInsets.all(MediaQuery.of(context).size.height / 96)
                );
            }
        }
        else if (Data.GetStagedata(level, x - 1).Item2 != 0)
        {
            return new Padding(
                child: new Container(
                    height: MediaQuery.of(context).size.height / 16,
                    child: new FittedBox(
                        child: new RaisedButton(
                            shape: new StadiumBorder(),
                            color: Colors.lightBlue,
                            onPressed: () =>
                            {
                                PlayerPrefs.SetString("Stage", level.ToString() + "Stage" + x.ToString());
                                PlayerPrefs.Save();
                                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                            },
                            child: new Row(
                                children: children
                            )
                        )
                    )
                ),
                padding: EdgeInsets.all(MediaQuery.of(context).size.height / 96)
            );
        }
        else
        {
            return new Padding(
                child: new Container(
                    height: MediaQuery.of(context).size.height / 16,
                    child: new FittedBox(
                        child: new RaisedButton(
                            shape: new StadiumBorder(),
                            color: Colors.lightBlue,
                            child: new Row(
                                children: children
                            )
                        )
                    )
                ),
                padding: EdgeInsets.all(MediaQuery.of(context).size.height / 96)
            );
        }
    }
    private List<Widget> Buttons(int level, int num)
    {
        List<Widget> button = new List<Widget>();
        string[] text = new string[] { "Beginner", "Intermediate", "Elite"};

        button.Add(new Padding(child: new Text(data: text[level -1], style: new TextStyle(fontSize: MediaQuery.of(context).size.height / 20, color: Colors.white)), padding: EdgeInsets.all(MediaQuery.of(context).size.height / 64)));
        for (int x = 1; x <= num; x += 1)
        {
            button.Add(Button(level, x));
        }

        return button;
    }
    public override Widget build(BuildContext context)
    {
        var controller = new PageController();


        return new MaterialApp(
            home: new Scaffold(
                body: new Unity.UIWidgets.widgets.Stack(
                    children: new List<Widget> {
                        new Positioned(
                            top: 0,
                            left: 0,
                            child: new Container(
                                width: MediaQuery.of(context).size.width,
                                height: MediaQuery.of(context).size.height * 10 /128,
                                decoration: new BoxDecoration(
                                    color: Colors.lightBlue,
                                    borderRadius: BorderRadius.only(bottomLeft: MediaQuery.of(context).size.height * 10 /128, bottomRight: MediaQuery.of(context).size.height * 10 /128)
                                )
                            )
                        ),
                        new Positioned(
                            top: 0,
                            left: 0,
                            right: 0,
                            bottom: 0,
                            child: new PageView(
                                controller: controller,
                                scrollDirection: Axis.horizontal,
                                children: new List<Widget> {
                                    new Column(
                                        crossAxisAlignment: CrossAxisAlignment.center,
                                        children: Buttons(1, 10)
                                    ),
                                    new Column(
                                        crossAxisAlignment: CrossAxisAlignment.center,
                                        children: Buttons(2, 10)
                                    ),
                                    new Column(
                                        crossAxisAlignment: CrossAxisAlignment.center,
                                        children: Buttons(3, 10)
                                    )
                                }
                            )
                        ),
                        new Positioned(
                            top: 5,
                            left: 10,
                            child: new IconButton(
                                icon: new Icon(Icons.navigate_before, color: Colors.white),
                                iconSize: MediaQuery.of(context).size.height/20,
                                onPressed: () => { SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single); }
                            )
                        )
                    }
                )
            )
        ) ;
    }
}

