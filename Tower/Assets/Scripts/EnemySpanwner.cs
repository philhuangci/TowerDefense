using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwner : MonoBehaviour
{
    public static int countEnemyAlive = 0;
    public Wave[] waves;
    public Transform START;
    public float waveRate = 3;
    private Coroutine coroutine;

    public void Stop()
    {
        // StopCoroutine(SpawnEnemy());
        StopCoroutine(coroutine);

    }


    private void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());

    }

    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i =0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                countEnemyAlive++;
                if (i != wave.count - 1) 
                    yield return new WaitForSeconds(wave.rate);
            }
            while(countEnemyAlive > 0)
            {
                yield return 0;
            }

            yield return new WaitForSeconds(waveRate);
        }
        while (countEnemyAlive > 0)
            yield return 0;

        GameManger.Instance.Win();
    }
}
