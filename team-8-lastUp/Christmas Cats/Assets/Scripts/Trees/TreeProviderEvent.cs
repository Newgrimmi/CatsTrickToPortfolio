using System;
using UnityEngine;

public class TreeProviderEvent : MonoBehaviour
{
    public event Action TreeDied;
    public event Action TreeTakeDamage;

    public void OnTreeDied()
    {
        TreeDied?.Invoke();
    }

    public void OnTreeTakeDamage()
    {
        TreeTakeDamage?.Invoke();
    }
}
