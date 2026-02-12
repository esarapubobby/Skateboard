using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    public int tilesOnScreen = 5;

    public Transform player;

    private List<GameObject> activeTiles = new List<GameObject>();

    private Vector3 nextSpawnPosition;
    private Quaternion nextSpawnRotation;
    public int initialStraightTiles = 1;
    private int spawnedTiles = 0;
    private int currentIndex = 0;

    void Start()
    {
        nextSpawnPosition = Vector3.zero;
        nextSpawnRotation = Quaternion.identity;

        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
         if (activeTiles.Count == 0) return;

        float distance = Vector3.Distance(
            player.position,
            activeTiles[activeTiles.Count - 1].transform.position
        );

        if (distance < 25f) // tile length 20 + buffer
        {
            SpawnTile();
            DeleteTile();
        }

    }

    void SpawnTile()
    {
        GameObject tile;
        
        if (spawnedTiles < initialStraightTiles)
        {
            tile = Instantiate(tilePrefabs[0], nextSpawnPosition, nextSpawnRotation);
        }
        else
        {
            tile = Instantiate(
                tilePrefabs[currentIndex],
                nextSpawnPosition,
                nextSpawnRotation
            );

            currentIndex++;

            if (currentIndex >= tilePrefabs.Length)
                currentIndex = 0;
        }

        activeTiles.Add(tile);

        Transform exitPoint = tile.transform.Find("ExitPoint");
        Debug.Log(exitPoint.position);
        nextSpawnPosition = exitPoint.position;
        nextSpawnRotation = exitPoint.rotation;

        spawnedTiles++;

    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }



}
