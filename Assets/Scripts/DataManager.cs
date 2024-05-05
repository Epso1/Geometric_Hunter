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


    public void SavePlayerData(string uID, string name)
    {
        playerData = new PlayerData(uID, name);
        string json = JsonUtility.ToJson(playerData);
        dbRef.Child("users").Child(uID).SetRawJsonValueAsync(json);
        UnityEngine.Debug.Log("Los datos se guardaron correctamente.");
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
    public void CreatePlayerData(string uID, string name)
    {
        playerData = new PlayerData(uID, name);
        string json = JsonUtility.ToJson(playerData);
        dbRef.Child("users").Child(uID).SetRawJsonValueAsync(json);
        UnityEngine.Debug.Log("Los datos se crearon correctamente.");

    }

    public int UserExists(string uID)
    {
        int result = 1;
        // Consultar si el uID existe en la base de datos
        dbRef.Child("users").OrderByChild("uID").EqualTo(uID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                result = 1;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    result = 0;
                }
                else
                {
                    result = 2;
                }
            }
        });
        return result;
    }

    public void logUser(string uID)
    {
        // Consultar si el uID existe en la base de datos
        dbRef.Child("users").Child(uID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al consultar la base de datos.");
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Debug.Log("El usuario con uID " + uID + " existe en la base de datos.");
                }
                else
                {
                    Debug.Log("El usuario con uID " + uID + " no existe en la base de datos.");
                }
            }
        });
    }

    // Método para comprobar si un usuario existe en la base de datos
    public void CheckIfUserExists(string uID, Action<bool> callback)
    {
        dbRef.Child("users").Child(uID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al comprobar la existencia del usuario: " + task.Exception);
                callback(false);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                callback(snapshot.Exists);
            }
        });
    }


}

