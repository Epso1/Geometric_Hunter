using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections;
using System;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<LevelButtonSet> levelButtonSets; // Lista de objetos que contienen botón de nivel y estrellas
    [SerializeField] private string[] levelNames;
    [SerializeField] private GameObject imageFadeOut;
    [SerializeField] private GameObject imageFadeIn;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private string mainMenuSceneName;

    void Start()
    {
        imageFadeIn.SetActive(true);
        DataManager dataManager = DataManager.Instance;
        if (dataManager != null && dataManager.playerData != null)
        {
            // Obtener los datos del jugador desde playerData
            PlayerData playerData = dataManager.playerData;

            // Iterar sobre los botones y habilitarlos o deshabilitarlos según los niveles desbloqueados
            for (int i = 0; i < levelButtonSets.Count; i++)
            {
                int levelIndex = i + 1;

                // Buscar el nivel correspondiente en la lista de niveles completados del jugador
                LevelData levelData = playerData.availableLevels.FirstOrDefault(level => level.levelIndex == levelIndex);

                if (levelData != null && !levelData.isLocked)
                {
                    levelButtonSets[i].levelButton.interactable = true; // Habilitar el botón si el nivel está desbloqueado
                    levelButtonSets[i].lockImage.gameObject.SetActive(false); // Desactivar la imagen del candado

                    // Activar los botones de estrellas según la cantidad de estrellas ganadas en el nivel
                    int starsEarned = levelData.starsEarned;
                    for (int j = 0; j < levelButtonSets[i].starImages.Length; j++)
                    {
                        levelButtonSets[i].starImages[j].gameObject.SetActive(j < starsEarned);
                    }
                }
                else
                {
                    levelButtonSets[i].levelButton.interactable = false; // Deshabilitar el botón si el nivel está bloqueado
                    levelButtonSets[i].levelButton.GetComponentInChildren<TMP_Text>().text = ""; // Borrar el texto del botón
                    levelButtonSets[i].lockImage.gameObject.SetActive(true); // Activar la imagen del candado
                    // Activar los botones de estrellas según la cantidad de estrellas ganadas en el nivel                 
                    for (int j = 0; j < levelButtonSets[i].starImages.Length; j++)
                    {
                        levelButtonSets[i].starImages[j].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void LoadLevel01()
    {
        StartCoroutine(LoadLevel(levelNames[0]));
    }

    public void LoadLevel02()
    {
        StartCoroutine(LoadLevel(levelNames[1]));
    }

    public IEnumerator LoadLevel(string levelName)
    {
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(levelName);
    }

    public void BackToMainMenu()
    {
        StartCoroutine(LoadMainMenuScene());
    }

    public IEnumerator LoadMainMenuScene()
    {
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
