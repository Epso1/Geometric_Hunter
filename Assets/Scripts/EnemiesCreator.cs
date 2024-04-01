using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float enemySpeed = 10f;
    [SerializeField] private float initialWait = 2f;
    [SerializeField] private float waitTime = 0.75f;
    [SerializeField] private int enemyQuantity = 100;


    void Start()
    {
        StartCoroutine(CreateEnemies());
    }   
    
    void Update()
    {
        
    }

    IEnumerator CreateEnemies()
    {
        yield return new WaitForSeconds(initialWait);
        for (int i = 0; i < enemyQuantity; i++)
        {
            int randomInt = Random.Range(0, enemies.Length);
            GameObject enemy = Instantiate(enemies[randomInt], this.transform.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().Speed = enemySpeed;   
            yield return new WaitForSeconds(waitTime);
        }
    }
}
