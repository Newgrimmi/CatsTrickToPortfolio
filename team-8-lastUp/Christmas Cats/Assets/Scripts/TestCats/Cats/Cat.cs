using UnityEngine;

public class Cat : MonoBehaviour
{
    protected float damage;
    protected float speed; 
    private float speedBeforeFreeze;
    private bool canAttack = true;

    private SkinnedMeshRenderer meshRenderer;

    [SerializeField] private Material runFrozenMaterial;
    [SerializeField] private Material defaultRunMaterial;
    [SerializeField] private Material defaultAttackMaterial;

    private bool isMovingTowardsTree = true;
    private bool leftEscaping;
    private bool rightEscaping;
    private bool escaping;
    private bool frozen;
    private float pointXToMove;
    private const float pointYToMove = -5f;
    private const float pointZToMove = 9f;

    private Vector3 leftEscapingPosition;
    private Vector3 rightEscapingPosition;

    protected CatUpgrades catUpgrade;
    private Vector3 targetPos = new Vector3(0, pointYToMove, pointZToMove);

    private void Awake()
    {
        catUpgrade = FindObjectOfType<CatUpgrades>();
        leftEscapingPosition = GameObject.FindGameObjectWithTag("LeftEscapePlace").transform.position;
        rightEscapingPosition = GameObject.FindGameObjectWithTag("RightEscapePlace").transform.position;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void OnEnable()
    {
        CalculateCat();
        FindTarget();
    }

    private void OnDisable()
    {
        meshRenderer.material = defaultRunMaterial;
        isMovingTowardsTree = true;
        canAttack = true;
        escaping = false;
        frozen = false;
        leftEscaping = false;
        rightEscaping = false;
    }

    private void Update()
    {       
        if (isMovingTowardsTree)
            MoveTowardsTarget();

        if (escaping)
            EscapeFromTarget();
    }

    public void FindTarget()
    {
        pointXToMove = Random.Range(-0.8f, 0.8f);
        targetPos.x = pointXToMove;
    }

    public virtual void CalculateCat()
    {
    }

    public void MoveTowardsTarget()
    {        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.LookAt(targetPos);
    }   

    public void EscapeFromTarget()
    {    
        if (leftEscaping)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftEscapingPosition, speed * Time.deltaTime);
            transform.LookAt(leftEscapingPosition);
        }

        else if (rightEscaping)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightEscapingPosition, speed * Time.deltaTime);
            transform.LookAt(rightEscapingPosition);
        }
    }

    private void RestoreSpeed()
    {
        speed = speedBeforeFreeze * 0.75f;
    }

    private void CatAttack()
    {
        meshRenderer.material = defaultAttackMaterial;
        var XmasTree = FindObjectOfType<ChristmasTree>();

        if (XmasTree != null)
        {
            if (XmasTree.TreeHp > 0)
            {
                XmasTree.TakeDamage(damage);
                XmasTree.treeProviderEvent.OnTreeTakeDamage();
            }
        }
        else
        {
            FindObjectOfType<BossHealth>().TakeDamage(damage);
        }
        Invoke("EndAttack", 1f);
    }

    public void EndAttack()
    {
        if (frozen)
        {
            meshRenderer.material = runFrozenMaterial;
        }
        else
        {
            meshRenderer.material = defaultRunMaterial;
        }

        escaping = true;
    }

    private void FreezeCat()
    {
        meshRenderer.material = runFrozenMaterial;
        speedBeforeFreeze = speed;
        speed = 0;
        Invoke("RestoreSpeed", 0.5f);
        frozen = true;
    }

    private void TouchTarget()
    {
        isMovingTowardsTree = false;
        canAttack = false;

        if (!frozen)
            CatAttack();
        else
            EndAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canAttack == true)
        {
            if (transform.position.x < 0 && other.gameObject.CompareTag("TargetTrigger"))
            {
                TouchTarget();
                leftEscaping = true;
            }
            else if (transform.position.x >= 0 && other.gameObject.CompareTag("TargetTrigger"))
            {
                TouchTarget();
                rightEscaping = true;
            }
        }

        if (other.gameObject.CompareTag("Explosion")) 
        {
            if (frozen == false)
            FreezeCat();
        }
    }

}
