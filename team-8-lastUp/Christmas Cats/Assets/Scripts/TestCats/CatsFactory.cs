using UnityEngine;

public class CatsFactory<T> : MonoBehaviour where T : Transform
{

    [SerializeField] private T _prefab;
    [SerializeField] private Transform parentTransform;
    public T GetNewInstance()
    {
        return Instantiate(_prefab, parentTransform);
    }
}
