using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        
    }

    void Update()
    {
       // actualiza el texto del score con el valor actual del score
        scoreText.text = "SCORE: " + ScoreManager.Instance.Score;
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
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
    }

}
