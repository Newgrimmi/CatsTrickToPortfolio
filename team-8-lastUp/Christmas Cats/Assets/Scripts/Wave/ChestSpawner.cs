using System;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private Chest chest;
    [SerializeField] private AudioDataCollection currentAudio;

    public void SpawnAfterTree(Action openChestCallback)
    {
        if(UnityEngine.Random.Range(0, 101) > 80)
        {
            currentAudio.PlayAudio(AudioDataCollection.AudioType.ChestDrop);
            chest.gameObject.SetActive(true);
            chest.ChangeIsClicked();
            chest.Initialize(openChestCallback);
        }

        else
        {
            chest.Initialize(openChestCallback);
            chest.DontOpened();
        }
    }

    public void SpawnAfterBoss(Action openChestCallback)
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.ChestDrop);
        chest.gameObject.SetActive(true);
        chest.ChangeIsClicked();
        chest.Initialize(openChestCallback);
    }
}
