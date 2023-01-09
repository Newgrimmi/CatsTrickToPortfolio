using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVibroChangeSound : MonoBehaviour
{
    [SerializeField] private Toggle vibroToggle;
    [SerializeField] private AudioDataCollection currentAudio;

    private void Start()
    {
        vibroToggle.onValueChanged.AddListener(delegate
        {
            PlayAudio();
        });
    }

    private void PlayAudio()
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.ButtonPressed);
    }
}
