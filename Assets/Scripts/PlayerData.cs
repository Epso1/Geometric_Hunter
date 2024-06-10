using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string uID;
    public string userName;
    public List<LevelData> availableLevels;

    public PlayerData(string uID, string name) { 
        this.uID = uID;
        this.userName = name;
        availableLevels = new List<LevelData> ();
        availableLevels.Add(new LevelData(1, 0, false));
        availableLevels.Add(new LevelData(2, 0, true));
        availableLevels.Add(new LevelData(3, 0, true));
        availableLevels.Add(new LevelData(4, 0, true));
        availableLevels.Add(new LevelData(5, 0, true));
        availableLevels.Add(new LevelData(6, 0, true));
    }
 

    public override string ToString()
    {
        string userDataString = "UID: " + uID + "\n" + "Name: " + userName + "\n";
        userDataString += "Completed Levels:\n";

        foreach (LevelData level in availableLevels)
        {
            userDataString += "- Level " + level.levelIndex + " - Stars: " + level.starsEarned + " - Locked: " + level.isLocked + "\n";
        }

        return userDataString;
    }

}
