using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When Using this pool, remember to restart the prefab behavior

/// <summary>
/// Object pool of french fries
/// </summary>
public class PrefabPool
{
    GameObject prefab;
    List<GameObject> pool;

    public PrefabPool(GameObject prefab)
    {
        this.prefab = prefab;
        pool = new List<GameObject>();
        for (int i = 0; i < pool.Capacity; i++)
        {
            pool.Add(GetNewPrefab());
        }
    }

    public void ClearPool()
    {
        pool.Clear();
    }

    public GameObject GetPrefab()
    {
        // check for available object in pool
        if (pool.Count > 0)
        {
            GameObject pref = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            pref.SetActive(true);
            return pref;
        }
        else
        {
            //Debug.Log("Growing pool ...");

            // pool empty, so expand pool and return new object
            pool.Capacity++;
            return GetNewPrefab();
        }
    }

    public void ReturnPrefab2Pool(GameObject pref)
    {
        pref.SetActive(false);
       
        pool.Add(pref);
    }

    /// <summary>
    /// Gets a new french fries object
    /// </summary>
    /// <returns>french fries</returns>
    GameObject GetNewPrefab()
    {
        GameObject pref = GameObject.Instantiate(prefab);

        //GameObject.DontDestroyOnLoad(pref);
        return pref;
    }
}
