using System;
using UnityEngine;

public class BossProviderEvent : MonoBehaviour
{
    public  event Action BossDied;

    public void OnBossDied()
    {
        BossDied?.Invoke();
    }
}
