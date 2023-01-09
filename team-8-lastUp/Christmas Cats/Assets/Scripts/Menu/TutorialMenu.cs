using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private SaveGameData saveGameData;
    [SerializeField] private Tutorial tutorialScript;
    [SerializeField] private LevelChanger levelChangerScript;
    [SerializeField] private CatUpgrades catUpgrades;
    [SerializeField] private Sprite upgradeButtonPressed;
    [SerializeField] private Button upgradeActiveButton;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject goBossLvlButton;

    public bool TutorialMode { get; private set; }
    public bool GoToBossIconShowed { get; private set; }

    private void Start()
    {
        StartGame();
    }

    public void ChangeGoToBossIconShowed()
    {
        GoToBossIconShowed = !GoToBossIconShowed;
    }

    public void TutorialModeActive()
    {
        if (TutorialMode)
        {
            if (catUpgrades.TapSpawnedCatLvl == 1)
            {
                tutorialScript.HideFingerSprite(1);
                tutorialScript.ShowFingerSprite(2);
            }

            if (catUpgrades.TapSpawnedCatLvl > 1)
            {
                tutorialScript.HideFingerSprite(3);

                if (levelChangerScript.FirstBossLose == true)
                {
                    goBossLvlButton.SetActive(true);
                    tutorialScript.ShowFingerSprite(4);
                    GoToBossIconShowed = true;
                }

                TutorialMode = false;
            }
        }
    }

    private void StartGame()
    {
        if (saveGameData.HasLoaded)
        {
            TutorialMode = saveGameData.AutoSave.TutorialMode;

            if (!TutorialMode)
            {
                tutorialCanvas.SetActive(false);
                upgradeActiveButton.interactable = true;
                upgradeActiveButton.image.sprite = upgradeButtonPressed;
                Destroy(tutorialCanvas);
                Destroy(hand);
            }
        }
        else
        {
            GoToBossIconShowed = false;
            TutorialMode = true;
        }
    }
}
