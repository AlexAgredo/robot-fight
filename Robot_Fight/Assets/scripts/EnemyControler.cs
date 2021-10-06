using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    private CharacterAnimation enemyAim;
    private Rigidbody2D enemyRigidBody;

    [SerializeField]
    private float mspeed;
    //[SerializeField] MVO VERTICAL
    //private float mspeedv; MVO VERTICLA

    private Transform playerTransform;

    private bool followPlayer;
    //private bool followPlayerv; MVO VERTIAL
    private bool attackPlayer;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float chasePlayerAfterAttack;

    private float currentAttackTimer;
    [SerializeField]
    private float defaultAttackTimer;

    private Collider2D playerCollider;

    [SerializeField]
    private GameObject punch1AttackPoint;
    [SerializeField]
    private GameObject punch2AttackPoint;

    public float punchDamage;
    //public bool isDie;

    private playerController GetPlayer;
    private Health myHealth;

     [SerializeField]
     private BarStat healtBar;

    private Vector2 movement;
    private void Awake()
    {
        myHealth = GetComponent<Health>();
        playerCollider = GameObject.FindGameObjectWithTag(tags.Player_Tag).GetComponent<Collider2D>();

        enemyAim = GetComponent<CharacterAnimation>();
        enemyRigidBody = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindGameObjectWithTag(tags.Player_Tag).transform;

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider);
        healtBar.bar = GameObject.FindGameObjectWithTag(tags.Enemy_Health_Bar).GetComponent<BarScript>();
        healtBar.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        healtBar.MaxVal = myHealth.maxHealth;
        GetPlayer = GameObject.FindGameObjectWithTag(tags.Player_Tag).GetComponent<playerController>();
        followPlayer = true;
        //followPlayerv = true;
        currentAttackTimer = defaultAttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        healtBar.CurrentVal = myHealth.healt;
        FacingToTarget();
        //DeadChecker();

        ///// INTENTO VERTICAL ANGULO
        /*Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRigidBody.rotation = angle;
        direction.Normalize();
        movement = direction;*/

    }

    private void FixedUpdate()
    {
        FollowPlayer();
        //FollowPlayerv(movement); INTENTO VERTICAL CON ANGULO 
        AttackPlayer();
    }

    void FollowPlayer()
    {
        //if (!followPlayer || isDie)
          //  return;

        if (Mathf.Abs(transform.position.x - playerTransform.position.x) > attackDistance)
        {
            if (playerTransform.transform.position.x < transform.position.x)
            {
                enemyRigidBody.velocity = new Vector2(-1 * mspeed, enemyRigidBody.velocity.y);
            }
            else
            {
                enemyRigidBody.velocity = new Vector2(1 * mspeed, enemyRigidBody.velocity.y);
            }

            if (enemyRigidBody.velocity.sqrMagnitude != 0)
            {
                enemyAim.Walk(1);
            }
        }

        else if (Mathf.Abs(transform.position.x - playerTransform.position.x) <= attackDistance)
        {
            enemyRigidBody.velocity = Vector2.zero;
            enemyAim.Walk(0);
            followPlayer = false;
            attackPlayer = true;
        }

    }

    /*void FollowPlayerv(Vector2 direction) INTENTO VERTICAL ANGULO
    {
        //if (!followPlayer || isDie)
        //  return;
        enemyRigidBody.MovePosition((Vector2)transform.position + (direction * mspeed * Time.deltaTime));
       

    }*/

    void AttackPlayer()
    {
        //if (!attackPlayer || isDie)
          //  return;

        currentAttackTimer -= Time.deltaTime;

        if (currentAttackTimer <= 0)
        {
            Attack(Random.Range(0, 5));
            currentAttackTimer = defaultAttackTimer;
        }

        if (Mathf.Abs(transform.position.x - playerTransform.position.x) > attackDistance + chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }

    void FacingToTarget()
    {
        //if (isDie)
          //  return;

        if (playerTransform.transform.position.x < transform.position.x)
        {
            Vector2 theScale = transform.localScale;
            theScale.x = -4;
            transform.localScale = theScale;
        }
        else
        {
            Vector2 theScale = transform.localScale;
            theScale.x = 4;
            transform.localScale = theScale;
        }
    }

    IEnumerator Punch_1(float time)
    {
        yield return new WaitForSeconds(time);
        enemyAim.punchR();
    }

    IEnumerator Punch_2(float time)
    {
        yield return new WaitForSeconds(time);
        enemyAim.punchL();
    }

    void Attack(int i)
    {
        switch (i)
        {
            case 0:
                StartCoroutine(Punch_1(0.1f));
                break;
            case 1:
                StartCoroutine(Punch_1(0.1f));
                StartCoroutine(Punch_2(0.1f));
                break;
            case 2:
                StartCoroutine(Punch_2(0.1f));
                StartCoroutine(Punch_1(0.1f));
                break;
        }
    }

    /*void DeadChecker()
    {
        if (isDie)
            return;

        if (myHealth.healt <= 0)
        {
            isDie = true;
            enemyAim.Die(isDie);
        }

    }*/

    public void ActivatePunch1()
    {
        punch1AttackPoint.SetActive(true);
    }

    public void ActivatePunch2()
    {
        punch2AttackPoint.SetActive(true);
    }

    public void DeaactivatePunch1()
    {
        punch1AttackPoint.SetActive(false);
    }

    public void DeaactivatePunch2()
    {
        punch2AttackPoint.SetActive(false);
    }

    public void DeactiveAllAttack()
    {
        punch1AttackPoint.SetActive(false);
        punch2AttackPoint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (isDie)
          //  return;

        if (collision.tag == tags.Punch_Attack_Tag)
        {
            enemyAim.Hurt();
            myHealth.healt -= GetPlayer.punchDamage;
        }
    }
}
