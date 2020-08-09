using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Example : EditorWindow
{
    private string saveFolderPath = "Assets/Resources/Prefab/";
    private List<GameObject> DropList = new List<GameObject>();
    //メニューに項目追加
    [MenuItem("Example/新しくウィンドウを出します")]
    static void open()
    {
        EditorWindow.GetWindow<Example>("Example");
    }

    void OnGUI()
    {
        var dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "drop");
        var evt = Event.current;

        switch (evt.type)
        {
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition)) break;
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                DragAndDrop.AcceptDrag();
                break;

            case EventType.DragExited:
                foreach (GameObject go in DragAndDrop.objectReferences)
                {
                    Debug.Log(go);
                    DropList.Add(go);
                }

                CreatePrefabs();
                Event.current.Use();
                break;
        }
    }

    //プレハブ化する関数
    void CreatePrefabs()
    {
        foreach (GameObject go in DropList)
        {
            if (DropList == null)
            {
                return;
            }
            string prefab = saveFolderPath + go.name + ".prefab";
            //PrefabUtility.CreatePrefab(prefab, go);
            PrefabUtility.SaveAsPrefabAsset(go, prefab);
        }
        DropList.Clear();
    }
}
