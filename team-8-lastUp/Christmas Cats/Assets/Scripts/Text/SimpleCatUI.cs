using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCatUI : MonoBehaviour
{
    [SerializeField] private Button butSimpleUpgrade;
    [SerializeField] private Sprite buttonOffActive;
    [SerializeField] private Sprite buttonOnActive;
    [SerializeField] private TextMeshProUGUI TapCatDamageText;
    [SerializeField] private TextMeshProUGUI butSimpleUpgradeText;
    [SerializeField] private TextMeshProUGUI priceSimpleUpgradeText;
    [SerializeField] private TextMeshProUGUI lvlSimpleUpgradeText;
    [SerializeField] private SaveGameData saveGameData;
    [SerializeField] private MoneyCounter currentMoney;
    [SerializeField] private CatUpgrades catUpgrades;

    private int priceSimpleUpgrade;
    private int curLevelUpgradeCat;

    readonly int startLevelCat = 1;
    readonly NotationText notation = new NotationText();
    readonly Color butOffColor = new Color(0.7843137f, 0.02745098f, 0.2196079f, 1);
    readonly Color butOnColor = new Color(0.2627451f, 0.7490196f, 0, 1);

    private void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        catUpgrades.PurchaseSimpleUp += ChangeMoneyValueForPurchase;
        catUpgrades.PurchaseSimpleUp += CountPriceSimpleUpgrade;
        currentMoney.ChangeMoneyValue += CanBuyUp;
    }

    private void OnDisable()
    {
        catUpgrades.PurchaseSimpleUp -= ChangeMoneyValueForPurchase;
        catUpgrades.PurchaseSimpleUp -= CountPriceSimpleUpgrade;
        currentMoney.ChangeMoneyValue -= CanBuyUp;
    }

    private void CountPriceSimpleUpgrade(int curLvlUpgrade)
    {
        curLevelUpgradeCat = curLvlUpgrade;
        if (curLevelUpgradeCat <= 1)
        {
            priceSimpleUpgrade = 100;
        }
        else
        {
            priceSimpleUpgrade = (int)(Mathf.Round((82.9553f * Mathf.Pow(1.1535f, curLvlUpgrade)) / 10) * 10);
        }
        UpdateUI(curLvlUpgrade);
    }

    private void UpdateUI(int curLevelUpgrade)
    {
        TapCatDamageText.text = "јтака: " + curLevelUpgrade.ToString();
        lvlSimpleUpgradeText.text = "”р:" + curLevelUpgrade.ToString();
        priceSimpleUpgradeText.text = notation.NotationMethods(priceSimpleUpgrade, "");
    }

    private void CanBuyUp(int curMoney)
    {
        if (curMoney < priceSimpleUpgrade)
        {
            butSimpleUpgradeText.color = butOffColor;
            butSimpleUpgrade.image.sprite = buttonOffActive;
            butSimpleUpgrade.enabled = false;
        }
        else
        {
            butSimpleUpgradeText.color = butOnColor;
            butSimpleUpgrade.image.sprite = buttonOnActive;
            butSimpleUpgrade.enabled = true;
        }
    }

    private void ChangeMoneyValueForPurchase(int curLevel)
    {
        if (curLevelUpgradeCat <= 1)
        {
            priceSimpleUpgrade = 100;
        }
        else
        {
            priceSimpleUpgrade = (int)(Mathf.Round((82.9553f * Mathf.Pow(1.1535f, curLevelUpgradeCat)) / 10) * 10);
        }
        currentMoney.PurchaseUpgrade(priceSimpleUpgrade);
    }

    private void StartGame()
    {
        if (saveGameData.HasLoaded)
        {
            CountPriceSimpleUpgrade(saveGameData.AutoSave.TapSpawnedCatLvl);
            CanBuyUp(saveGameData.AutoSave.Money);
        }
        else
        {
            CountPriceSimpleUpgrade(startLevelCat);
            TapCatDamageText.text = "јтака: 1";
            lvlSimpleUpgradeText.text = "”р: 1";
        }
    }
}