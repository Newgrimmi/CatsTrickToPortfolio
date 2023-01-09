using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHitAudio : MonoBehaviour
{
    private AudioDataCollection currentAudio;
    private int soundController;

    private void Awake()
    {
        soundController = 0;
        currentAudio = FindObjectOfType<AudioDataCollection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TapSpawnedCat")|| other.gameObject.CompareTag("AutoSpawnedCat"))
        {
            if(soundController == 0)
            {
                currentAudio.PlayAudio(AudioDataCollection.AudioType.CatAttack);
                soundController++;
            }
        }
    }
}
