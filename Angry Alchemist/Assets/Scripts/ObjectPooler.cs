using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType { UIPotion}

public interface IPooledObject
{
    void OnObjectSpawn();
}

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public PoolType tag;
        public GameObject prefab;
        public int size;
    }

    public struct PoolQueues
    {
        public Queue<GameObject> activeQueue, inactiveQueue;
    }

    public static ObjectPooler instance;


    public List<Pool> pools;
    public Dictionary<PoolType, PoolQueues> poolDictionary;

    public GameObject poolContainer;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<PoolType, PoolQueues>();

        #region Initialize Pools
        foreach(Pool pool in pools)
        {
            PoolQueues objectPools = new PoolQueues();
            objectPools.inactiveQueue = new Queue<GameObject>();
            objectPools.activeQueue = new Queue<GameObject>();
            GameObject go = new GameObject(pool.tag.ToString());
            go.transform.SetParent(poolContainer.transform);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, go.transform);
                obj.SetActive(false);
                objectPools.inactiveQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPools);
        }
        #endregion
    }

    public GameObject SpawnFromPool(PoolType tag, Vector3 pos, Quaternion rot)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Object pool with tag " + tag.ToString() + " does not exist");
            return null;
        }
        if(poolDictionary[tag].inactiveQueue.Count == 0)
        {
            Debug.LogWarning("Object pool with tag " + tag.ToString() + " has no more inactive entities to choose from! Increase the pool size!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].inactiveQueue.Dequeue();
        poolDictionary[tag].activeQueue.Enqueue(objectToSpawn);

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
            pooledObj.OnObjectSpawn();

        return objectToSpawn;
    }

    public void DespawnFromPool(PoolType tag, GameObject gameObject)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Object pool with tag " + tag.ToString() + " does not exist");
            return;
        }

        poolDictionary[tag].activeQueue.Dequeue();
        poolDictionary[tag].inactiveQueue.Enqueue(gameObject);

        gameObject.SetActive(false);


    }


}
