using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amount;
    public GameObject objectToPool;
    public bool canGrow = true;
}

public class PoolerScript : MonoBehaviour
{
    public static PoolerScript current;

    public Transform moleContainer;

    public List<ObjectPoolItem> itemsToPool;

    private List<GameObject> pooledObjects;

    private void Awake()
    {
        current = this;

        pooledObjects = new List<GameObject>();
        
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool, moleContainer);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.canGrow)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool, moleContainer);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
