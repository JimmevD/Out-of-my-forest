using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    private int currentWave;
    private int totalEnemies;
    private float timeBetweenWaves;
    private bool countDownToNextWave;
    private float timeUntilBegin;
    private bool isBeginning;
    [SerializeField] TextMeshProUGUI waveCounter; 
    [SerializeField] TextMeshProUGUI enemiesCounter;
    [SerializeField] TextMeshProUGUI skipWaitTime; 
    private Trees trees;
    [SerializeField] private GameObject lumberjacks;
    [SerializeField] private GameObject excavators;

    [SerializeField] private Transform[] randomSpawnPosistions;

    [SerializeField] WaveSpawnContent[] waveSpawnContent;
   
    void Start()
    {
        trees = FindObjectOfType<Trees>();

        isBeginning = true;
        waveCounter.text = "Starting in";
        timeUntilBegin = 10;
    }

    private void Update()
    {
        if (isBeginning)
        {
            timeUntilBegin -= Time.deltaTime;
            enemiesCounter.text = timeUntilBegin.ToString("F0");

            if (timeUntilBegin < 0)
            {
                StartCoroutine(SpawnNewWaves());
                isBeginning = false;
            }
        }

        if (countDownToNextWave)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                timeBetweenWaves = 0.1f;
            }

            timeBetweenWaves -= Time.deltaTime;
            enemiesCounter.text = timeBetweenWaves.ToString("F0");

            if (timeBetweenWaves < 0)
            {
                countDownToNextWave = false;
                StartCoroutine(SpawnNewWaves());
            }
        }  
    }

    private IEnumerator SpawnNewWaves()
    {
        skipWaitTime.enabled = false;
        trees.RemoveHologramTrees();
       
        for (int i = 0; i < waveSpawnContent[currentWave].lumberjackCount; i++)
        {
            GameObject go = Instantiate(lumberjacks, randomSpawnPosistions[Random.Range(0, randomSpawnPosistions.Length)].position, Quaternion.identity);
            go.GetComponent<Enemy>().trees = trees;
            go.GetComponent<Enemy>().waveManager = this;
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < waveSpawnContent[currentWave].excavatorCount; i++)
        {
            GameObject go = Instantiate(excavators, randomSpawnPosistions[Random.Range(0, randomSpawnPosistions.Length)].position, Quaternion.identity);
            go.GetComponent<Enemy>().trees = trees;
            go.GetComponent<Enemy>().waveManager = this;
            yield return new WaitForSeconds(0.05f);
        }

        enemiesCounter.enabled = true;
        totalEnemies = waveSpawnContent[currentWave].lumberjackCount + waveSpawnContent[currentWave].excavatorCount;
        enemiesCounter.text = "Enemies Remaining: " + totalEnemies.ToString();
        
        currentWave++;
        waveCounter.text = "Wave: " + currentWave.ToString();

        if (currentWave == waveSpawnContent.Length)
        {
            waveCounter.text = "Last Wave";
        }
    }

    public void KilledEnemy()
    {
        totalEnemies--;
        enemiesCounter.text = "Enemies Remaining: " + totalEnemies.ToString();

        if (totalEnemies == 0)
        {
            if (currentWave == waveSpawnContent.Length)
            {
                EndScreen.EndScenario(true);
                return;
            }

            trees.SpawnHoloGram();
            waveCounter.text = "PREPAIR FOR NEXT WAVE";
            skipWaitTime.enabled = true;
            countDownToNextWave = true;
            timeBetweenWaves = 30;
        }
    }

    [System.Serializable]
    public struct WaveSpawnContent
    {
        public int lumberjackCount;
        public int excavatorCount;
    }
}
