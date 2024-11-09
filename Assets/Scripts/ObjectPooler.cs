using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledProjectiles;
    public GameObject projectileToPool;
    public int amountProjectilesToPool;
    public List<GameObject> pooledCollectibles;
    public GameObject collectiblesToPool;
    public int amountCollectiblesToPool;
    public List<GameObject> pooledBrides;
    public GameObject brideToPool;
    public int amountBridesToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create new List game object
        pooledProjectiles = new List<GameObject>();
        // Loop through list creating instances
        for (int i = 0; i < amountProjectilesToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(projectileToPool);
            obj.SetActive(false); // Deactivate the object
            pooledProjectiles.Add(obj);  // Add it to the list
            obj.transform.SetParent(this.transform);  // Set it to a child of this game object (spawnManager)
        }

        // Create new List game object
        pooledCollectibles = new List<GameObject>();
        // Loop through list creating instances
        for (int i = 0; i < amountCollectiblesToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(collectiblesToPool);
            obj.SetActive(false); // Deactivate the object
            pooledCollectibles.Add(obj);  // Add it to the list
            obj.transform.SetParent(this.transform);  // Set it to a child of this game object (spawnManager)
        }

        // Create new List game object
        pooledBrides = new List<GameObject>();
        // Loop through list creating instances
        for (int i = 0; i < amountBridesToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(brideToPool);
            obj.SetActive(false); // Deactivate the object
            pooledBrides.Add(obj);  // Add it to the list
            obj.transform.SetParent(this.transform);  // Set it to a child of this game object (spawnManager)
        }
    }

    public GameObject GetPooledProjectile()
    {
        // Check through all pooled objects in the list
        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            // Return a pooled object that is NOT currently active
            if (!pooledProjectiles[i].activeInHierarchy)
            {
                return pooledProjectiles[i];
            }
        }
        // Otherwise, return null
        return null;
    }

    public GameObject GetPooledCollectible()
    {
        // Check through all pooled objects in the list
        for (int i = 0; i < pooledCollectibles.Count; i++)
        {
            // Return a pooled object that is NOT currently active
            if (!pooledCollectibles[i].activeInHierarchy)
            {
                return pooledCollectibles[i];
            }
        }
        // Otherwise, return null
        return null;
    }

    public GameObject GetPooledBride()
    {
        // Check through all pooled objects in the list
        for (int i = 0; i < pooledBrides.Count; i++)
        {
            // Return a pooled object that is NOT currently active
            if (!pooledBrides[i].activeInHierarchy)
            {
                return pooledBrides[i];
            }
        }
        // Otherwise, return null
        return null;
    }
}
