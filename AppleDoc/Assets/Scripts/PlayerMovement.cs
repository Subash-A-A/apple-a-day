using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    [Header("Particles")]
    [SerializeField] GameObject jumpEffect;

    [Header("Refrences")]
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask whatIsGround;

    private Animator anim;
    private float horizontal;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        MyInput();
        Move();
        Jump();
    }

    private void MyInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("velX", horizontal);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Move()
    {   
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, whatIsGround);
        isJumping = Input.GetButtonDown("Jump");


        if (isGrounded && isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Instantiate(jumpEffect, groundCheckTransform.position, Quaternion.identity);
        }
    }
}
