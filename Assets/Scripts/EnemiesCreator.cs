using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float enemySpeed = 6f;
    [SerializeField] private float initialWait = 2f;
    [SerializeField] private float waitTimeDivider = 20f;
    [SerializeField] private int wavesNumber = 3;
    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private float waveWaitTime = 3f;
    [SerializeField] private float enemySpeedMultiplier = 1.5f;
    [SerializeField] private GameController gameController;
    private float waitTime;
    public float waveInfoTextTime = 5f;


    public IEnumerator CreateEnemies()
    {
        yield return new WaitForSeconds(initialWait);
        waitTime = 1 - (enemySpeed / waitTimeDivider);

        for (int j = 0; j < wavesNumber; j++)
        {
            if (j == (wavesNumber - 1))
            {
                gameController.SetWaveInfoText("FINAL WAVE\nIS COMING!!!");
            }
            else
            {
                gameController.SetWaveInfoText("WAVE " + (j + 1) + "\nIS COMING!!!");
            }
            
            yield return new WaitForSeconds(waveInfoTextTime);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                int randomInt = Random.Range(0, enemies.Length);
                GameObject enemy = Instantiate(enemies[randomInt], this.transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().Speed = enemySpeed;
                yield return new WaitForSeconds(waitTime);
            }

            yield return new WaitForSeconds(waveWaitTime);
            enemySpeed *= enemySpeedMultiplier;
            waitTime = 1 - (enemySpeed / waitTimeDivider);
        }
        
    }
}
