using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.IO;
using Unity.UIWidgets.foundation;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using GameData;

namespace GameData
{
    [DataContract]
    public struct SaveData
    {
        [DataMember]
        public List<List<int>> Beginner { get; set; }
        [DataMember]
        public List<List<int>> Intermediate { get; set; }
        [DataMember]
        public List<List<int>> Elite { get; set; }
        [DataMember]
        public bool First { get; set; }
        [DataMember]
        public float BGM { get; set; }
        [DataMember]
        public float SE { get; set; }
    }
}


public static class Data
{

    const string SAVE_FILE_PATH = "save";

    public static void Initiallise()
    {
        List<int> stage = new List<int> { 50, 0, 100}; // Boundary, Stars. Best Score
        List<List<int>> level = new List<List<int>>();

        for (int x = 0; x < 10; x += 1)
        {
            level.Add(stage);
        }

        var data = new SaveData();
        data.Beginner = level;
        data.Intermediate = level;
        data.Elite = level;
        data.First = true;
        data.BGM = 1.0f;
        data.SE = 1.0f;

        Write(data);
    }

    static void Write(SaveData data)
    {
        var json = new DataContractJsonSerializer(typeof(SaveData));
        var path = UnityEngine.Application.persistentDataPath + "/" + SAVE_FILE_PATH + ".json";

        using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            json.WriteObject(stream, data);

            stream.Flush();
            stream.Close();
        }
    }

    public static void SetStar(int level, int stage, int star)
    {
        SaveData data = Read();

        if (level == 1)
        {
            data.Beginner[stage - 1][1] = star;
        }else if (level == 2)
        {
            data.Intermediate[stage - 1][1] = star;
        }else if (level == 3)
        {
            data.Elite[stage - 1][1] = star;
        }

        Write(data);
    }

    public static void SetScore(int level, int stage, int score)
    {
        SaveData data = Read();

        if (level == 1)
        {
            data.Beginner[stage - 1][2] = score;
        }
        else if (level == 2)
        {
            data.Intermediate[stage - 1][2] = score;
        }
        else if (level == 3)
        {
            data.Elite[stage - 1][2] = score;
        }

        Write(data);
    }

    public static void SetFirst(bool first)
    {
        var data = Read();
        data.First = first;

        Write(data);
    }

    public static void SetVolume((float, float) volume)
    {
        SaveData data = Read();
        data.BGM = volume.Item1;
        data.SE = volume.Item2;

        Write(data);
    }

    public static SaveData Read() 
    {
        var json = new DataContractJsonSerializer(typeof(SaveData));

        var path = UnityEngine.Application.persistentDataPath + "/" + SAVE_FILE_PATH + ".json";
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            SaveData data = (SaveData)json.ReadObject(stream);

            stream.Close();
            return data;
        }
        
    }

    public static (int, int, int) GetStagedata(int level, int stage)
    {
        (int, int, int) stagedata;
        SaveData data= Read();
        if (level == 1)
        { 
            stagedata = (data.Beginner[stage - 1][0], data.Beginner[stage - 1][1], data.Beginner[stage - 1][2]);
        }else if (level == 2)
        {
            stagedata = (data.Intermediate[stage - 1][0], data.Intermediate[stage - 1][1], data.Intermediate[stage - 1][2]);
        }
        else if (level == 3)
        {
            stagedata = (data.Elite[stage - 1][0], data.Elite[stage - 1][1], data.Elite[stage - 1][2]);
        }
        else
        {
            stagedata = (0, 0, 0);
        }

        return stagedata;
    }

    public static (float, float) GetVolume()
    {
        (float, float) volume;
        SaveData data = Read();
        volume = (data.BGM, data.SE);

        return volume;
    }
}