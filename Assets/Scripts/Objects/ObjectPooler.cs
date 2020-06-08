using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    List<GameObject> pool;
    int numPooledObjects;
    GameObject prefab;

    public ObjectPooler(int num, GameObject prefab)
    {
        this.prefab = prefab;
        pool = new List<GameObject>();
        numPooledObjects = num;
        for (int o = 0; o < num; o++)
        {
            GameObject bullet =  Object.Instantiate(prefab);
            pool.Add(bullet);
            bullet.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int o = 0; o < pool.Count; o++)
            if (!pool[o].activeInHierarchy)
                return pool[o];

        GameObject bullet = Object.Instantiate(prefab);
        pool.Add(bullet);
        bullet.SetActive(false);

        return bullet;
    }
}
