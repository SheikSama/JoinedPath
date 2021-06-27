using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    public bool stopSpawns = false;

    public GameObject BasicEnemiePrefab;
    public GameObject HorizontalEnemiePrefab;
    public GameObject BigBadBossPrefab;


    public List<GameObject> spawners;

    public const float verticalSpanwerTime1 = 3f;
    public const float verticalSpanwerTime2 = 5f;
    public const float horizontalSpanwerTime1 = 4f;
    public const float BBBSpanwerTime1 = 25f;

    public float randomTimerRange = 2f;
    public float randomBBBTimerRange = 12f;

    public float VspawnerTick1 = 3f;
    public float VspawnerTick2 = 5f;
    public float BBBspawnerTick = 25f;
    public float HspawnerTick1 = 4f;

    // Start is called before the first frame update
    void Start()
    {
        VspawnerTick1 = Random.Range(verticalSpanwerTime1 - randomTimerRange, verticalSpanwerTime1); 
        VspawnerTick2 = Random.Range(verticalSpanwerTime2 - randomTimerRange, verticalSpanwerTime2); 
        HspawnerTick1 = Random.Range(horizontalSpanwerTime1 - randomTimerRange, horizontalSpanwerTime1); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopSpawns)
        {
            VspawnerTick1 -= Time.deltaTime;

            if (VspawnerTick1 <= 0.0f)
            {
                SpawnVerticalRandomEnemie1();
            }

            VspawnerTick2 -= Time.deltaTime;

            if (VspawnerTick2 <= 0.0f)
            {
                SpawnVerticalRandomEnemie2();
            }

            HspawnerTick1 -= Time.deltaTime;

            if (HspawnerTick1 <= 0.0f)
            {
                SpawnHorizontalRandomEnemie1();
            }

            BBBspawnerTick -= Time.deltaTime;

            if (BBBspawnerTick <= 0.0f)
            {
                SpawnBigBadBoss();
            }
        }
        
    }

    private void SpawnVerticalRandomEnemie1()
    {

        int randomSpawnerPos = Random.Range(0, spawners.Count-1);

        VspawnerTick1 = Random.Range(verticalSpanwerTime1 - randomTimerRange, verticalSpanwerTime1);

        Transform selectedSpawner = spawners[randomSpawnerPos].transform;

        Vector3 posToSpawn = selectedSpawner.position;
        posToSpawn.z = 0;

        Instantiate(BasicEnemiePrefab, posToSpawn, selectedSpawner.rotation);

    }

    private void SpawnVerticalRandomEnemie2()
    {

        int randomSpawnerPos = Random.Range(0, spawners.Count - 1);

        VspawnerTick2 = Random.Range(verticalSpanwerTime2 - randomTimerRange, verticalSpanwerTime2);

        Transform selectedSpawner = spawners[randomSpawnerPos].transform;

        Vector3 posToSpawn = selectedSpawner.position;
        posToSpawn.z = 0;

        Instantiate(BasicEnemiePrefab, posToSpawn, selectedSpawner.rotation);

    }

    private void SpawnBigBadBoss()
    {

        int randomSpawnerPos = Random.Range(0, spawners.Count - 1);

        BBBspawnerTick = Random.Range(BBBSpanwerTime1 - randomBBBTimerRange, BBBSpanwerTime1);

        Transform selectedSpawner = spawners[randomSpawnerPos].transform;

        Vector3 posToSpawn = selectedSpawner.position;
        posToSpawn.z = 0;

        Instantiate(BigBadBossPrefab, posToSpawn, selectedSpawner.rotation);

    }

    private void SpawnHorizontalRandomEnemie1()
    {
        int direction;
        Transform selectedSpawner;
        int randomSpawnerPos = Random.Range(0, 2);

        HspawnerTick1 =  Random.Range(horizontalSpanwerTime1 - randomTimerRange, horizontalSpanwerTime1); 

        if (randomSpawnerPos > 0)
        {
            selectedSpawner = GameObject.Find("LeftSpawner").transform;
            direction = 1;

        }
        else
        {
            selectedSpawner = GameObject.Find("RightSpawner").transform;
            direction = -1;
        }

        Vector3 posToSpawn = selectedSpawner.position;
        posToSpawn.z = 0;

        GameObject enemie = Instantiate(HorizontalEnemiePrefab, posToSpawn, selectedSpawner.rotation);
        enemie.GetComponent<HorizontalEnemieController>().SetDirection(direction);

    }



}
