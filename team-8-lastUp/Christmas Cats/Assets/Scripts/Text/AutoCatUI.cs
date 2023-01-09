using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoCatUI : MonoBehaviour
{
    [SerializeField] private Button butAutoSpawnedUpgrade;
    [SerializeField] private Sprite buttonOffActive;
    [SerializeField] private Sprite buttonOnActive;
    [SerializeField] private Sprite avtoSpawnedCatBack;
    [SerializeField] private Image avtoSpawnedCatBackImage;
    [SerializeField] private GameObject avtoSpawnedCatNameImage;
    [SerializeField] private GameObject avtoSpawnedCatDamageText;
    [SerializeField] private TextMeshProUGUI AutoSpawnedCatDamageText;
    [SerializeField] private TextMeshProUGUI butAutoSpawnedUpgradeText;
    [SerializeField] private TextMeshProUGUI priceAutoSpawnedUpgradeText;
    [SerializeField] private TextMeshProUGUI lvlAutoSpawnedUpgradeText;
    [SerializeField] private OnClick autoSpawn;
    [SerializeField] private SaveGameData saveGameData;
    [SerializeField] private MoneyCounter currentMoney;
    [SerializeField] private CatUpgrades catUpgrades;

    private int priceAutoSpawnedUpgrade;
    private int curLevelUpgradeCat;

    readonly int startLevelCat = 0;
    readonly NotationText notation = new NotationText();
    readonly Color butOffColor = new Color(0.7843137f, 0.02745098f, 0.2196079f, 1);
    readonly Color butOnColor = new Color(0.2627451f, 0.7490196f, 0, 1);

    private void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        catUpgrades.PurchaseAutoUp += ChangeMoneyValueForPurchase;
        catUpgrades.PurchaseAutoUp += CountPriceAutoSpawnedUpgrade;
        currentMoney.ChangeMoneyValue += CanBuyUpgrade;
    }

    private void OnDisable()
    {
        catUpgrades.PurchaseAutoUp -= ChangeMoneyValueForPurchase;
        catUpgrades.PurchaseAutoUp -= CountPriceAutoSpawnedUpgrade;
        currentMoney.ChangeMoneyValue -= CanBuyUpgrade;
    }

    private void CountPriceAutoSpawnedUpgrade(int curLvlUpgrade)
    {
        curLevelUpgradeCat = curLvlUpgrade;
        if (curLevelUpgradeCat <= 1)
        {
            priceAutoSpawnedUpgrade = 1000;
        }
        else
        {
            priceAutoSpawnedUpgrade = (int)(Mathf.Round(842.6765f * Mathf.Pow(1.1525f, curLvlUpgrade) / 10) * 10);
        }
        UpdateUI(curLvlUpgrade);
    }

    private void UpdateUI(int curLevelUpgrade)
    {
        
        if (curLevelUpgrade < 1)
        {
            butAutoSpawnedUpgradeText.text = "Купить";
            lvlAutoSpawnedUpgradeText.text = "";
        }
        else
        {
            StartCoroutine(autoSpawn.AutoSpawnCatForTime());
            OpenCat();
            butAutoSpawnedUpgradeText.text = "Улучшить";
            lvlAutoSpawnedUpgradeText.text = "Ур:" + curLevelUpgrade.ToString();
        }

        AutoSpawnedCatDamageText.text = "Атака: " + (10 + 5 * (curLevelUpgrade - 1)).ToString();
        priceAutoSpawnedUpgradeText.text = notation.NotationMethods(priceAutoSpawnedUpgrade, "");
    }

    private void CanBuyUpgrade(int curMoney)
    {
        if (curMoney < priceAutoSpawnedUpgrade)
        {
            butAutoSpawnedUpgradeText.color = butOffColor;
            butAutoSpawnedUpgrade.image.sprite = buttonOffActive;
            butAutoSpawnedUpgrade.enabled = false;
        }
        else
        {
            butAutoSpawnedUpgradeText.color = butOnColor;
            butAutoSpawnedUpgrade.image.sprite = buttonOnActive;
            butAutoSpawnedUpgrade.enabled = true;
        }
    }

    private void ChangeMoneyValueForPurchase(int curLevel)
    {
        currentMoney.PurchaseUpgrade(priceAutoSpawnedUpgrade);
    }

    private void StartGame()
    {
        if (saveGameData.HasLoaded)
        {
            if (saveGameData.AutoSave.AutoSpawnedCatLvl > 0)
            {
                CountPriceAutoSpawnedUpgrade(saveGameData.AutoSave.AutoSpawnedCatLvl);
                OpenCat();
                
            }
            else
            {
                CountPriceAutoSpawnedUpgrade(saveGameData.AutoSave.AutoSpawnedCatLvl);
            }
        }
        else
        {
            CountPriceAutoSpawnedUpgrade(startLevelCat);
            butAutoSpawnedUpgradeText.text = "Купить";
            lvlAutoSpawnedUpgradeText.text = "";
        }

    }
    private void OpenCat()
    {
        avtoSpawnedCatNameImage.SetActive(true);
        avtoSpawnedCatBackImage.sprite = avtoSpawnedCatBack;
        avtoSpawnedCatDamageText.SetActive(true);
        
    }
}
