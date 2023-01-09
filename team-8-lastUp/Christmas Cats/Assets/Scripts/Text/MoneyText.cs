using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private MoneyCounter curMoney;

    readonly NotationText notation = new NotationText();

    private void OnEnable()
    {
        curMoney.ChangeMoneyValue += ShowMoneyCount;
    }

    private void OnDisable()
    {
        curMoney.ChangeMoneyValue -= ShowMoneyCount;
    }

    private void ShowMoneyCount(int curMoneyValue)
    {
        moneyText.text = notation.NotationMethods(curMoneyValue, "");
    }
}
