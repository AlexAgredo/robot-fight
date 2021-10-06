using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private Rigidbody2D myRigidbody2D;
    private CharacterAnimation myAnim;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float speedv;

    private bool facingRight;
    //public bool isDefense;

    public float punchDamage;
    //public bool isDie;

    private EnemyControler GetEnemy;
    private Health myHealth;

    [SerializeField]
    private BarStat healthBar;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<CharacterAnimation>();
        myHealth = GetComponent<Health>();
        healthBar.bar = GameObject.FindGameObjectWithTag(tags.Player_Health_Bar).GetComponent<BarScript>();
        healthBar.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.MaxVal = myHealth.maxHealth;
        GetEnemy = GameObject.FindGameObjectWithTag(tags.Enemy_Tag).GetComponent<EnemyControler>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.CurrentVal = myHealth.healt;
        CheckUserInput();
        //DeadChecker();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis(Axis.Horizontal_Axis);
        HandleMovement(horizontal);
        

        float vertical = Input.GetAxis(Axis.Vertical_Axis);
        HandleMovementy(vertical);
        Flip(vertical);
    }

    private void HandleMovement(float horizontal) //IMPORTANTE PA VQUE SE MUEVA EN Y TOCA QUE HACER ALGO PARECIDO CREO
    {
        //if (isDie)
          //  return;

        myRigidbody2D.velocity = new Vector2(horizontal * speed, myRigidbody2D.velocity.y);
        myAnim.Walk(horizontal);
    }

    private void HandleMovementy(float vertical) //IMPORTANTE PA VQUE SE MUEVA EN Y TOCA QUE HACER ALGO PARECIDO CREO
    {
        //if (isDie)
        //  return;

        myRigidbody2D.velocity = new Vector2(vertical * speedv, myRigidbody2D.velocity.x);
        myAnim.Walkv(vertical);
    }

    private void Flip(float horizontal)
    {
        //if (isDie)
          //  return;

        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }

    /*void DeadChecker()
    {
        if (isDie)
            return;

        if (myHealth.healt <= 0)
        {
            isDie = true;
            myAnim.Die(isDie);
        }

    }*/

    private void CheckUserInput()
    {
        /*if (isDie)
            return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            isDefense = true;
            myAnim.Defense(isDefense);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isDefense = false;
            myAnim.Defense(isDefense);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (isDie)
          //  return;

        if (collision.tag == tags.Punch_Attack_Tag /*&& !isDefense*/)
        {
            myAnim.Hurt();
            myHealth.healt -= GetEnemy.punchDamage;
        }
    }

}

