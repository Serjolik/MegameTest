using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    private List<GameObject> pooledBulletObjects = new List<GameObject>();
    private List<GameObject> pooledBigAsteroidObjects = new List<GameObject>();
    private List<GameObject> pooledMediumAsteroidObjects = new List<GameObject>();
    private List<GameObject> pooledSmallAsteroidObjects = new List<GameObject>();
    public GameObject BulletPool;
    public GameObject BigAsteroidPool;
    public GameObject MediumAsteroidPool;
    public GameObject SmallAsteroidPool;
    public int amountBulletPool;
    public int amountBigAsteroidPool;
    private int amountMediumAsteroidPool;
    private int amountSmallAsteroidPool;

    private GameObject tmp;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        amountMediumAsteroidPool = amountBigAsteroidPool * 2;
        amountSmallAsteroidPool = amountBigAsteroidPool * 2 * 2;

        for (int i = 0; i < amountBulletPool; i++)
        {
            BulletAddedToPool();
        }

        for (int i = 0; i < amountSmallAsteroidPool; i++)
        {
            if (i < amountBigAsteroidPool)
            {
                BigAddedToPool();
            }
            if (i < amountMediumAsteroidPool)
            {
                MediumAddedToPool();
            }
            SmallAddedToPool();
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
        if (objectType == "Bullet")
        {
            BulletAddedToPool();
            amountBulletPool++;
            return pooledBulletObjects[amountBulletPool - 1];
        }
        else if (objectType == "BigAsteroid")
        {
            BigAddedToPool();
            amountBigAsteroidPool++;
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    MediumAddedToPool();
                    amountMediumAsteroidPool++;
                }
                SmallAddedToPool();
                amountSmallAsteroidPool++;
            }
            return pooledBigAsteroidObjects[amountBigAsteroidPool - 1];
        }
        return null;
    }

    private void BulletAddedToPool()
    {
        tmp = Instantiate(BulletPool);
        tmp.SetActive(false);
        pooledBulletObjects.Add(tmp);
    }
    private void BigAddedToPool()
    {
        tmp = Instantiate(BigAsteroidPool);
        tmp.SetActive(false);
        pooledBigAsteroidObjects.Add(tmp);
    }
    private void MediumAddedToPool()
    {
        tmp = Instantiate(MediumAsteroidPool);
        tmp.SetActive(false);
        pooledMediumAsteroidObjects.Add(tmp);
    }
    private void SmallAddedToPool()
    {
        tmp = Instantiate(SmallAsteroidPool);
        tmp.SetActive(false);
        pooledSmallAsteroidObjects.Add(tmp);
    }
}
