using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastlingSpawner : MonoBehaviour
{
    GameObject player;
    public GameObject enemyPrefab;

    public float MinimumTimeSpawn = 3;
    public float MaximumTimeSpawn = 3;
    public int spawnRange = 70;
    public float spawnAreaRange = 3;
    public int direction = 1;
    private float spawnTimer = 3f;
    bool deactivate = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        RandomAndInitSpawnerTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!deactivate)
        {
            float distanceToPLayer = Vector2.Distance(this.transform.position, player.transform.position);
            if (distanceToPLayer < spawnRange)
            {
                spawnTimer -= Time.deltaTime;

                if (spawnTimer <= 0.0f)
                {
                    SpawnEnemie();
                }
            }
        }
    }

    public void RandomAndInitSpawnerTime()
    {
        spawnTimer = Random.Range(MinimumTimeSpawn, MaximumTimeSpawn);
    }

    public void SpawnEnemie()
    {

        Vector3 posToSpawn = Random.insideUnitCircle * spawnAreaRange;

        GameObject enemie = Instantiate(enemyPrefab, this.transform.position + posToSpawn, this.transform.rotation);
        enemie.GetComponent<HorizontalEnemieController>().SetDirection(direction);
        RandomAndInitSpawnerTime();

    }

    private void OnWillRenderObject()
    {
        deactivate = true;
        Debug.Log("Spawner en camera");
    }
}
