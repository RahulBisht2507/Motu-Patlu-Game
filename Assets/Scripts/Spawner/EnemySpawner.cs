using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int xpoz;
    public int zpoz;
    public int enemyCount;
    bool ready;

    private void Start()
    {
        StartCoroutine(EnemyDrop());
        
    }
    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            xpoz = Random.Range(100,900);
            zpoz = Random.Range(50, 250);
            Instantiate(enemy, new Vector3(xpoz,43, zpoz), Quaternion.identity);
            yield return new WaitForSeconds(1f);
            enemyCount++;
            if(enemyCount == 1)
            {
                ready = true;
            }
        }
    }
    private void Update()
    {
        if (enemyCount == 0)
        {
            StartCoroutine(EnemyDrop());
        }
    }
}
