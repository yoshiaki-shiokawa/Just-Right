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
using UnityEngine.UI;
using Unity.UIWidgets.cupertino;
using Unity.UIWidgets.foundation;

public class Setting : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MySettingWidget();
    }
    protected override void OnEnable()
    {
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"), "Material Icons");
        Screen.fullScreen = false; // ナビゲーションバー表示用
        base.OnEnable();
    }
}

public class MySettingWidget : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new MaterialApp(home: new MySettingStatefulWidget());
    }
}

public class MySettingStatefulWidget : StatefulWidget
{
    public override State createState()
    {
        return new MySettingWidgetState();
    }
}

class MySettingWidgetState : State<MySettingStatefulWidget>
{
    string lastSelectedValue;
    float _valueBGM = PlayerPrefs.GetFloat("BGM");
    float _valueSE = PlayerPrefs.GetFloat("SE");

    string lastscene = PlayerPrefs.GetString("SettingScene");

    InitialliseAds ads = new InitialliseAds();

    VoidCallback BackToStart = () => {SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);};
    VoidCallback BackToGame = () => { SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single); };

    void ShowDialog(BuildContext context = null, Widget child = null)
    {
        Unity.UIWidgets.cupertino.CupertinoRouteUtils.showCupertinoDialog(
            context: context,
            builder: (BuildContext _context) => child
        ).Then((object value) => {
            if (value != null)
            {
                this.setState(() => { this.lastSelectedValue = value as string; });
            }
        });
    }

    public override Widget build(BuildContext context)
    {
        AppBar appbar = new AppBar();
        if (lastscene == "Start")
        {
            appbar = new AppBar(
                leading: new IconButton(
                    icon: new Icon(Icons.navigate_before, color: Colors.white, size: 25),
                    onPressed: BackToStart
                    )
                );
        }else if (lastscene == "Game")
        {
            appbar = new AppBar(
                leading: new IconButton(
                    icon: new Icon(Icons.navigate_before, color: Colors.white, size: 25),
                    onPressed: BackToGame
                    )
                );
        }

        return new MaterialApp(
            home: new Scaffold(
                appBar: appbar,
                body: new ListView(
                    padding: EdgeInsets.all(10),
                    scrollDirection: Axis.vertical,
                    children: new List<Widget> {
                        new Container(
                            decoration: new BoxDecoration(
                                borderRadius: BorderRadius.circular(10),
                                border: Border.all(
                                    color: Colors.grey,
                                    width: 5
                                )
                            ),
                            child: new Column(
                                mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.start,
                                children: new List<Widget>
                                {
                                    new Padding(
                                        padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
                                        child: new Unity.UIWidgets.widgets.Text(
                                            data: "Setting",
                                            style: new TextStyle(
                                                fontSize: MediaQuery.of(context).size.height / 32
                                            )
                                        )
                                    ),
                                    new Row(
                                        mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.spaceBetween,
                                        children: new List<Widget>
                                        {
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                                child: new Unity.UIWidgets.widgets.Text(
                                                    data: "BGM Volume",
                                                    style: new TextStyle(
                                                        fontSize: MediaQuery.of(context).size.height / 40
                                                    )
                                                )
                                            ),
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(5, 5, 10, 5),
                                                child: new Container(
                                                    width: MediaQuery.of(context).size.width/3,
                                                    child: new Unity.UIWidgets.material.Slider(
                                                        min: 0f,
                                                        max: 1f,
                                                        divisions: 10,
                                                        value: _valueBGM,
                                                        inactiveColor: Colors.grey,
                                                        activeColor: Colors.lightBlue,
                                                        onChangeEnd: (float value) => {
                                                            PlayerPrefs.GetFloat("BGM", value);
                                                            PlayerPrefs.Save();
                                                            Data.SetVolume((value, Data.GetVolume().Item2));
                                                            this.setState(() => { this._valueBGM = value; });
                                                            SceneManager.LoadSceneAsync("SettingScene", LoadSceneMode.Single);
                                                        }
                                                    )
                                                )
                                            )
                                        }
                                    ),
                                    new Row(
                                        mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.spaceBetween,
                                        children: new List<Widget>
                                        {
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                                child: new Unity.UIWidgets.widgets.Text(
                                                    data: "Sound Effect Volume",
                                                    style: new TextStyle(
                                                        fontSize: MediaQuery.of(context).size.height / 40
                                                    )
                                                )
                                            ),
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(5, 5, 10, 5),
                                                child: new Container(
                                                    width: MediaQuery.of(context).size.width/3,
                                                    child: new Unity.UIWidgets.material.Slider(
                                                        min: 0f,
                                                        max: 1f,
                                                        divisions: 10,
                                                        value: _valueSE,
                                                        inactiveColor: Colors.grey,
                                                        activeColor: Colors.lightBlue,
                                                        onChangeEnd: (float value) => {
                                                            PlayerPrefs.GetFloat("SE", value);
                                                            PlayerPrefs.Save();
                                                            Data.SetVolume((Data.GetVolume().Item1, value));
                                                            this.setState(() => { this._valueSE = value; });
                                                           SceneManager.LoadSceneAsync("SettingScene", LoadSceneMode.Single);
                                                        }
                                                    )
                                                )
                                            )
                                        }
                                    ),
                                    new Row(
                                        mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.spaceBetween,
                                        children: new List<Widget>
                                        {
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                                child: new Unity.UIWidgets.widgets.Text(
                                                    data: "Reset all saved data",
                                                    style: new TextStyle(
                                                        fontSize: MediaQuery.of(context).size.height / 40
                                                    )
                                                )
                                            ),
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(5, 5, 10, 5),
                                                child: new MaterialButton(
                                                    child: new Unity.UIWidgets.widgets.Text(
                                                        data: "Reset",
                                                        style: new TextStyle(
                                                            fontSize: MediaQuery.of(context).size.height / 40,
                                                            color: Colors.white
                                                        )
                                                    ),
                                                    color: Colors.lightBlue,
                                                    onPressed: () =>  {
                                                        this.ShowDialog(
                                                            context: context,
                                                            child:
                                                            new AlertDialog(
                                                                title: new Unity.UIWidgets.widgets.Text(
                                                                    "Reset?",
                                                                    style: new TextStyle(
                                                                        fontSize: MediaQuery.of(context).size.height / 40
                                                                    )
                                                                ),
                                                                content: new Unity.UIWidgets.widgets.Text(
                                                                    "This will reset all data in this device.",
                                                                    style: new TextStyle(
                                                                        fontSize: MediaQuery.of(context).size.height / 48
                                                                    )
                                                                ),
                                                                actions: new List<Widget> {
                                                                    new FlatButton(
                                                                        child: new Unity.UIWidgets.widgets.Text(
                                                                            "Reset",
                                                                            style: new TextStyle(
                                                                                fontSize: MediaQuery.of(context).size.height / 48
                                                                            )
                                                                        ),
                                                                        onPressed: () => { Data.Initiallise(); Navigator.pop(context, "Cancel");}
                                                                    ),
                                                                    new FlatButton(
                                                                        child: new Unity.UIWidgets.widgets.Text(
                                                                            "Cancel",
                                                                            style: new TextStyle(
                                                                                fontSize: MediaQuery.of(context).size.height / 48
                                                                            )
                                                                        ),
                                                                        onPressed: () => { Navigator.pop(context, "Cancel"); }
                                                                    ),
                                                                }
                                                            )
                                                        );
                                                    }
                                                )
                                            )
                                        }
                                    )
                                }
                            )
                        ),
                        new Container(height: 10),
                        new Container(
                            decoration: new BoxDecoration(
                                borderRadius: BorderRadius.circular(10),
                                border: Border.all(
                                    color: Colors.grey,
                                    width: 5
                                )
                            ),
                            child: new Column(
                                mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.start,
                                children: new List<Widget>
                                {
                                    new Padding(
                                        padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
                                        child: new Unity.UIWidgets.widgets.Text(
                                            data: "Rule",
                                            style: new TextStyle(
                                                fontSize: MediaQuery.of(context).size.height / 32
                                            )
                                        )
                                    ),
                                    new Padding(
                                        padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                        child: new Unity.UIWidgets.widgets.Text(
                                            data: "The goal is to adjust the amount of water to the target percentage of the orginal amount.\n" +
                                            "You can spill some water by tilting your device.\n\n" +
                                            "You can recieve the following stars on each stage.\n" +
                                            "1 Star: ± 15%\n2 Stars: ± 10%\n3 Stars: ± 5%",
                                            style: new TextStyle(
                                                fontSize: MediaQuery.of(context).size.height / 40
                                            )
                                        )
                                    )
                                }
                            )
                        ),
                        new Container(height: 10),
                        new Container(
                            decoration: new BoxDecoration(
                                borderRadius: BorderRadius.circular(10),
                                border: Border.all(
                                    color: Colors.grey,
                                    width: 5
                                )
                            ),
                            child: new Column(
                                mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.start,
                                children: new List<Widget>
                                {
                                    new Padding(
                                        padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
                                        child: new Unity.UIWidgets.widgets.Text(
                                            data: "Feedback",
                                            style: new TextStyle(
                                                fontSize: MediaQuery.of(context).size.height / 32
                                            )
                                        )
                                    ),
                                    new Row(
                                        mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.spaceBetween,
                                        children: new List<Widget>
                                        {
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                                child: new Unity.UIWidgets.widgets.Text(
                                                    data: "Give us your feedback",
                                                    style: new TextStyle(
                                                        fontSize: MediaQuery.of(context).size.height / 40
                                                    )
                                                )
                                            ),
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(5, 5, 10, 5),
                                                child: new MaterialButton(
                                                    child: new Unity.UIWidgets.widgets.Text(
                                                        data: "Send",
                                                        style: new TextStyle(
                                                            fontSize: MediaQuery.of(context).size.height / 40,
                                                            color: Colors.white
                                                        )
                                                    ),
                                                    color: Colors.lightBlue,
                                                    onPressed: () =>  {}
                                                )
                                            )
                                        }
                                    ),
                                    new Row(
                                        mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.spaceBetween,
                                        children: new List<Widget>
                                        {
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                                child: new Unity.UIWidgets.widgets.Text(
                                                    data: "Cheer us.\n  (Watch an advertisement)",
                                                    style: new TextStyle(
                                                        fontSize: MediaQuery.of(context).size.height / 40
                                                    )
                                                )
                                            ),
                                            new Padding(
                                                padding: EdgeInsets.fromLTRB(5, 5, 10, 5),
                                                child: new MaterialButton(
                                                    child: new Unity.UIWidgets.widgets.Text(
                                                        data: "Watch",
                                                        style: new TextStyle(
                                                            fontSize: MediaQuery.of(context).size.height / 40,
                                                            color: Colors.white
                                                        )
                                                    ),
                                                    color: Colors.lightBlue,
                                                    onPressed: () =>  {ads.ShowRewardedVideo(); }
                                                )
                                            )
                                        }
                                    )
                                }
                            )
                        ),
                        new Container(height: 10),
                        new Container(
                            decoration: new BoxDecoration(
                                borderRadius: BorderRadius.circular(10),
                                border: Border.all(
                                    color: Colors.grey,
                                    width: 5
                                )
                            ),
                            child: new Column(
                                mainAxisAlignment: Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                crossAxisAlignment: Unity.UIWidgets.rendering.CrossAxisAlignment.start,
                                children: new List<Widget>
                                {
                                    new Padding(
                                        padding: EdgeInsets.fromLTRB(5, 5, 5, 10),
                                        child: new Unity.UIWidgets.widgets.Text(
                                            data: "About",
                                            style: new TextStyle(
                                                fontSize: MediaQuery.of(context).size.height / 32
                                            )
                                        )
                                    ),
                                    new Padding(
                                        padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                        child: new Unity.UIWidgets.widgets.Text(
                                            data: "App Version: "+ Application.version,
                                            style: new TextStyle(
                                                fontSize: MediaQuery.of(context).size.height / 40
                                            )
                                        )
                                    ),
                                    new Padding(
                                        padding: EdgeInsets.fromLTRB(20, 5, 5, 5),
                                        child: new Unity.UIWidgets.widgets.Text(
                                            data: "This game is made by Yoshiaki Shiokawa",
                                            style: new TextStyle(
                                                fontSize: MediaQuery.of(context).size.height / 40
                                            )
                                        )
                                    )
                                }
                            )
                        ),
                    }
                )
            )
        ) ;
    }
}

