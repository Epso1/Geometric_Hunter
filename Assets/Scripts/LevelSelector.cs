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
