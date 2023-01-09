using UnityEngine;
using System;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private LevelChanger level;
    [SerializeField] private SaveGameData saveGameData;
    [SerializeField] private TreeProviderEvent treeProviderEvent;
    [SerializeField] private BossProviderEvent bossProvider;

    readonly int bossWave = 5;

    private int bossMoney;
    private int treeMoney;

    public event Action<int> ChangeMoneyValue;

    public int Money { get; private set; }

    private void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        bossProvider.BossDied += OnTargetDied;
        treeProviderEvent.TreeDied += OnTargetDied;
        level.ChangeLevelValue += CollectMoneyTree;
    }

    private void OnDisable()
    {
        bossProvider.BossDied -= OnTargetDied;
        treeProviderEvent.TreeDied -= OnTargetDied;
        level.ChangeLevelValue -= CollectMoneyTree;
    }
    
    public void PurchaseUpgrade(int moneyCost)
    {
        Money -= moneyCost;
        ChangeMoneyValue?.Invoke(Money);
    }

    public void AddMoney(int value)
    {
        Money += value;
        ChangeMoneyValue?.Invoke(Money);
    }

    private void OnTargetDied()
    {
        if (level.Level % bossWave == 0)
        {
            AddMoney(CollectMoneyBoss(level.BossCount));
        }
        else
        {
            AddMoney(treeMoney);
        }
    }

    private int CollectMoneyBoss(int curBossCount)
    {
        bossMoney = 150 + 50 * curBossCount;
        return bossMoney;
    }

    private void CollectMoneyTree(int oldLevel,int curLevel)
    {
        treeMoney = curLevel * 10;
    }

    private void StartGame()
    {
        if (saveGameData.HasLoaded)
        {
            Money = saveGameData.AutoSave.Money;
        }
        else
        {
            Money = 0;
        }
        ChangeMoneyValue?.Invoke(Money);
    }
 
}
