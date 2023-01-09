using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] private Transform cannonPlace;
    [SerializeField] private Vector3 snowballFallPlace = new Vector3(0, 2f, -1f);
    [SerializeField] private ParticleSystem shotEffect;

    private AudioDataCollection currentAudio;
    private int countForAttackSound = 1;
    private readonly float snowballSpeed = 3;

    private void Awake()
    {
        currentAudio = FindObjectOfType<AudioDataCollection>();
    }

    public void Attack()
    {
        countForAttackSound++;

        if (countForAttackSound % 2 == 0)
        {
            currentAudio.PlayAudio(AudioDataCollection.AudioType.BossBeforeAttack);
        }
        currentAudio.PlayAudio(AudioDataCollection.AudioType.BossAttack);

        shotEffect.Play();
        snowballFallPlace.x = Random.Range(-0.3f, 0.4f);
        snowballFallPlace.z = Random.Range(-1.5f, -0.5f);
        GameObject snowball = Instantiate(snowballPrefab, cannonPlace.position, Quaternion.identity);
        snowball.GetComponent<Rigidbody>().AddForce(snowballFallPlace * snowballSpeed, ForceMode.Impulse);
    }

}
