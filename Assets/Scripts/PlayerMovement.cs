using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb;
    public bool facingRight;
    private float defaultMoveSpeed;
    private bool isMoving;
    Vector2 move;

    void Start() {
        defaultMoveSpeed = 7f;
    }
    
    void Update()
    {
        AnimatorSettings();
        Move();
    }

    private void Flip(float horizontal) 
    {
        if(horizontal > 0 && facingRight || horizontal < 0 && !facingRight)
        {
            facingRight = !facingRight;
            /*
            Vector3 characterScale = transform.localScale;
            characterScale.x *= -1;
            transform.localScale = characterScale; */
            transform.Rotate(0f, 180f, 0f);
        }

    }
    private void AnimatorSettings() 
    {
        // Animator
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) 
            isMoving = true;
        else
            isMoving = false;
        animator.SetBool("IsMoving", isMoving);


    }
    private void Move()
    {
        // Correr
        if(Input.GetKeyDown(KeyCode.LeftShift))
            defaultMoveSpeed = 12f;
        if(Input.GetKeyUp(KeyCode.LeftShift))
            defaultMoveSpeed = 7f;

        // Movimentação
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        
        // Inverter animação
        float hz = Input.GetAxis("Horizontal");
        Flip(hz);

        // Mover
        rb.MovePosition(rb.position + move * defaultMoveSpeed * Time.deltaTime);
    }
    
}
