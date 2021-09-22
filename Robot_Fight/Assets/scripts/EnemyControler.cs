using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    private CharacterAnimation enemyAim;
    private Rigidbody2D enemyRigidBody;

    [SerializeField]
    private float mspeed;

    private Transform playerTransform;

    private bool followPlayer;
    private bool attackPlayer;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float chasePlayerAfterAttack;

    private float currentAttackTimer;
    [SerializeField]
    private float defaultAttackTimer;

    private Collider2D playerCollider;

    private void Awake()
    {
        playerCollider = GameObject.FindGameObjectWithTag(tags.Player_Tag).GetComponent<Collider2D>();

        enemyAim = GetComponent<CharacterAnimation>();
        enemyRigidBody = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindGameObjectWithTag(tags.Player_Tag).transform;

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider);
    }

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        currentAttackTimer = defaultAttackTimer; 
    }

    // Update is called once per frame
    void Update()
    {
        FacingToTarget();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
        AttackPlayer();
    }

    void FollowPlayer()
    {
        if (!followPlayer) 
            return;

        if(Mathf.Abs(transform.position.x - playerTransform.position.x) > attackDistance)
        {
            if(playerTransform.transform.position.x < transform.position.x)
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

        else if(Mathf.Abs(transform.position.x - playerTransform.position.x) <= attackDistance)
        {
            enemyRigidBody.velocity = Vector2.zero;
            enemyAim.Walk(0);
            followPlayer = false;
            attackPlayer = true;
        }
    }

    void AttackPlayer()
    {
        if (!attackPlayer)
            return;

        currentAttackTimer -= Time.deltaTime;

        if(currentAttackTimer <= 0)
        {
            Attack(Random.Range(0, 2));
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
        if(playerTransform.transform.position.x < transform.position.x)
        {
            Vector2 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
        else
        {
            Vector2 theScale = transform.localScale;
            theScale.x = 1;
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
}
