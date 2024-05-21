using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager Instance;

    private int score = 0;

    public int Score
    {
        get => score;
        set => score = value;
    }

    private int hiScore = 0;

    public int HiScore
    {
        get => hiScore;
        set => hiScore = value;
    }

    void Awake()
    {
        if (ScoreManager.Instance == null)
        {
            ScoreManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Resetear el score cuando se carga el nivel
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        score = 0;
    }


    void Start()
    {

    }

    void Update()
    {
        if (score < 0)
        {
            score = 0;
        }

        if (score > hiScore)
        {
            hiScore = score;
        }
    }
}
