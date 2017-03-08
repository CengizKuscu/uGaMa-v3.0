using System.Collections.Generic;
using UnityEngine;

namespace uGaMa.Extensions
{
    public class Pooler :MonoBehaviour, IPooler
    {
        int pooledAmount = 15;
        List<GameObject> pooledObjects;
        GameObject pooledObject;
        bool willGrow = false;
        Transform targetParent;

        public int PooledAmount
        {
            get { return pooledAmount; }
            set { pooledAmount = value; }
        }

        public GameObject PooledObject
        {
            get { return pooledObject; }
            set { pooledObject = value; }
        }

        public List<GameObject> PooledObjects
        {
            get { return pooledObjects; }
        }

        public bool WillGrow
        {
            get { return willGrow; }
            set { willGrow = value; }
        }

        public Transform TargetParent
        {
            get { return targetParent; }
            set { targetParent = value; }
        }

        public void Init()
        {
            if (pooledAmount > 0)
            {
                pooledObjects = new List<GameObject>();
                for (int i = 0; i < pooledAmount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(pooledObject);
                    obj.SetActive(false);
                    if (targetParent)
                    {
                        obj.transform.SetParent(targetParent);
                    }
                    pooledObjects.Add(obj);
                }
            }
        }


        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }

            if (willGrow)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                pooledObjects.Add(obj);
                if (targetParent)
                {
                    obj.transform.SetParent(targetParent);
                }

                return obj;
            }

            return null;
        }

        public void Reset()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                Destroy(pooledObjects[i]);
            }

            pooledObjects.Clear();
        }

        public void OnDestroy()
        {
            Reset();
        }
    }
}