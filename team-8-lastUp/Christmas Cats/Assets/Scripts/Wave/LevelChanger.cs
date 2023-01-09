using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefs;
    [SerializeField] private GameObject backTimer;
    [SerializeField] private Button goBossLevelButton;
    [SerializeField] private Button surrenderButton;

    [SerializeField] private ChestSpawner chestSpawner;
    [SerializeField] private CurtainSpawner curtainSpawner;
    [SerializeField] private DuckCatSpawner duckCatSpawner;

    [SerializeField] private OnClick onClick;
    [SerializeField] private Tutorial tutorialScript;
    [SerializeField] private AudioDataCollection currentAudio;
    [SerializeField] private BossProviderEvent bossProvider;

    [SerializeField] private TutorialMenu tutorialMenu;
    [SerializeField] private BossTimer bossTimerScript;
    [SerializeField] private SaveGameData saveGameData;

    private GameObject curEnemy;
    private bool upgradesMenuEnabled;
    private bool isBossLose;
    
    public enum CurtainType { Win, Lose, TryBoss }

    public event Action<int, int> ChangeLevelValue;

    readonly Vector3 treeSpawn = new Vector3(0,-8.5f, 9f);
    readonly Vector3 bossSpawn = new Vector3(0, -5f, 9.3f);
    
    public bool FirstBossLose { get; private set; }
    public int BossWave { get; } = 5;
    public bool FirstBossFight { get; private set; } = true;
    public int OldLevel { get; private set; }
    public int Level { get; private set; } = 1;
    public int BossCount { get; private set; } = 0;
    public bool TutorialEnd { get; private set; } = false;

    private void Start()
    {
        if (saveGameData.HasLoaded)
        {
            TutorialEnd = saveGameData.AutoSave.TutorialEnd;
            FirstBossFight = saveGameData.AutoSave.FirstBossFight;
            BossCount = saveGameData.AutoSave.BossCount;
            Level = saveGameData.AutoSave.Level;
        }

        goBossLevelButton.onClick.AddListener(TryBossLevel);
        surrenderButton.onClick.AddListener(ClickBossWaveEnd);
        ChangeLevelValue?.Invoke(OldLevel, Level);
        LevelDown();
        CallNewWave();
    }

    private void OnEnable()
    {
        bossProvider.BossDied += OnBossDied;
    }

    private void OnDisable()
    {
        bossProvider.BossDied -= OnBossDied;
    }

    public void TryBossLevel()
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.ButtonPressed);
        isBossLose = false;
        Destroy(curEnemy);
        ShowCurtain(CurtainType.TryBoss);
        tutorialScript.HideFingerSprite(4);
        TutorialEnd = true;
    }

    public void BossWaveTimerEnd()
    {  
        backTimer.SetActive(false);         
        Destroy(curEnemy);
        LevelDown();
        ShowCurtain(CurtainType.Lose);
        isBossLose = true;
        FirstBossLose = true;

        if (tutorialMenu.GoToBossIconShowed == false && tutorialScript.readyForRedButton == true)
        {
            tutorialScript.ShowFingerSprite(4);
            tutorialMenu.ChangeGoToBossIconShowed();
            tutorialScript.readyForRedButton = false;
        }

        if (!upgradesMenuEnabled)
        {
            tutorialScript.EnableUpgradesMenu();    
            upgradesMenuEnabled = true;
        }
    }

    public void ClickBossWaveEnd()
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.ButtonPressed);
        BossWaveTimerEnd();
    }

    public void OnBossDied()
    {
        BossCount++;
        backTimer.SetActive(false);
        Destroy(curEnemy);
        ShowCurtain(CurtainType.Win);

        if (!upgradesMenuEnabled)
        {
            tutorialScript.EnableUpgradesMenu(); 
            upgradesMenuEnabled = true;
        }
    }

    private void ShowChest()
    {
        if (Level % BossWave == 0)
        {
            chestSpawner.SpawnAfterBoss(AddDuckCat);
        }
        else
        {
            chestSpawner.SpawnAfterTree(AddDuckCat);
        }
    }

    public void ShowCurtain(CurtainType type)
    {
        onClick.KillAllCats();
        onClick.CantSpawnCats();

        switch (type)
        {
            case CurtainType.Win:
                curtainSpawner.SpawnAfterWin(ShowChest);
                break;
            case CurtainType.Lose:
                curtainSpawner.SpawnAfterLose(StartNewWave);
                break;
            case CurtainType.TryBoss:
                curtainSpawner.SpawnAfterWin(StartNewWave);
                break;
        }
    }

    private void AddDuckCat()
    {
        duckCatSpawner.Spawn(StartNewWave);
    }

    private void StartNewWave()
    {
        onClick.CanSpawnCats();

        if (isBossLose == false)
        {
            CallNewWave();
        }
        else
        {
            CallRepeatWave();
        }   
    }

    private void CallNewWave()
    {
        LevelUp();

        if (Level % BossWave == 0)
        {
            curEnemy = Instantiate(enemyPrefs[0], bossSpawn, Quaternion.Euler(0, 180f, 0));
            backTimer.SetActive(true);
            bossTimerScript.SetTimer();           
            bossTimerScript.FindBossHealthScript(curEnemy);
            FirstBossFight = false;
        }
        else
        {
            curEnemy = Instantiate(enemyPrefs[UnityEngine.Random.Range(1, enemyPrefs.Length)], treeSpawn, Quaternion.identity);
        }
    }

    private void CallRepeatWave()
    {
        curEnemy = Instantiate(enemyPrefs[UnityEngine.Random.Range(1, enemyPrefs.Length)], treeSpawn, Quaternion.identity);
    }

    private void LevelUp()
    {
        OldLevel = Level;
        Level++;
        ChangeLevelValue?.Invoke(OldLevel, Level);
    }

    private void LevelDown()
    {
        OldLevel = Level;
        Level--;
        ChangeLevelValue?.Invoke(OldLevel, Level);
    }
}
