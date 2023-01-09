using System.Collections.Generic;
using UnityEngine;

public class PoolCats 
{

    private List<Transform> _poolSimpleCat;

    public PoolCats (int count, CatsFactory<Transform> currentFactory)
    {
        CreatePool(count, currentFactory);
    }

    public void CreatePool(int count, CatsFactory<Transform> currentFactory)
    {
        _poolSimpleCat = new List<Transform>();

        for (int i = 0; i < count; i++)
        {
            CreateObject(currentFactory);
        }
    }

    private Transform CreateObject(CatsFactory<Transform> currentFactory, bool isActiveByDefault = false)
    {
        var createdObject = currentFactory.GetNewInstance();
        createdObject.gameObject.SetActive(isActiveByDefault);
        createdObject.position = new Vector3(Random.Range(-1.5f, 1.6f), -5f, 1f);
        createdObject.rotation = Quaternion.identity;
        _poolSimpleCat.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out Transform element)
    {
        foreach (var mono in _poolSimpleCat)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public Transform GetFreeElement(CatsFactory<Transform> currentFactory)
    {
        if(HasFreeElement(out var element))
        {
            element.position = new Vector3(Random.Range(-1.5f, 1.6f), -5f, 1f);
            element.rotation = Quaternion.identity;
            return element;
        }

        return CreateObject(currentFactory, true);
    }
}
