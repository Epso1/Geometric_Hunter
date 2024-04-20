using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int levelIndex;
    public int starsEarned;

    public LevelData(int index, int stars)
    {
        levelIndex = index;
        starsEarned = stars;
    }

}


