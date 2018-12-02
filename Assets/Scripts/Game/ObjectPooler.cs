﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour 
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    #region simpleton
    public static ObjectPooler Instance;
    private void Awake ()
    {
        Instance = this;
    }
    #endregion

    void Start () 
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
	
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Use the same way as Instantiate
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag)){
            return null;
        }

        GameObject spawnObject = poolDictionary[tag].Dequeue();

        // requeue
        poolDictionary[tag].Enqueue(spawnObject);

        // set active, position, and rotation
        spawnObject.transform.position = position;
        spawnObject.transform.rotation = rotation;
        spawnObject.SetActive(true);

        return spawnObject;
    }
}
