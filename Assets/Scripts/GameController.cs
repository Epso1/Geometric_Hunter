using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Diagnostics;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private float timeToReloadScene = 3f;
    [SerializeField] private GameObject imageFadeIn;
    [SerializeField] private GameObject imageFadeOut;
    [SerializeField] private float fadeDuration = 1f;
    
    void Start()
    {
        imageFadeIn.SetActive(true);
    }

    void Update()
    {
       // actualiza el texto del score con el valor actual del score
        scoreText.text = "SCORE: " + ScoreManager.Instance.Score;
        hiScoreText.text = "HI-SCORE: " + ScoreManager.Instance.HiScore;
    }

    private int GetActiveSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
 
    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(timeToReloadScene);
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
