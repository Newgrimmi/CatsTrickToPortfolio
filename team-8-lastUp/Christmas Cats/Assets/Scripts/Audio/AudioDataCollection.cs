using System;
using System.Collections.Generic;
using UnityEngine;


public class AudioDataCollection : MonoBehaviour
{

    [SerializeField] private AudioData audioData;

    private AudioSource[] soundsArray;

    private Dictionary<string, AudioClip> audioClipCollection = new Dictionary<string, AudioClip>();

    public enum AudioType { BossAttack, ButtonPressed, ChestDrop, ChestOpened, MainBackground,
        PurchaseNewCat, PurchaseUpgrade, SnowStorm, SpawnCat, TreeAttack, CatAttack, TreeDie, DuckCat, BossBeforeAttack, BossDie, RespBoss }


    private void Awake()
    {
        soundsArray = GetComponents<AudioSource>();

        for (int i = 0; i < soundsArray.Length; i++)
        {
            soundsArray[i].clip = audioData.AudioClips[i].AudioClip;
        }
    }

    private void Start()
    {
        for (int i = 0; i < audioData.AudioClips.Length; i++)
        {
            audioClipCollection.Add(audioData.AudioClips[i].name ,audioData.AudioClips[i].AudioClip);

        }
        soundsArray[0].Play();
    }

    public void PlayAudio(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.BossDie:
            case AudioType.RespBoss:
            case AudioType.BossAttack:
            case AudioType.ButtonPressed:
            case AudioType.ChestDrop:
            case AudioType.ChestOpened:
            case AudioType.PurchaseNewCat:
            case AudioType.PurchaseUpgrade:
            case AudioType.SnowStorm:
            case AudioType.SpawnCat:
            case AudioType.CatAttack:
            case AudioType.DuckCat:
            case AudioType.BossBeforeAttack:
            case AudioType.TreeDie:
            case AudioType.TreeAttack:
                var a = Array.Find(soundsArray, AudioData => AudioData.clip == audioClipCollection[audioType.ToString()]);
                a.PlayOneShot(a.clip);
                break;
            default:
                break;
        }
    }

    public void StopAudio(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.BossAttack:
            case AudioType.ButtonPressed:
            case AudioType.ChestDrop:
            case AudioType.ChestOpened:
            case AudioType.MainBackground:
            case AudioType.PurchaseNewCat:
            case AudioType.PurchaseUpgrade:
            case AudioType.SnowStorm:
            case AudioType.SpawnCat:
            case AudioType.CatAttack:
            case AudioType.DuckCat:
            case AudioType.BossBeforeAttack:
            case AudioType.TreeDie:
            case AudioType.TreeAttack:
                var a = Array.Find(soundsArray, AudioData => AudioData.clip == audioClipCollection[audioType.ToString()]);
                a.Stop();
                break;
            default:
                break;
        }
    }
}
