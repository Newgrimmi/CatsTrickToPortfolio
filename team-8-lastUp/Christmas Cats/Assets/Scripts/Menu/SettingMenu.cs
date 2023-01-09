using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject setMenu;
    [SerializeField] private GameObject closeSetMenuButton;
    [SerializeField] private Button settingActiveButton;
    [SerializeField] private Button closeSetMenuBut;
    [SerializeField] private Sprite settingButtonPressed;
    [SerializeField] private Sprite settingButtonIdle;
    [SerializeField] private AudioDataCollection currentAudio;

    private bool isSetMenuEnable;

    private void Start()
    {
        settingActiveButton.onClick.AddListener(SetActiveSettingMenu);
        closeSetMenuBut.onClick.AddListener(SetActiveSettingMenu);
    }

    public void SetActiveSettingMenu()
    {
        currentAudio.PlayAudio(AudioDataCollection.AudioType.ButtonPressed);

        if (isSetMenuEnable == false)
        {
            closeSetMenuButton.SetActive(true);
            settingActiveButton.image.sprite = settingButtonPressed;
            setMenu.SetActive(true);
            isSetMenuEnable = !isSetMenuEnable;
        }
        else
        {
            closeSetMenuButton.SetActive(false);
            settingActiveButton.image.sprite = settingButtonIdle;
            setMenu.SetActive(false);
            isSetMenuEnable = !isSetMenuEnable;
        }
    }
}
