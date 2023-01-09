using UnityEngine;

public class BossUpgrade : MonoBehaviour
{
    [SerializeField] private SaveGameData saveGameData;

    public int BossLevel { get; private set; }

    private void Start()
    {
        if (saveGameData.HasLoaded)
        {
            BossLevel = saveGameData.AutoSave.BossLevel;
        }
        else
        {
            BossLevel = 1;
        }
    }
    public float BossCalculate(float calculateHP)
    {
        calculateHP = Mathf.Round((60.7764f * Mathf.Pow(2.2901f, BossLevel) * 2));
        return calculateHP;
    }

    public void UpgradeBoss()
    {
        BossLevel++;
    }


}
