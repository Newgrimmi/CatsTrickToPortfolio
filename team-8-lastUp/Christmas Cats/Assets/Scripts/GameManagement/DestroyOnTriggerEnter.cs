using System;
using UnityEngine;

public class DestroyOnTriggerEnter : MonoBehaviour
{
    public event Action<Transform> CatDied;

    private void OnCollisionEnter(Collision collision)
    {
        CatDied?.Invoke(collision.transform);
        collision.gameObject.SetActive(false);
    }
}
