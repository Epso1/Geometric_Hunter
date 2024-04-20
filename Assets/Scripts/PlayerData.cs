using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string userName;
    public List<LevelData> completedLevels;

    public PlayerData()
    {
        completedLevels = new List<LevelData>();
    }

    public override string ToString()
    {
        string userDataString = "User: " + userName + "\n";
        userDataString += "Completed Levels:\n";

        foreach (LevelData level in completedLevels)
        {
            userDataString += "- Level " + level.levelIndex + " - Stars: " + level.starsEarned + "\n";
        }

        return userDataString;
    }

}
