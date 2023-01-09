using UnityEngine;
using TMPro;

public class BossTimer : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI bossTimerText;
    [SerializeField] private LevelChanger levelChanger;

    private BossHealth bossHp;

    public float Timer { get; private set; }

    private void Start()
    {
        SetTimer();
    }

    private void Update()
    {
        if (bossHp.BossIsAlive)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0)
        {
            bossTimerText.text = "0";
            levelChanger.BossWaveTimerEnd();
        }
        else
        {
            bossTimerText.text = Timer.ToString("0.0");
        }
    }

    public void FindBossHealthScript(GameObject boss)
    {
        bossHp = boss.GetComponent<BossHealth>();
    }

    public void SetTimer()
    {
        Timer = 30f;
    }
}
