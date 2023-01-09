using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] private SaveGameData saveKeeping;
    [SerializeField] private MoneyCounter moneyCounter;
    [SerializeField] private TutorialMenu tutorialMenu;
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private CatUpgrades catUpgrades;
    [SerializeField] private BossUpgrade bossUpgrade;

    public void Save()
    {
        saveKeeping.AutoSave.Level = levelChanger.Level;
        saveKeeping.AutoSave.BossCount = levelChanger.BossCount;
        saveKeeping.AutoSave.FirstBossFight = levelChanger.FirstBossFight;
        saveKeeping.AutoSave.TutorialMode = tutorialMenu.TutorialMode;
        saveKeeping.AutoSave.Tutorial = catUpgrades.Tutorial;
        saveKeeping.AutoSave.TapSpawnedCatLvl = catUpgrades.TapSpawnedCatLvl;
        saveKeeping.AutoSave.AutoSpawnedCatLvl = catUpgrades.AutoSpawnedCatLvl;
        saveKeeping.AutoSave.Money = moneyCounter.Money;
        saveKeeping.AutoSave.BossLevel = bossUpgrade.BossLevel;
        saveKeeping.AutoSave.TutorialEnd = levelChanger.TutorialEnd;
    }
}
