using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnInterval = 10f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject(){
        Instantiate(enemyToSpawn,transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
