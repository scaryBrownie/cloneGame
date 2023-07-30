using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public float spawnYOffset = 10f;
    public float spawnDistance = 10f;
    public int maxObjectCount = 15;
    public float destroyDistance = 40f;

    public float plusX, lineX, firstObsPos;

    private List<GameObject> spawnedObjects;
    private Transform playerTransform;
    private int consecutiveCount = 0;
    private int lastSpawnedIndex = -1;
    private int highestObstacleCount = 0;
    

    public GameObject[] bObjectPrefabs;

    private void Start()
    {
        spawnedObjects = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnObjects();
    }

    private void Update()
    {
        if (playerTransform.position.y - GetLowestObjectY() >= destroyDistance)
        {
            DestroyLowestObject();
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                float playerPosY = playerObject.transform.position.y;

                foreach (GameObject obstacle in obstacles)
                {
                    float obstaclePosY = obstacle.transform.position.y;

                    if (obstaclePosY > playerPosY)
                    {
                        highestObstacleCount++;
                    }
                }

                if (highestObstacleCount < maxObjectCount)
                {
                    SpawnObject();
                }

                highestObstacleCount = 0;
            }

        }
}

    private void SpawnObjects()
    {
        float spawnY = firstObsPos;

        for (int i = 0; i < maxObjectCount; i++)
        {
            int randomIndex;
            GameObject objectPrefab;
            Vector3 spawnPosition;

            do
            {
                randomIndex = Random.Range(0, objectPrefabs.Length);
                objectPrefab = objectPrefabs[randomIndex];

                if (objectPrefab.name.StartsWith("Plus"))
                {
                    spawnPosition = new Vector3(plusX, spawnY, transform.position.z);
                }
                else if (objectPrefab.name.StartsWith("Line"))
                {
                    spawnPosition = new Vector3(lineX, spawnY, transform.position.z);
                }
                else
                {
                    spawnPosition = new Vector3(transform.position.x, spawnY, transform.position.z);
                }
            }
            while (randomIndex == lastSpawnedIndex && consecutiveCount >= 2);

           
            float randomValue = Random.value;
            if (randomValue < 0.5f && bObjectPrefabs.Length > 0)
            {
                int randomBIndex = Random.Range(0, bObjectPrefabs.Length);
                GameObject bObjectPrefab = bObjectPrefabs[randomBIndex];
                float bSpawnY = (spawnY + GetHighestObjectY()) / 2f;
                GameObject newBObject = Instantiate(bObjectPrefab, new Vector3(0f, bSpawnY, transform.position.z), Quaternion.identity);
                spawnedObjects.Add(newBObject);
            }

            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(newObject);

            if (randomIndex == lastSpawnedIndex)
            {
                consecutiveCount++;
            }
            else
            {
                lastSpawnedIndex = randomIndex;
                consecutiveCount = 1;
            }

            spawnY += spawnDistance;
        }
    }

    private float GetLowestObjectY()
    {
        float lowestY = float.MaxValue;
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            if (spawnedObject != null && spawnedObject.transform.position.y < lowestY)
            {
                lowestY = spawnedObject.transform.position.y;
            }
        }
        return lowestY;
    }

    private void DestroyLowestObject()
    {
        GameObject lowestObject = null;
        float lowestY = float.MaxValue;
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            if (spawnedObject != null && spawnedObject.transform.position.y < lowestY)
            {
                lowestY = spawnedObject.transform.position.y;
                lowestObject = spawnedObject;
            }
        }
        spawnedObjects.Remove(lowestObject);
        Destroy(lowestObject);
    }

    private void SpawnObject()
    {
        int randomIndex;
        GameObject objectPrefab;
        Vector3 spawnPosition;

        do
        {
            randomIndex = Random.Range(0, objectPrefabs.Length);
            objectPrefab = objectPrefabs[randomIndex];

            float highestY = GetHighestObjectY();

            if (objectPrefab.name.StartsWith("Plus"))
            {
                spawnPosition = new Vector3(plusX, highestY + spawnDistance, transform.position.z);
            }
            else if (objectPrefab.name.StartsWith("Line"))
            {
                spawnPosition = new Vector3(lineX, highestY + spawnDistance, transform.position.z);
            }
            else
            {
                spawnPosition = new Vector3(transform.position.x, highestY + spawnDistance, transform.position.z);
            }
        }
        while (randomIndex == lastSpawnedIndex && consecutiveCount >= 2);

        
        float randomValue = Random.value;
        if (randomValue < 0.5f && bObjectPrefabs.Length > 0)
        {
            int randomBIndex = Random.Range(0, bObjectPrefabs.Length);
            GameObject bObjectPrefab = bObjectPrefabs[randomBIndex];
            float bSpawnY = (spawnPosition.y + GetHighestObjectY()) / 2f;
            GameObject newBObject = Instantiate(bObjectPrefab, new Vector3(0f, bSpawnY, transform.position.z), Quaternion.identity);
            spawnedObjects.Add(newBObject);
        }

        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(newObject);

        if (randomIndex == lastSpawnedIndex)
        {
            consecutiveCount++;
        }
        else
        {
            lastSpawnedIndex = randomIndex;
            consecutiveCount = 1;
        }
    }

    private float GetHighestObjectY()
    {
        float highestY = float.MinValue;
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            if (spawnedObject != null && spawnedObject.transform.position.y > highestY)
            {
                highestY = spawnedObject.transform.position.y;
            }
        }
        return highestY;
    }
}

