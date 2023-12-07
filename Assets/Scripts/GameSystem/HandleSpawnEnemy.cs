using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HandleSpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private HandleWinLose winLose;

    [SerializeField] private float spawnTimerMax;
    private float spawnTimer;
    private int spawnTimesMax = 4;
    private bool continueSpawn = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        spawnTimer = spawnTimerMax;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        //print(spawnTimer);
        if (spawnTimer <= 320f && spawnTimer >= 250f)
        {
            //print("faster");
            HandleWaveHint.Instance.SetHint("FIRST WAVE");
            SetSpawn(6, 5f);
        }
        else if (spawnTimer <= 250f && spawnTimer >= 200f)
        {
            //print("slower");
            HandleWaveHint.Instance.HideHint();
            SetSpawn(2, 25f);
        }
        else if (spawnTimer <= 100f && spawnTimer >= 50f)
        {
            //print("faster");
            HandleWaveHint.Instance.SetHint("LAST WAVE");
            SetSpawn(8, 3f);
        }
        else
        {
            //print("normal
            HandleWaveHint.Instance.HideHint();
            SetSpawn(2, 15f);
        }

        if (spawnTimer <= 10)
        {
            //print("Stop");
            if (continueSpawn)
            {
                continueSpawn = false;
            }
        }

        if (spawnTimer <= 0)
        {
            winLose.Win();
        }
    }

    public float GetSpawnTimerNormalize()
    {
        //print((int)(spawnTimerMax - spawnTimer) / spawnTimerMax);
        return (spawnTimerMax - spawnTimer) / spawnTimerMax;
    }

    private void SetSpawn(int max, float interval)
    {
        spawnTimesMax = max;
        spawnInterval = interval;
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnInterval);

        System.Random random = new();
        int spawnTimes = random.Next(0, spawnTimesMax);
        for (int i = 0; i <= spawnTimes; i++)
        {
            int point = random.Next(0, 4);
            Instantiate(enemyPrefab, spawnPoints[point].position, Quaternion.identity);
        }

        if (continueSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }
}
