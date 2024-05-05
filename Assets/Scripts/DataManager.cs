using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;
using JetBrains.Annotations;

public class DataManager : MonoBehaviour
{
    public PlayerData playerData;
    public DatabaseReference dbRef;
    public bool dataFound;

    public static DataManager Instance;

    void Awake()
    {
        if (DataManager.Instance == null)
        {
            DataManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }


    public void LoadPlayerData(string uID)
    {
        StartCoroutine(LoadDataEnum(uID));
    }

    IEnumerator LoadDataEnum(string uID)
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        var serverData = dbRef.Child("users").Child(uID).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("process is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            print("server data found");

            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            print("no data found");
        }

    }


    public void SavePlayerData(PlayerData playerData)
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        string uID = playerData.uID;       
        string json = JsonUtility.ToJson(playerData);
        dbRef.Child("users").Child(uID).SetRawJsonValueAsync(json);
        UnityEngine.Debug.Log("Los datos se guardaron correctamente.");
    }

    public void CreatePlayerData(string uID, string name)
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        playerData = new PlayerData(uID, name);
        string json = JsonUtility.ToJson(playerData);
        dbRef.Child("users").Child(uID).SetRawJsonValueAsync(json);
        UnityEngine.Debug.Log("Los datos se crearon correctamente.");
    }

    public void CheckPlayerData(string uID)
    {
        StartCoroutine(CheckPlayerDataEnum(uID));
    }

    public IEnumerator CheckPlayerDataEnum(string uID)
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        var serverData = dbRef.Child("users").Child(uID).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("process is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            dataFound = true;
            print("server data found");
        }
        else
        {
            dataFound = false;
            print("no data found");
        }

    }

}

