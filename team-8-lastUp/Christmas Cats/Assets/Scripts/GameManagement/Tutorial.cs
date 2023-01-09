using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] fingerSprite;  
    [SerializeField] private Button upgradesMenuButton;
    [SerializeField] private Sprite upgradesUnlockedSprite;
    public bool readyForRedButton = false;
    private bool disableClaw = false;

    public void ShowFingerSprite(int index)
    {
        if (fingerSprite[index] != null)
        fingerSprite[index].SetActive(true);

    }

    public void HideFingerSprite(int index)
    {
        // fingerSprite[index].SetActive(false);

        if (fingerSprite[index] != null)
        Destroy(fingerSprite[index]);

        if (index == 3)
        {
            readyForRedButton = true;
        }
    }

    public void EnableUpgradesMenu()
    {
        upgradesMenuButton.interactable = true;
        upgradesMenuButton.image.sprite = upgradesUnlockedSprite;

        if (disableClaw == false)
        {
            ShowFingerSprite(1);
            disableClaw = true;
        }
    }

    

}
