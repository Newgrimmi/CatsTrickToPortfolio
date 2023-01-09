using UnityEngine;

public class MustAttack : MonoBehaviour
{
    private BossAttack bossAttack;

    private void Awake()
    {
        bossAttack = FindObjectOfType<BossAttack>();
    }

    public void Attack()
    {
        bossAttack.Attack();
    }
}
