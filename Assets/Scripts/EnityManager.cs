using System.Collections.Generic;
using UnityEngine;

public class EnityManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> entityList = new List<GameObject>();

    [SerializeField] GameObject entityPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] List<GameObject> mushroomPrefabs;
    [SerializeField] GameObject PortalPrefab;
    private int mushroomIndex = 0;

    private float spawnDelayTime = 1;
    private float spawnTime = 0;

    private float spawnCoinDelayTime = 10;
    private float spawnCoinTime = 0;

    private float spawnMushroomDelayTime = 20;
    private float spawnMushroomTime = 0;

    private float spawnPortalDelayTime = 60;
    private float spawnPortalTime = 0;

    public int MaxEntity { get; } = 10;
    public int EntityCount { get; private set; } = 0;
    public static EnityManager instance
    {
        get; private set;
    }

    private void Start()
    {
        instance = this;

        spawnCoinTime = Time.time + spawnCoinDelayTime;
        spawnMushroomTime = Time.time + spawnMushroomDelayTime;
        spawnPortalTime = Time.time + spawnPortalDelayTime;
    }

    public void SpawnEntity(float x, float z, Vector3 triggerPos)
    {
        //Debug.Log("Куб " + x + " на " + z + " координаты:" + triggerPos);
        if (Time.time > spawnTime) //задержка между спавнами
        {
            spawnTime = Time.time + spawnDelayTime;

            Vector3 pos = new Vector3(Random.Range(-x, x), entityPrefab.transform.localScale.y, Random.Range(-z, z)) + triggerPos; //координаты для спавна entity

            GameObject entity = SpawnSmallOrBigEntity(pos);
            entityList.Add(entity);
        }
    }

    public int GetEntityCount()
    {
        return entityList.Count;
    }

    public void DestroyVisibleEntity()
    {
        for (int i = 0; i < entityList.Count; i++)
        {
            EnityVisibleOrNotVisible enity = entityList[i].GetComponent<EnityVisibleOrNotVisible>();
            if (enity)
            {
                if (!enity.isVisible)
                {
                    RemoveAndDestroyObjectInEnityList(enity.gameObject);
                }
            }
        }
    }

    public void RemoveAndDestroyObjectInEnityList(GameObject _gameObject)
    {
        RemoveAndDestroyObjectInEnityList(_gameObject, 0f);
    }

    public void RemoveAndDestroyObjectInEnityList(GameObject _gameObject, float destroyTime)
    {
        Destroy(_gameObject, destroyTime);
        entityList.Remove(_gameObject);
    }

    private GameObject SpawnSmallOrBigEntity(Vector3 spawnPosition)
    {
        GameObject entity = default;

        if (Time.time >= spawnCoinTime)
        {
            entity = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            spawnCoinTime = Time.time + spawnCoinDelayTime;
        }
        else if (Time.time >= spawnMushroomTime)
        {
            entity = GetMashroomPrefab(spawnPosition);
            spawnMushroomTime = Time.time + spawnMushroomDelayTime;
        }
        else if (Time.time >= spawnPortalTime)
        {
            entity = Instantiate(PortalPrefab, spawnPosition, Quaternion.identity);
            spawnPortalTime = Time.time + spawnPortalDelayTime;
        }
        else
        {
            int bigEntityCount = 0;
            int smallEntityCount = 0;

            for (int i = 0; i < entityList.Count; i++) //кого больше/меньше
            {
                if (entityList[i].transform.localScale == entityPrefab.transform.localScale) //маленький
                {
                    smallEntityCount++;
                }
                else if (entityList[i].transform.localScale == entityPrefab.transform.localScale * 2) //больших
                {
                    bigEntityCount++;
                }
            }


            entity = Instantiate(entityPrefab, spawnPosition, Quaternion.identity);
            entity.transform.localScale *= Random.Range(1, 3);

            if (bigEntityCount >= MaxEntity * 0.6f) //если больших больше чем 60 процентов
            {
                entity.transform.localScale /= 2;
            }
            else if (smallEntityCount >= MaxEntity * 0.4f) //если маленьких больше чем 40 процентов
            {
                entity.transform.localScale *= 2;
            }
        }

        entity.transform.position = new Vector3(spawnPosition.x, entity.transform.localScale.y + 0.5f, spawnPosition.z);

        return entity;
    }

    private GameObject GetMashroomPrefab(Vector3 spawnPosition)
    {
        mushroomIndex = (mushroomIndex + 1) % (mushroomPrefabs.Count - 1);
        return Instantiate(mushroomPrefabs[mushroomIndex], spawnPosition, Quaternion.identity);
    }
}
