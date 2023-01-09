using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chest : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MoneyCounter curMoney;
    [SerializeField] private LevelChanger curLevel;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private AudioDataCollection currentAudio;
    [SerializeField] private Toggle vibroToggle;
    [SerializeField] private GameObject openedChestEffect;

    readonly int upgradeLevel = 10;
    readonly int upgradeValue = 100;
    readonly int minValueAfterTree = 150;
    readonly int maxValueAfterTree = 250;
    readonly int minValueAfterBoss = 200;
    readonly int maxValueAfterBoss = 300;

    private Vibration vibro = new Vibration();
    private Animator anim;
    private Action savedCallBackChest;

    private bool isClicked;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeEnableButtonFalse()
    {
        levelUI.ButtonEnableFalse();
    }

    public void IdleState()
    {
        anim.SetBool("IsIdle", true);
    }

    public void Initialize(Action callback)
    {
        savedCallBackChest = callback;
    }

    public void OnOpened()
    {
        int CountMinValueForTree = minValueAfterTree + (curLevel.Level / upgradeLevel) * upgradeValue;
        int CountMaxValueForTree = maxValueAfterTree + (curLevel.Level / upgradeLevel) * upgradeValue;
        int CountMinValueForBoss = minValueAfterBoss + (curLevel.Level / upgradeLevel) * upgradeValue;
        int CountMaxValueForBoss = maxValueAfterBoss + (curLevel.Level / upgradeLevel) * upgradeValue;

        if (curLevel.Level % 5==0)
        {
            curMoney.AddMoney(UnityEngine.Random.Range(CountMinValueForBoss, CountMaxValueForBoss));
        }
        else
        {
            curMoney.AddMoney(UnityEngine.Random.Range(CountMinValueForTree, CountMaxValueForTree));
        }

        ChangeEnableButtonTrue();
        DisableOpenEffect();
        savedCallBackChest?.Invoke();
        gameObject.SetActive(false);
    }
    
    public void DontOpened()
    {
        ChangeEnableButtonTrue();
        savedCallBackChest?.Invoke();
        gameObject.SetActive(false);
    }

    public void ChangeIsClicked()
    {
        isClicked = !isClicked;
    }

    public void PlayOpenEffect()
    {
        openedChestEffect.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isClicked)
        {
            vibro.IsVibration(vibroToggle.isOn);
            currentAudio.PlayAudio(AudioDataCollection.AudioType.ChestOpened);
            anim.SetBool("IsOpen", true);
            ChangeIsClicked();
        }
    }

    private void DisableOpenEffect()
    {
        openedChestEffect.SetActive(false);
    }

    private void ChangeEnableButtonTrue()
    {
        levelUI.ButtonEnableTrue();
    }
}
