using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string startingSceneName = "Scene01";
    [SerializeField] private GameObject imageFadeOut;
    [SerializeField] private GameObject imageFadeIn;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private TextMeshProUGUI userNameText;

    void Start()
    {
        imageFadeIn.SetActive(true);
        userNameText.text = DataManager.Instance.playerData.userName;
    }

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
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(startingSceneName);

    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        Application.Quit();
    }


}


