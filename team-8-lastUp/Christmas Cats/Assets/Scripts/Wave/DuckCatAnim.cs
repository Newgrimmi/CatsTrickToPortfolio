using DG.Tweening;
using UnityEngine;


public class DuckCatAnim : MonoBehaviour
{
    [SerializeField] private GameObject toy;
    [SerializeField] private Transform posToMoveToToys;
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private MoneyCounter curMoney;

    private int valueAfterClick;
    private Vector3 target;

    private readonly Vector3 posUp = new Vector3(0, 80, 0);

    private void OnEnable()
    {
        target = posToMoveToToys.position;
    }

    public void ClickedAnim()
    {
        toy.transform.position = gameObject.transform.position;
        toy.transform.localScale = new Vector3(1, 1, 1);
        toy.SetActive(true);

        DOTween.Sequence()
            .Append(toy.transform.DOMove(gameObject.transform.position + posUp, 0.5f))
            .AppendInterval(0.6f)
            .AppendCallback(ContinueAnim)
            .SetEase(Ease.InOutQuad)
            .AppendInterval(0.9f)
            .AppendCallback(AddMoney);
    }

    private void ContinueAnim()
    {
        toy.transform.DOMove(target, 0.9f);
        toy.transform.DOScale(0.5f, 0.9f);
    }

    private void AddMoney()
    {
        valueAfterClick = 500 + ((levelChanger.Level / levelChanger.BossWave) * 100);
        curMoney.AddMoney(valueAfterClick);
        toy.SetActive(false);
    }
}
