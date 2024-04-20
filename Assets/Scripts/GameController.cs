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
    [SerializeField] private AudioSource soundFXAudioSource;
    [SerializeField] private AudioSource BGMusicAudioSource;
    [SerializeField] private AudioClip goodShotAudioClip;
    [SerializeField] private float goodShotAudioClipVolume = .8f;
    [SerializeField] private AudioClip badShotAudioClip;
    [SerializeField] private float badShotAudioClipVolume= .6f;
    [SerializeField] private AudioClip BGMusicAudioClip;
    [SerializeField] private float BGMusicAudioClipVolume = .6f;
    [SerializeField] private AudioClip playerDiesAudioClip;
    [SerializeField] private float playerDiesAudioClipVolume = .6f;
    [SerializeField] private AudioClip playerShootsAudioClip;
    [SerializeField] private float playerShootsAudioClipVolume = .6f;
    
    SaveManager saveManager;
    PlayerData playerData = new PlayerData();

    private void Awake()
    {
        saveManager = GetComponent<SaveManager>();
    }

    void Start()
    {
        imageFadeIn.SetActive(true);
        PlayBGMusic();
       
    }

    void Update()
    {
       // actualiza el texto del score con el valor actual del score
        scoreText.text = "SCORE: " + ScoreManager.Instance.Score;
        hiScoreText.text = "HI-SCORE: " + ScoreManager.Instance.HiScore;

        UnityEngine.Debug.Log(playerData.ToString());

        if (Input.GetKeyDown(KeyCode.O))
        {
            playerData = LoadGameData();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            SaveGameData();
        }
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

    public void PlayGoodShot()
    {
        soundFXAudioSource.volume = goodShotAudioClipVolume;
        soundFXAudioSource.clip = goodShotAudioClip;
        soundFXAudioSource.Play();
    }

    public void PlayBadShot()
    {
        soundFXAudioSource.volume = badShotAudioClipVolume;
        soundFXAudioSource.clip = badShotAudioClip;
        soundFXAudioSource.Play();
    }

    public void PlayPlayerDies()
    {
        soundFXAudioSource.volume = playerDiesAudioClipVolume;
        soundFXAudioSource.clip = playerDiesAudioClip;
        soundFXAudioSource.Play();
    }

    public void PlayPlayerShoots()
    {
        soundFXAudioSource.volume = playerShootsAudioClipVolume;
        soundFXAudioSource.clip = playerShootsAudioClip;
        soundFXAudioSource.Play();
    }

    public void PlayBGMusic()
    {
        BGMusicAudioSource.volume = BGMusicAudioClipVolume;
        BGMusicAudioSource.clip = BGMusicAudioClip;
        BGMusicAudioSource.Play();
    }

    public void SaveGameData()
    {
        // Ejemplo de cómo guardar datos
        PlayerData playerData = new PlayerData();   
        playerData.userName = "EjemploUsuario";
        playerData.completedLevels.Add(new LevelData(1, 3)); // Nivel 1 completado con 3 estrellas
        playerData.completedLevels.Add(new LevelData(2, 2)); // Nivel 2 completado con 2 estrellas

        saveManager.SavePlayerData(playerData);
    }

    public PlayerData LoadGameData()
    {
        return saveManager.LoadPlayerData();
    }

}
