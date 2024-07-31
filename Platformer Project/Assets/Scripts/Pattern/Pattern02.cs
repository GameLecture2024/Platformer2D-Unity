using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern02 : MonoBehaviour
{
    public GameObject warningSign;
    public GameObject enemyPrefab;

    // 몇 마리 소환할 것인가?
    public int spawnCount = 1;
    // 몇 초를 주기로 생성할 것인가?
    public float spawnCycle = 1f;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(-8.53f, 1.54f, 0);
        GameObject warning = Instantiate(warningSign, spawnPos, Quaternion.identity);   
        
        yield return new WaitForSeconds(1f);
        Destroy(warning);
        CreateEnemyInstance(1);


        gameObject.SetActive(false);
    }

    private void CreateEnemyInstance(int count)
    {
        Vector3 spawnPos = new Vector3(-8.53f, 1.54f, 0);
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }


}
