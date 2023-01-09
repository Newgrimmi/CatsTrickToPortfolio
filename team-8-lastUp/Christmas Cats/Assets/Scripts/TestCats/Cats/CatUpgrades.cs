using System;
using UnityEngine;
using UnityEngine.UI;

public class CatUpgrades : MonoBehaviour
{
    [SerializeField] private SaveGameData saveGameData;
    [SerializeField] private AudioDataCollection currentAudio;
    [SerializeField] private Tutorial tutorScript;
    [SerializeField] private Button upgradeSimpleCat;
    [SerializeField] private Button upgradeAutoCat;

    private bool clawDisable = false;

    public event Action<int> PurchaseSimpleUp;
    public event Action<int> PurchaseAutoUp;

    public int TapSpawnedCatLvl { get; private set; } = 1;
    public int AutoSpawnedCatLvl { get; private set; } = 0;
    public bool Tutorial { get; private set; } = true;
    public enum CatType { Simple, AutoSpawned }

    private void Start()
    {
        StartGame();
    }

    private void OnDestroy()
    {
        upgradeAutoCat.onClick.RemoveListener(LevelUpAutoSpawned);
        upgradeSimpleCat.onClick.RemoveListener(LevelUpTapSpawned);
    }

    public float[] CatCalculator(CatType catType)
    {
        if (catType == CatType.Simple)
        {
            float damage = TapSpawnedCatLvl;
            float speed = 2f;

            float[] parametres = { damage, speed };

            return parametres;
        }
        else  // CatType.AutoSpawned
        {
            float damage = 10 + 5 * (AutoSpawnedCatLvl - 1);
            float speed = 2.1f;

            float[] parametres = { damage, speed, };

            return parametres;
        }
    }

    public void LevelUpTapSpawned()
    {
        TapSpawnedCatLvl++;
        PurchaseSimpleUp?.Invoke(TapSpawnedCatLvl);
        currentAudio.PlayAudio(AudioDataCollection.AudioType.PurchaseUpgrade);

        if (Tutorial)
        {
            tutorScript.HideFingerSprite(2);

            if (clawDisable == false)
            {
                tutorScript.ShowFingerSprite(3);
                clawDisable = true;
            }

            Tutorial = false;
        }
    }

    public void LevelUpAutoSpawned()
    {
        AutoSpawnedCatLvl++;
        PurchaseAutoUp?.Invoke(AutoSpawnedCatLvl);
        currentAudio.PlayAudio(AudioDataCollection.AudioType.PurchaseUpgrade);
    }

    private void StartGame()
    {
        upgradeAutoCat.onClick.AddListener(LevelUpAutoSpawned);
        upgradeSimpleCat.onClick.AddListener(LevelUpTapSpawned);

        if (saveGameData.HasLoaded)
        {
            TapSpawnedCatLvl = saveGameData.AutoSave.TapSpawnedCatLvl;
            AutoSpawnedCatLvl = saveGameData.AutoSave.AutoSpawnedCatLvl;
            Tutorial = saveGameData.AutoSave.Tutorial;
        }
    }
}
