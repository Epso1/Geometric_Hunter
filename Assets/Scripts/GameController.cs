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
    #region variables
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private TextMeshProUGUI waveInfoText;

    [SerializeField] private GameObject imageFadeIn;
    [SerializeField] private GameObject imageFadeOut;
    [SerializeField] private float timeToReloadScene = 3f;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource soundFXAudioSource;
    [SerializeField] private AudioSource BGMusicAudioSource;
    [SerializeField] private AudioClip goodShotAudioClip;
    [SerializeField] private float goodShotAudioClipVolume = .8f;
    [SerializeField] private AudioClip badShotAudioClip;
    [SerializeField] private float badShotAudioClipVolume = .6f;
    [SerializeField] private AudioClip BGMusicAudioClip;
    [SerializeField] private float BGMusicAudioClipVolume = .6f;
    [SerializeField] private AudioClip victoryMusicAudioClip;
    [SerializeField] private float victoryAudioClipVolume = .6f;
    [SerializeField] private AudioClip playerDiesAudioClip;
    [SerializeField] private float playerDiesAudioClipVolume = .6f;
    [SerializeField] private AudioClip playerShootsAudioClip;
    [SerializeField] private float playerShootsAudioClipVolume = .6f;

    [Header("Level Config")]
    [SerializeField] private int levelIndex = 1;
    [SerializeField] private String mainMenuSceneName;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private EnemiesCreator enemiesCreator;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject victoryMessage;
    [SerializeField] private float levelCompletedWait = 8f;
    [SerializeField] private string levelSelectionSceneName;
    [SerializeField] private int levelIndexToUnlock = 2;
    [SerializeField] private int topScore = 300;
    [SerializeField] private int medScore = 200;
    [SerializeField] private float afterCheckWait = 2f;

    private bool levelCompleted = false;
    private SaveManager saveManager;

    #endregion


    private void Awake()
    {
        saveManager = GetComponent<SaveManager>();
    }

    void Start()
    {
        imageFadeIn.SetActive(true);
        PlayBGMusic();
        victoryMessage.SetActive(false);
        StartCoroutine(CheckLevelFinished());

    }

    void Update()
    {
        scoreText.text = "SCORE: " + ScoreManager.Instance.Score;
        hiScoreText.text = "HI-SCORE: " + ScoreManager.Instance.HiScore;

        if (levelCompleted == true)
        {
            levelCompleted = false;
            StartCoroutine(LevelCompleted());
        }

    }

    public IEnumerator LevelCompleted()
    {
        PlayVictoryMusic();
        victoryMessage.SetActive(true);
        player.GetComponent<Animator>().SetTrigger("Win");
        cameraAnimator.SetTrigger("Win");

        DataManager.Instance.UnlockLevel(levelIndexToUnlock);

        if (ScoreManager.Instance.Score >= topScore)
        {
            DataManager.Instance.UpdateLevelStars(levelIndex, 3);
        }
        else if (ScoreManager.Instance.Score < topScore && ScoreManager.Instance.Score >= medScore)
        {
            DataManager.Instance.UpdateLevelStars(levelIndex, 2);
        }
        else
        {
            DataManager.Instance.UpdateLevelStars(levelIndex, 1);
        }

        yield return new WaitForSeconds(levelCompletedWait);
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(levelSelectionSceneName);

    }

    public IEnumerator CheckLevelFinished()
    {
        yield return StartCoroutine(enemiesCreator.CreateEnemies());
        yield return new WaitForSeconds(afterCheckWait);
        if (player != null)
        {
            if (!player.GetComponent<Player>().playerIsDead)
            {
                levelCompleted = true;
            }
        }
    }

    public IEnumerator ReloadScene()
    {
        cameraAnimator.SetTrigger("Die");
        yield return new WaitForSeconds(timeToReloadScene);
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        StartCoroutine(RestartScene());
    }

    public IEnumerator RestartScene()
    {
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadMainMenuScene());
    }

    public IEnumerator LoadMainMenuScene()
    {
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource aSource in audioSources)
        {
            aSource.Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource aSource in audioSources)
        {
            aSource.UnPause();
        }
    }
    public void SetWaveInfoText(string newText)
    {
        StartCoroutine(SetWaveInfoTextEnum(newText));
    }

    public IEnumerator SetWaveInfoTextEnum(string newText)
    {
        waveInfoText.gameObject.SetActive(true);
        waveInfoText.text = newText;
        yield return new WaitForSeconds(enemiesCreator.waveInfoTextTime);
        waveInfoText.gameObject.SetActive(false);
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

    public void StopBGMusic()
    {
        BGMusicAudioSource.Stop();
    }


    public void PlayVictoryMusic()
    {
        BGMusicAudioSource.volume = victoryAudioClipVolume;
        BGMusicAudioSource.clip = victoryMusicAudioClip;
        BGMusicAudioSource.Play();
    }
}
