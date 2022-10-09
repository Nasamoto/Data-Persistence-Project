using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerInfo Instance;
    static string path;
    public string highestPlayer;
    public int highestScore;
    public string playerName;
    public UsersData Data;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Data = new UsersData();
        path = Application.persistentDataPath + "/savefile.json";
        LoadInfo();        
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class SaveData 
    {
        public string name;
        public int score;
        public SaveData(string name, int score)
        {
            this.name = name;
            this.score = score;
        }        
    }

    [System.Serializable]
    public class UsersData
    {
        public List<SaveData> Users;
    }

    public void LoadInfo()
    {
        string data = File.ReadAllText(path);
        UsersData d = JsonUtility.FromJson<UsersData>(data);
        SaveData[] saveDatas = d.Users.ToArray();
        Data.Users = saveDatas.ToList<SaveData>();
        //for(int i = 0; i < saveDatas.Length; ++i)
        //{
        //    Debug.Log(saveDatas[i].name + " " + saveDatas[i].score);
        //}
        //foreach(var i in Data.Users)
        //{
        //    Debug.Log(i.name + " " + i.score);
        //}
        if(Data.Users.Count != 0)
        {
            Data.Users.Sort((p1, p2) =>
            {
                return p1.score > p2.score ? 0 : 1;
            });
            highestPlayer = Data.Users[0].name;
            highestScore = Data.Users[0].score;
        }
        else
        {
            highestPlayer = playerName;
            highestScore = 0;
        }
    }

    public void SaveInfo(int score)
    {
        SaveData data = Data.Users.Find(delegate (SaveData data) {
            return data.name == playerName;
        });
        if(data != null)
        {
            data.score = score > data.score ? score : data.score;
        }
        else
        {
            data = new SaveData(playerName, score);
            Data.Users.Add(data);
        }
        string d = JsonUtility.ToJson(Data);
        File.WriteAllText(path, d);
    }

    //public void LoadInfo()
    //{
    //    string path = Application.persistentDataPath + "/savefile.json";

    //    if(File.Exists(path))
    //    {
    //        string jsonStringK = File.ReadAllText(path);
            
    //    }
    //    //if(File.Exists(path))
    //    //{
    //    //    string json = File.ReadAllText(path);
    //    //    SaveData data = JsonUtility.FromJson<SaveData>(json);
    //    //    highestPlayer = data.name;
    //    //    highestScore = data.score;
    //    //}
    //}

    //public void SaveInfo(int score)
    //{
    //    string path = Application.persistentDataPath + "/savefile.json";

    //    SaveData data = saveListData.Find(
    //        delegate(SaveData data)
    //        {
    //            return data.name == playerName;
    //        });
    //    if(data == null)
    //    {
    //        data = new SaveData(playerName, score);            
    //        saveListData.Add(data);
    //    }
    //    else
    //    {
    //        Debug.Log(data.name + " " + data.score);
    //        data.score = score;
    //    }
    //    string jsonToSave = JsonUtility.ToJson(saveListData.ToArray());
    //    PlayerPrefs.SetString("Data", jsonToSave);
    //    PlayerPrefs.Save();
    //    Debug.Log("==========" + jsonToSave);
    //    File.WriteAllText(path, jsonToSave);

    //    //SaveData data = new SaveData();
    //    //data.name = playerName;
    //    //data.score = score;
    //    //string json = JsonUtility.ToJson(data);
    //    //File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    //}
}
