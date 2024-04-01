using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   
    // Crea un singleton para que el ScoreManager no se destruya al cambiar de escena
    public static ScoreManager Instance;

    private int score = 0;

    public int Score
    {
        get => score;
        set => score = value;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 0)
        {
            score = 0;
        }
    }
}
