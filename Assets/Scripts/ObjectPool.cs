using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    public int initialPoolSize = 10;

    public List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Если все объекты заняты, создаем новый
        GameObject newObj = Instantiate(objectPrefab);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
