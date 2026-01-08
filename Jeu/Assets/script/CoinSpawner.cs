using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int numberOfCoins = 10;
    public float spawnRadius = 10f;
    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; )
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = 1f; // keep above ground
            Instantiate(coinPrefab, randomPos, Quaternion.identity);
            if (i < numberOfCoins - 1)
                yield return new WaitForSeconds(spawnInterval);
        }
    }
}
