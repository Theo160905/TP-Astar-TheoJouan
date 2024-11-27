using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 10;
    private Queue<GameObject> poolQueue;

    public List<Box> Box;

    private Box firstSelecteBox;
    private Box secondSelectedBox;

    void Start()
    {
        poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }

        OnSpawnBomb();
    }

    public GameObject GetBomb(GameObject gameObject)
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.gameObject.transform.position = gameObject.transform.position;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(objectPrefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
        OnSpawnBomb();
    }

    public void OnSpawnBomb()
    {
        List<Box> availableObjects = new List<Box>();

        foreach (var box in Box)
        {
            Box boxScript = box.GetComponent<Box>();
            if (boxScript != null && !boxScript.IsOccupied)
            {
                availableObjects.Add(box);
            }
        }

        if (availableObjects.Count >= 2)
        {
            firstSelecteBox = availableObjects[Random.Range(0, availableObjects.Count)];
            availableObjects.Remove(firstSelecteBox);
            GetBomb(firstSelecteBox.gameObject);

            secondSelectedBox = availableObjects[Random.Range(0, availableObjects.Count)];
            GetBomb(secondSelectedBox.gameObject);
        }
        
    }
}
