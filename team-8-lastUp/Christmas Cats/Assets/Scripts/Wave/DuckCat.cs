using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DuckCat : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Toggle vibroToggle;
    [SerializeField] private AudioDataCollection currentAudio;
    [SerializeField] private DuckCatAnim duckCatAnim;

    private Animator anim;
    private Action savedCallBackDuckCat;
    private RectTransform rectTransform; 

    private bool isClicked;

    private readonly Vibration vibro = new Vibration();
    private readonly Vector3 rightPositionScale = new Vector3(-1, 1, 1);
    private readonly Vector3 leftPositionScale = new Vector3(1, 1, 1);

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (rectTransform.anchoredPosition.x> 0)
        {
            transform.localScale = rightPositionScale;
        }
        else
        {
            transform.localScale = leftPositionScale;
        }
    }

    public void Initialize(Action callback)
    {
        savedCallBackDuckCat = callback;
    }

    public void OnOpened()
    {
        savedCallBackDuckCat?.Invoke();
        StartCoroutine(AutoDuckDie());
    }

    public void DontOpened()
    {
        savedCallBackDuckCat?.Invoke();
        gameObject.SetActive(false);
    }

    public void ChangeIsClicked()
    {
        isClicked = !isClicked;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isClicked)
        {
            currentAudio.PlayAudio(AudioDataCollection.AudioType.DuckCat);
            vibro.IsVibration(vibroToggle.isOn);
            anim.SetBool("IsOff", true);
            duckCatAnim.ClickedAnim();
            ChangeIsClicked();
            StopCoroutine(AutoDuckDie());
            StartCoroutine(AfterClickDuckDie());
        }
    }

    private IEnumerator AfterClickDuckDie()
    {
        while (gameObject.activeInHierarchy==true)
        {
            yield return new WaitForSeconds(2f);
            currentAudio.StopAudio(AudioDataCollection.AudioType.DuckCat);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator AutoDuckDie()
    {
        while (isClicked)
        {
            yield return new WaitForSeconds(6);
            currentAudio.StopAudio(AudioDataCollection.AudioType.DuckCat);
            gameObject.SetActive(false);
            ChangeIsClicked();
        }
    }
}
