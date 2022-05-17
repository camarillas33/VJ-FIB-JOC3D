using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject barrelPrefab;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    public float initX, initY, initZ, initX2, initY2, initZ2;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(asteroidWave());
    }
    private void spawnEnemy()
    {
        GameObject a = Instantiate(barrelPrefab) as GameObject;
        a.transform.position = new Vector3(initX, initY, initZ);
        GameObject b = Instantiate(barrelPrefab) as GameObject;
        b.transform.position = new Vector3(initX2, initY2, initZ2);
    }
    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        } 
    }
}
