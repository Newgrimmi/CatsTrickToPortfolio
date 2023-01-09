using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject scrollUpgradeMenu;
    [SerializeField] private GameObject upgradeList;
    [SerializeField] private GameObject closeUpgradeMenuButton;
    [SerializeField] private Button upgradeActiveButton;
    [SerializeField] private Button closeUpgradeMenuBut;
    [SerializeField] private Sprite upgradeButtonPressed;
    [SerializeField] private Sprite upgradeButtonIdle;
    [SerializeField] private Canvas gameMenuCanvas;
    [SerializeField] private AudioDataCollection currentAudio;
    [SerializeField] private TutorialMenu tutorialMenu;

    private bool isUpgradeMenuEnable;
    private Vector3 posDefault;

    readonly Vector3 posUp = new Vector3(0, 895f, 0);

    private void Start()
    {
        upgradeActiveButton.onClick.AddListener(SetActiveUpgradeMenu);
        closeUpgradeMenuBut.onClick.AddListener(SetActiveUpgradeMenu);
        posDefault = upgradeMenu.transform.position;
    }

    public void SetActiveUpgradeMenu()
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.ButtonPressed);
        tutorialMenu.TutorialModeActive();

        if (isUpgradeMenuEnable == false)
        {
            closeUpgradeMenuButton.SetActive(true);
            upgradeActiveButton.image.sprite = upgradeButtonIdle;
            upgradeMenu.transform.position += posUp * gameMenuCanvas.scaleFactor;
            isUpgradeMenuEnable = !isUpgradeMenuEnable;
        }
        else
        {
            closeUpgradeMenuButton.SetActive(false);
            upgradeActiveButton.image.sprite = upgradeButtonPressed;
            upgradeMenu.transform.position = posDefault;
            isUpgradeMenuEnable = !isUpgradeMenuEnable;
        }
    }
}
