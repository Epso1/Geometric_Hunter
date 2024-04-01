using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
        SceneManager.LoadScene(1);
      
    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        Application.Quit();
    }
}


