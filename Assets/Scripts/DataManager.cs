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
        StartCoroutine(LoadPlayerDataEnum(uID));
    }

    IEnumerator LoadPlayerDataEnum(string uID)
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
        StartCoroutine(SavePlayerDataEnum(playerData));
    }

    IEnumerator SavePlayerDataEnum(PlayerData playerData)
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        string uID = playerData.uID;
        string json = JsonUtility.ToJson(playerData);
        var task = dbRef.Child("users").Child(uID).SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => task.IsCompleted);
        UnityEngine.Debug.Log("Data saved.");
    }

    public void CreatePlayerData(string uID, string name)
    {
        StartCoroutine(CreatePlayerDataEnum(uID, name));
    }

    IEnumerator CreatePlayerDataEnum(string uID, string name)
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        playerData = new PlayerData(uID, name);
        string json = JsonUtility.ToJson(playerData);
        var task = dbRef.Child("users").Child(uID).SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => task.IsCompleted);
        UnityEngine.Debug.Log("Data created.");
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

    public void UpdateLevelStars(int levelIndexToUpdateStars, int newStarsEarned)
    {
        // Busca el nivel con el índice especificado
        foreach (LevelData level in playerData.availableLevels)
        {
            if (level.levelIndex == levelIndexToUpdateStars)
            {
                // Solo actualiza las estrellas si newStarsEarned es mayor que las estrellas actuales
                if (newStarsEarned > level.starsEarned)
                {
                    level.starsEarned = newStarsEarned;
                }
                break; // Una vez que se ha actualizado, sal del bucle
            }
        }

        // Guarda los cambios actualizados en la base de datos
        SavePlayerData(playerData);
    }

    public void UnlockLevel(int levelIndexToUpdateLock)
    {
        // Busca el nivel con el índice especificado
        foreach (LevelData level in playerData.availableLevels)
        {
            if (level.levelIndex == levelIndexToUpdateLock)
            {
                // Solo actualiza isLocked si el nivel está bloqueado
                if (level.isLocked)
                {
                    level.isLocked = false;
                }
                break; // Una vez que se ha actualizado, sal del bucle
            }
        }

        // Guarda los cambios actualizados en la base de datos
        SavePlayerData(playerData);
    }
}
