using UnityEngine;
using TMPro;

public class ChristmasTree : MonoBehaviour
{
    public float TreeHp { get; private set; }

    public TreeProviderEvent treeProviderEvent { get; private set; }
    private AudioDataCollection currentAudio;
    private LevelChanger curLevel;
    private Animator anim;
    private HealthBar healthBar;
    private TextMeshProUGUI healthBarText;

    private bool isDead;
    private int countForSound;
    private float maxHp;

    private void Awake()
    {
        treeProviderEvent = FindObjectOfType<TreeProviderEvent>();
        currentAudio = FindObjectOfType<AudioDataCollection>();
        curLevel = FindObjectOfType<LevelChanger>();
        healthBar = FindObjectOfType<HealthBar>();
        healthBarText = healthBar.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        countForSound = 1;
        CountHp();
        anim = GetComponent<Animator>();
        maxHp = TreeHp;
        healthBar.SetMaxHealth(maxHp);
        healthBarText.SetText($"{Mathf.Round(TreeHp)} / {Mathf.Round(maxHp)}");
    }

    private void OnEnable()
    {
        treeProviderEvent.TreeTakeDamage += AnimHit;
      //  EventController.TreeTakeDamage += AnimHit;
    }

    private void OnDisable()
    {
        treeProviderEvent.TreeTakeDamage -= AnimHit;
       // EventController.TreeTakeDamage -= AnimHit;
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            TreeHp -= damage;
            if (TreeHp <= 0)
            {
                TreeHp = 0;
                isDead = !isDead;
                currentAudio.PlayAudio(AudioDataCollection.AudioType.TreeDie);
                anim.SetBool("IsDead", true);
            }
        }
        
        healthBar.SetCurrentHealth(TreeHp);
        healthBarText.SetText($"{Mathf.Round(TreeHp)} / {Mathf.Round(maxHp)}");
    }

    public void TreeDestroyed()
    {
        treeProviderEvent.OnTreeDied();
       // EventController.OnTreeDied();
        Destroy(gameObject); 
    }

    public void ShowCurtain()
    {
        curLevel.ShowCurtain(LevelChanger.CurtainType.Win);
    }

    public void IdleState()
    {
        anim.SetBool("IsIdle", true);
    }

    private void CountHp()
    {
        if(curLevel.Level == 1)
        {
            TreeHp = 20f;
        }
        else
        {
            TreeHp = Mathf.RoundToInt(((9.4837f * Mathf.Pow(1.204f, curLevel.Level))*2));
        }
    }

    private void AnimHit()
    {
        countForSound++;
        if(countForSound%2==0)
            currentAudio.PlayAudio(AudioDataCollection.AudioType.TreeAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (anim.GetBool("IsIdle") == true)
        {
            anim.SetTrigger("IsDamagee");
        }
    }
}
