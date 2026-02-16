using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{ public GameObject coinPrefab;
    public int coinsPerTile = 3;
    public float heightOffset = 0.5f;

    private Stack<GameObject> coinPool = new Stack<GameObject>();

    public void SpawnCoinsOnTile(GameObject tile)
    {
        Transform exitPoint = tile.transform.Find("ExitPoint");

        Vector3 startPos = tile.transform.position;
        Vector3 endPos = exitPoint.position;

        Vector3 direction = (endPos - startPos).normalized;
        float tileLength = Vector3.Distance(startPos, endPos);

        for (int i = 1; i <= coinsPerTile; i++)
        {
            GameObject coin = GetCoinFromPool();

            float distance = (tileLength / (coinsPerTile + 1)) * i;

            Vector3 spawnPos = startPos + direction * distance;

            spawnPos.y += heightOffset;

            coin.transform.position = spawnPos;
            coin.transform.rotation = Quaternion.Euler(90f,0f,0f)*tile.transform.rotation;

            coin.SetActive(true);
        }
    }

    GameObject GetCoinFromPool()
    {
        if (coinPool.Count > 0)
            return coinPool.Pop();

        GameObject coin = Instantiate(coinPrefab);
        coin.SetActive(false);
        return coin;
    }
    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Push(coin);
    }


}