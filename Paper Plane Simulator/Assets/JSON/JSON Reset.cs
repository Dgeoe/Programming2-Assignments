using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class JSONReset : MonoBehaviour
{
    private string saveFilePath;
    private TimeTrialStuff progress;

    public int Ringx;

    void Awake()
    {
        //yeah this isnt a good fix but it will work for now 
        saveFilePath = Path.Combine(Application.persistentDataPath, "player_progress.json");
        progress = new TimeTrialStuff(); 
        SaveProgress(); 
        Ringx = progress.Ringx;
    }
    private void SaveProgress()
    {
        string json = JsonUtility.ToJson(progress);
        File.WriteAllText(saveFilePath, json);
    }
}
