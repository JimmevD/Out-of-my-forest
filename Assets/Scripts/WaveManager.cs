using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWave;
    private Trees trees;
    [SerializeField] private GameObject lumberjacks;
    [SerializeField] private GameObject excavators;

    [SerializeField] private Transform[] randomSpawnPosistions;

    [SerializeField] WaveSpawnContent[] waveSpawnContent;
   
    void Start()
    {
        trees = FindObjectOfType<Trees>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(SpawnNewWaves());
        }
    }

    private IEnumerator SpawnNewWaves()
    {
        for (int i = 0; i < waveSpawnContent[currentWave].lumberjackCount; i++)
        {
            GameObject go = Instantiate(lumberjacks, randomSpawnPosistions[Random.Range(0, randomSpawnPosistions.Length)].position, Quaternion.identity);
            go.GetComponent<Enemy>().trees = trees;
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < waveSpawnContent[currentWave].excavatorCount; i++)
        {
            GameObject go = Instantiate(excavators, randomSpawnPosistions[Random.Range(0, randomSpawnPosistions.Length)].position, Quaternion.identity);
            go.GetComponent<Enemy>().trees = trees;
            yield return new WaitForSeconds(0.2f);
        }

        currentWave++;
    }



    [System.Serializable]
    public struct WaveSpawnContent
    {
        public int lumberjackCount;
        public int excavatorCount;
    }
}
