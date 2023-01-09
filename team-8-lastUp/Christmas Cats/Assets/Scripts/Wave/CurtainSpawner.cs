using System;
using UnityEngine;

public class CurtainSpawner : MonoBehaviour
{
    [SerializeField] private Curtain curtain;
    [SerializeField] private AudioDataCollection currentAudio;

    public void SpawnAfterWin(Action startCurtainCallback)
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.SnowStorm);
        curtain.gameObject.SetActive(true);
        curtain.InitializeAfterTarget(startCurtainCallback);
    }

    public void SpawnAfterLose(Action startCurtainCallback)
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.SnowStorm);
        curtain.gameObject.SetActive(true);
        curtain.InitializeAfterLose(startCurtainCallback);
    }
}
