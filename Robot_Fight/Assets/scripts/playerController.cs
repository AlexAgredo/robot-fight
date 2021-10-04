using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    
    private Rigidbody2D myRigidbody2D;
    private CharacterAnimation myAnim;

    [SerializeField]
    private float speed;

    private bool facingRight;


    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<CharacterAnimation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;

    }

    // Update is called once per frame
    void Update()
    {
        //CheckUserInput();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis(Axis.Horizontal_Axis); //Unity toma por defecto a y d o las teclas como input
        HandleMovement(horizontal);
        Flip(horizontal);
    }

    private void HandleMovement(float horizontal)
    {
        myRigidbody2D.velocity = new Vector2(horizontal * speed, myRigidbody2D.velocity.y);
        myAnim.Walk(horizontal);
    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }
    }
   
}
