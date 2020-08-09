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
using SocialConnector;

public class Start : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MyWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; // ナビゲーションバー表示用
        base.OnEnable();
    }
}

public class MyWidget: StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MyStatefulWidget());
    }
}

public class MyStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MyWidgetState();
    }
}

class MyWidgetState : State<MyStatefulWidget>
{
    public override Widget build(BuildContext context)
    {
        //AsyncOperation async = new AsyncOperation();
        //try
        //{
            //async = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
            //async.allowSceneActivation = false;
        //}
        //catch
        //{
            //Debug.Log("SceneError");
        //}
        
        
        List<Unity.UIWidgets.widgets.Widget> stack = new List<Widget>();
        stack.Add(
            new Positioned(
                child: new Container(
                    decoration: new BoxDecoration(
                        gradient: new LinearGradient(
                            colors: new List<Unity.UIWidgets.ui.Color> {Colors.white, Colors.cyan, Colors.indigo },
                            begin: Alignment.topCenter,
                            end: Alignment.bottomCenter))),
                top: 0,
                bottom: 0,
                left: 0,
                right: 0
                )
            ) ;
        stack.Add(
            new Positioned(
                child: new IconButton(
                    icon: new Icon(Icons.settings, color: Colors.black38),
                    iconSize: MediaQuery.of(context).size.height/20,
                    onPressed: () => {
                        PlayerPrefs.SetString("SettingScene", "Start");
                        PlayerPrefs.Save();
                        SceneManager.LoadSceneAsync("SettingScene", LoadSceneMode.Single); 
                    }
                ),
                top: 10,
                left: 10));
        stack.Add(
            new Positioned(
                child: new IconButton(
                    icon: new Icon(Icons.share, color: Colors.black38),
                    iconSize: MediaQuery.of(context).size.height / 20,
                    onPressed: () => {
                        SocialConnector.SocialConnector.Share("Just Right", "https://twitter.com/jyuko49", null);
                    }
                ),
                top: 10,
                right: 10));
        stack.Add(
            new Positioned(
                child: Unity.UIWidgets.widgets.Image.asset(
                    "Title"
                ),
                top: MediaQuery.of(context).size.height / 4,
                left: 50,
                right: 50));
        stack.Add(
            new Positioned(
                child: new RaisedButton(
                    onPressed: () => {
                        //async.allowSceneActivation = true;
                        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
                    }, 
                    color: Colors.blue,
                    padding: EdgeInsets.all(0),
                    child: new Container(
                        child: new Icon(Icons.play_arrow, size: MediaQuery.of(context).size.height / 16, color: Colors.lightBlueAccent),  
                        width: MediaQuery.of(context).size.width / 3,
                        decoration: new BoxDecoration(
                            color: Colors.blue,
                            border: Border.all(color: Colors.blueGrey))),
                    elevation: 10),
                bottom: (MediaQuery.of(context).size.height / 4),
                left: (MediaQuery.of(context).size.width / 3),
                right: (MediaQuery.of(context).size.width / 3)
                ));
        PlayerPrefs.SetFloat("dropsize", (float)0.4);
        return new MaterialApp(
            home: new Scaffold(
                body: new Unity.UIWidgets.widgets.Stack(children: stack
                )
            )
        );
    }
}

