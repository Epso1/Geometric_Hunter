using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{   
    [SerializeField] private string startingSceneName = "Scene01";

    public void StartGameButton()
    {
        StartCoroutine(StartGame());
    }

    public void ExitGameButton()
    {
        StartCoroutine(ExitGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(startingSceneName);
      
    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        Application.Quit();
    }
}


