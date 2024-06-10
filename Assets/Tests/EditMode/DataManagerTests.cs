
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

public class DataManagerTests
{
    private PlayerData playerData;
    private GameObject dataManagerObject; 
    private DataManager dataManager;

    [SetUp]
    public void SetUp()
    {
        playerData = new PlayerData("12345", "Pepe");
        dataManagerObject = new GameObject();
        dataManager = dataManagerObject.AddComponent<DataManager>();
    }

    [Test]
    public void CreatePlayerDataEnumTest()
    {
        dataManager.StartCoroutine(dataManager.CreatePlayerDataEnum(playerData.uID, playerData.userName));

        Assert.IsFalse(playerData.availableLevels[0].isLocked);
        Assert.IsTrue(playerData.availableLevels[1].isLocked);
        Assert.IsTrue(playerData.availableLevels[2].isLocked);
        Assert.IsTrue(playerData.availableLevels[3].isLocked);
        Assert.IsTrue(playerData.availableLevels[4].isLocked);
        Assert.IsTrue(playerData.availableLevels[5].isLocked);
    }

    [Test]
    public void UpdateLevelStarsTest()
    {
        int levelIndex = 1;
        int stars = 3;

        dataManager.StartCoroutine(dataManager.CreatePlayerDataEnum(playerData.uID, playerData.userName));      

        dataManager.UpdateLevelStars(levelIndex, stars);

        Assert.IsTrue(dataManager.playerData.availableLevels[levelIndex-1].starsEarned == stars);
    }

    [Test]
    public void UnlockLevelTest()
    {
        int levelIndexToUnlock = 3;

        dataManager.StartCoroutine(dataManager.CreatePlayerDataEnum(playerData.uID, playerData.userName));

        dataManager.UnlockLevel(levelIndexToUnlock);

        Assert.IsTrue(dataManager.playerData.availableLevels[levelIndexToUnlock - 1].isLocked == false);
    }




}
