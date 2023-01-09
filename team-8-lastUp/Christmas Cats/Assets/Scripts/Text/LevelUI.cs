using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject goBossLevelButton;
    [SerializeField] private GameObject surrenderButton;
    [SerializeField] private TextMeshProUGUI curLevelText;
    [SerializeField] private Button goBossLevelBut;
    [SerializeField] private Button surrenderBut;
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private SaveGameData saveGameData;

    private void Start()
    {
        
        if (saveGameData.HasLoaded)
        {
            UpgradeUI(saveGameData.AutoSave.Level -1 , saveGameData.AutoSave.Level);
        }
        else
        {
            UpgradeUI(levelChanger.Level -1 , levelChanger.Level);
        }
    }

    private void OnEnable()
    {
        levelChanger.ChangeLevelValue += UpgradeUI;
    }

    private void OnDisable()
    {
        levelChanger.ChangeLevelValue -= UpgradeUI;
    }


    private void UpgradeUI(int oldLevel, int nextLevel)
    {
        curLevelText.text = "Óð." + nextLevel.ToString();

        if(nextLevel> oldLevel)
        {
            if(nextLevel % 5 == 0 && !levelChanger.FirstBossFight)
            {
                surrenderButton.SetActive(true);
                goBossLevelButton.SetActive(false);
            }
            else
            {
                goBossLevelButton.SetActive(false);
                surrenderButton.SetActive(false);
            }
        }
        else
        {
            surrenderButton.SetActive(false);
            goBossLevelButton.SetActive(true);
        }
    }

    public void ButtonEnableTrue()
    {
        goBossLevelBut.enabled = true;
        surrenderBut.enabled = true;
    }
    public void ButtonEnableFalse()
    {
        goBossLevelBut.enabled = false;
        surrenderBut.enabled = false;
    }
}
