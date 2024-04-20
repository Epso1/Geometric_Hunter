using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{

    [SerializeField] private Button[] buttons;

    void Start()
    {
        PlayerPrefs.SetString("LEVEL1", "COMPLETED");
        PlayerPrefs.SetString("LEVEL2", "COMPLETED");
        PlayerPrefs.SetString("LEVEL3", "UNCOMPLETED");
        PlayerPrefs.SetString("LEVEL4", "UNCOMPLETED");
        PlayerPrefs.SetString("LEVEL5", "UNCOMPLETED");
        PlayerPrefs.SetString("LEVEL6", "UNCOMPLETED");
        UnityEngine.Debug.Log("LEVEL1: " + PlayerPrefs.GetString("LEVEL1"));
        UnityEngine.Debug.Log("LEVEL2: " + PlayerPrefs.GetString("LEVEL2"));
        UnityEngine.Debug.Log("LEVEL3: " + PlayerPrefs.GetString("LEVEL3"));
        UnityEngine.Debug.Log("LEVEL4: " + PlayerPrefs.GetString("LEVEL4"));
        UnityEngine.Debug.Log("LEVEL5: " + PlayerPrefs.GetString("LEVEL5"));
        UnityEngine.Debug.Log("LEVEL6: " + PlayerPrefs.GetString("LEVEL6"));

        for (int i = 0; i < buttons.Length; i++)
        {
            string levelName = "LEVEL" + (i +1).ToString();
            if (!PlayerPrefs.GetString(levelName).Equals("COMPLETED"))
            {
            //buttons[i].enabled = false;
            buttons[i].interactable = false;
            }
        }
        
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(2);
    }

        
}
