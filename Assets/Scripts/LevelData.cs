using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int levelIndex;
    public int starsEarned;
    public bool isLocked;

    public LevelData(int levelIndex, int starsEarned, bool isLocked) { 
        this.levelIndex = levelIndex;
        this.starsEarned = starsEarned;
        this.isLocked = isLocked;
    }

}


