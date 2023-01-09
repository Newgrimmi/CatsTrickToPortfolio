using UnityEngine;
using TMPro;

public class BossHealth : MonoBehaviour
{
    private TextMeshProUGUI healthBarText;
    private BossUpgrade bossUpgrade;
    private HealthBar healthBar;
    private AudioDataCollection audio;
    private BossProviderEvent bossProvider;

    private float bossHealth;
    private float maxHealth;

    private readonly NotationText notation = new NotationText();

    public bool BossIsAlive { get; private set; }

    private void Awake()
    {
        bossProvider = FindObjectOfType<BossProviderEvent>();
        bossUpgrade = FindObjectOfType<BossUpgrade>();
        healthBar = FindObjectOfType<HealthBar>();
        healthBarText = healthBar.GetComponentInChildren<TextMeshProUGUI>();
        audio = FindObjectOfType<AudioDataCollection>();
    }

    void Start()
    {
        audio.PlayAudio(AudioDataCollection.AudioType.RespBoss);
        BossIsAlive = true;
        bossHealth = bossUpgrade.BossCalculate(bossHealth);
        maxHealth = bossHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBarText.SetText($"{notation.NotationMethods(Mathf.Round(bossHealth),"")} / {notation.NotationMethods(Mathf.Round(maxHealth), "")}");
    }

    public void TakeDamage(float damage)
    {
        if (BossIsAlive)
        {
            bossHealth -= damage;

            if (bossHealth <= 0)
            {
                bossHealth = 0;
                Die();
            }
        }
       
        healthBar.SetCurrentHealth(bossHealth);
        healthBarText.SetText($"{notation.NotationMethods(Mathf.Round(bossHealth), "")} / {notation.NotationMethods(Mathf.Round(maxHealth), "")}");
    }

    public void Die()
    {
        audio.PlayAudio(AudioDataCollection.AudioType.BossDie);
        BossIsAlive = false;
        GetComponentInChildren<Animator>().SetBool("isDead", true);
        bossUpgrade.UpgradeBoss();
        Invoke("CallOnBossDied", 2.11f);
    }

    private void CallOnBossDied()
    {
        bossProvider.OnBossDied();
    }
}
