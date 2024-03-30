using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    public int Score
    {
        get => score;
        set => score = value;
    }
    // Crea un singleton para que el GameController no se destruya al cambiar de escena
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
                if (instance == null)
                {
                    instance = new GameObject("GameController").AddComponent<GameController>();
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    } 

   
    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log("SCORE: " + score);
        scoreText.text = "SCORE: " + score;
    }

    public IEnumerator ReloadScene()
    {
        Debug.Log("Reloading scene");
        // Espera 2 segundos
        yield return new WaitForSeconds(1f);
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
