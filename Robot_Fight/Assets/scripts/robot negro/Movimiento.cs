using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad;
    private Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().position = new Vector2(4.93f, -1.01f);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float mvHorizontal = Input.GetAxisRaw("Horizontal"); //Unity toma por defecto a y d, o las teclas como input horizontales
        HandleMovement(mvHorizontal);
        animator.SetFloat("walk2 speed", mvHorizontal); //walk 2 es caminar hacia adelante
        animator.SetFloat("walk speed", mvHorizontal); //walk es caminar hacia atras

    }

    public void HandleMovement(float x)
    {
        rb.velocity = new Vector2(x*velocidad, rb.velocity.y); //reemplazo la velocidad en x por la variable velocidad que le pongo en Unity
    }

}
