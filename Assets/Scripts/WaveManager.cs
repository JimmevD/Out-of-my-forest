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
    [SerializeField] TextMeshProUGUI waveCounter; 
    [SerializeField] TextMeshProUGUI enemiesCounter; 
    private Trees trees;
    [SerializeField] private GameObject lumberjacks;
    [SerializeField] private GameObject excavators;

    [SerializeField] private Transform[] randomSpawnPosistions;

    [SerializeField] WaveSpawnContent[] waveSpawnContent;
   
    void Start()
    {
        trees = FindObjectOfType<Trees>();
        waveCounter.text = "Starting";
        enemiesCounter.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(SpawnNewWaves());
        }

        if (countDownToNextWave)
        {
            timeBetweenWaves -= Time.deltaTime;
            enemiesCounter.text = timeBetweenWaves.ToString("F0");

            if (timeBetweenWaves < 0)
            {
                countDownToNextWave = false;
                SpawnNewWaves();
            }
        }  
    }

    private IEnumerator SpawnNewWaves()
    {
        trees.RemoveHologramTrees();
       
        for (int i = 0; i < waveSpawnContent[currentWave].lumberjackCount; i++)
        {
            GameObject go = Instantiate(lumberjacks, randomSpawnPosistions[Random.Range(0, randomSpawnPosistions.Length)].position, Quaternion.identity);
            go.GetComponent<Enemy>().trees = trees;
            go.GetComponent<Enemy>().waveManager = this;
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < waveSpawnContent[currentWave].excavatorCount; i++)
        {
            GameObject go = Instantiate(excavators, randomSpawnPosistions[Random.Range(0, randomSpawnPosistions.Length)].position, Quaternion.identity);
            go.GetComponent<Enemy>().trees = trees;
            go.GetComponent<Enemy>().waveManager = this;
            yield return new WaitForSeconds(0.2f);
        }

        enemiesCounter.enabled = true;
        totalEnemies = waveSpawnContent[currentWave].lumberjackCount + waveSpawnContent[currentWave].excavatorCount;
        enemiesCounter.text = "Enemies Remaining: " + totalEnemies.ToString();
        
        currentWave++;
        waveCounter.text = "Wave: " + currentWave.ToString();
    }

    public void KilledEnemy()
    {
        totalEnemies--;

        if (totalEnemies == 0)
        {
            trees.SpawnHoloGram();
            waveCounter.text = "PREPAIR FOR NEXT WAVE";
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
