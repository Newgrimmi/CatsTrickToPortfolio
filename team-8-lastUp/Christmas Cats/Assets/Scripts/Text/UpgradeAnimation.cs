using UnityEngine;
using DG.Tweening;
using TMPro;

public class UpgradeAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bigPlusSimple;
    [SerializeField] private TextMeshProUGUI middlePlusSimple;
    [SerializeField] private TextMeshProUGUI smallPlusSimple;

    [SerializeField] private TextMeshProUGUI bigPlusAuto;
    [SerializeField] private TextMeshProUGUI middlePlusAuto;
    [SerializeField] private TextMeshProUGUI smallPlusAuto;

    [SerializeField] private CatUpgrades catUpgrades;

    private void OnEnable()
    {
        catUpgrades.PurchaseSimpleUp += AnimSimpleCat;
        catUpgrades.PurchaseAutoUp += AnimAutoCat;
    }
    private void OnDisable()
    {
        catUpgrades.PurchaseSimpleUp -= AnimSimpleCat;
        catUpgrades.PurchaseAutoUp -= AnimAutoCat;
    }

    private void AnimSimpleCat(int curLevel)
    {
        DOTween.Sequence()
            .Append(bigPlusSimple.DOFade(1, 0.2f))
            .AppendInterval(0.05f)
            .Append(middlePlusSimple.DOFade(1, 0.2f))
            .AppendInterval(0.05f)
            .Append(smallPlusSimple.DOFade(1, 0.2f))
            .Append(bigPlusSimple.DOFade(0, 0.2f))
            .AppendInterval(0.05f)
            .Append(middlePlusSimple.DOFade(0, 0.2f))
            .AppendInterval(0.05f)
            .Append(smallPlusSimple.DOFade(0, 0.2f));
    }

    private void AnimAutoCat(int curLevel)
    {
        DOTween.Sequence()
            .Append(bigPlusAuto.DOFade(1, 0.2f))
            .AppendInterval(0.05f)
            .Append(middlePlusAuto.DOFade(1, 0.2f))
            .AppendInterval(0.05f)
            .Append(smallPlusAuto.DOFade(1, 0.2f))
            .Append(bigPlusAuto.DOFade(0, 0.2f))
            .AppendInterval(0.05f)
            .Append(middlePlusAuto.DOFade(0, 0.2f))
            .AppendInterval(0.05f)
            .Append(smallPlusAuto.DOFade(0, 0.2f));
    }
}
