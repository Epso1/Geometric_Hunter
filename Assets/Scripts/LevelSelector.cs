using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LevelSelector : MonoBehaviour
{

    [SerializeField] private Button[] buttons;
    [SerializeField] private string level01Name;
    void Start()

    {
        /*
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
        */
        DataManager dataManager = DataManager.Instance;
        if (dataManager != null && dataManager.playerData != null)
        {
            // Obtener los datos del jugador desde playerData
            PlayerData playerData = dataManager.playerData;

            // Iterar sobre los botones y habilitarlos o deshabilitarlos según los niveles desbloqueados
            for (int i = 0; i < buttons.Length; i++)
            {
                int levelIndex = i + 1;

                // Buscar el nivel correspondiente en la lista de niveles completados del jugador
                LevelData levelData = playerData.availableLevels.FirstOrDefault(level => level.levelIndex == levelIndex);

                if (levelData != null && !levelData.isLocked)
                {
                    buttons[i].interactable = true; // Habilitar el botón si el nivel está desbloqueado
                }
                else
                {
                    buttons[i].interactable = false; // Deshabilitar el botón si el nivel está bloqueado
                }
            }
        }
    }

    public void LoadLevel01()
    {
        SceneManager.LoadScene(level01Name);
    }

        
}
