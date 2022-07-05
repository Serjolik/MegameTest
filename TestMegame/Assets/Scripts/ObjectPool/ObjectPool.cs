using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    private List<GameObject> pooledBulletObjects;
    private List<GameObject> pooledBigAsteroidObjects;
    private List<GameObject> pooledMediumAsteroidObjects;
    private List<GameObject> pooledSmallAsteroidObjects;
    public GameObject BulletPool;
    public GameObject BigAsteroidPool;
    public GameObject MediumAsteroidPool;
    public GameObject SmallAsteroidPool;
    public int amountBulletPool;
    public int amountBigAsteroidPool;
    private int amountMediumAsteroidPool;
    private int amountSmallAsteroidPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        amountMediumAsteroidPool = amountBigAsteroidPool * 2;
        amountSmallAsteroidPool = amountBigAsteroidPool * 2 * 2;
        pooledBulletObjects = new List<GameObject>();
        pooledBigAsteroidObjects = new List<GameObject>();
        pooledMediumAsteroidObjects = new List<GameObject>();
        pooledSmallAsteroidObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amountBulletPool; i++)
        {
            tmp = Instantiate(BulletPool);
            tmp.SetActive(false);
            pooledBulletObjects.Add(tmp);
        }

        for (int i = 0; i < amountSmallAsteroidPool; i++)
        {
            if (i < amountBigAsteroidPool)
            {
                tmp = Instantiate(BigAsteroidPool);
                tmp.SetActive(false);
                pooledBigAsteroidObjects.Add(tmp);
            }
            if (i < amountMediumAsteroidPool)
            {
                tmp = Instantiate(MediumAsteroidPool);
                tmp.SetActive(false);
                pooledMediumAsteroidObjects.Add(tmp);
            }
            tmp = Instantiate(SmallAsteroidPool);
            tmp.SetActive(false);
            pooledSmallAsteroidObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject(string objectType)
    {
        switch (objectType)
        {
            case ("Bullet"):
                for (int i = 0; i < amountBulletPool; i++)
                {
                    if (!pooledBulletObjects[i].activeInHierarchy)
                    {
                        return pooledBulletObjects[i];
                    }
                }
                break;
            case ("BigAsteroid"):
                for (int i = 0; i < amountBigAsteroidPool; i++)
                {
                    if (!pooledBigAsteroidObjects[i].activeInHierarchy)
                    {
                        return pooledBigAsteroidObjects[i];
                    }
                }
                break;
            case ("MediumAsteroid"):
                for (int i = 0; i < amountMediumAsteroidPool; i++)
                {
                    if (!pooledMediumAsteroidObjects[i].activeInHierarchy)
                    {
                        return pooledMediumAsteroidObjects[i];
                    }
                }
                break;
            case ("SmallAsteroid"):
                for (int i = 0; i < amountSmallAsteroidPool; i++)
                {
                    if (!pooledSmallAsteroidObjects[i].activeInHierarchy)
                    {
                        return pooledSmallAsteroidObjects[i];
                    }
                }
                break;
            default:
                Debug.Log("Unknown object type");
                break;
        }
        return NewPool(objectType);
    }

    private GameObject NewPool(string objectType)
    {
        GameObject tmp;
        if (objectType == "Bullet")
        {
            tmp = Instantiate(BulletPool);
            tmp.SetActive(false);
            pooledBulletObjects.Add(tmp);
            amountBulletPool++;
            return pooledBulletObjects[amountBulletPool - 1];
        }
        else if (objectType == "BigAsteroid")
        {
            tmp = Instantiate(BigAsteroidPool);
            tmp.SetActive(false);
            pooledBigAsteroidObjects.Add(tmp);
            amountBigAsteroidPool++;
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    tmp = Instantiate(MediumAsteroidPool);
                    tmp.SetActive(false);
                    pooledMediumAsteroidObjects.Add(tmp);
                    amountMediumAsteroidPool++;
                }
                tmp = Instantiate(SmallAsteroidPool);
                tmp.SetActive(false);
                pooledSmallAsteroidObjects.Add(tmp);
                amountSmallAsteroidPool++;
            }
            return pooledBigAsteroidObjects[amountBigAsteroidPool - 1];
        }
        return null;
    }
}
