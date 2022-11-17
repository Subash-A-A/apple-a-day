using UnityEngine;
using System.Collections;

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
    [SerializeField] Animator camAnim;

    private Animator anim;
    private float horizontal;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool prevFrameIsGrounded;
    private bool isJumping;

    public bool canDash;
    public bool isDashing;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        canDash = true;
    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        MyInput();
        Jump();
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Move();
    }

    private void MyInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        isJumping = Input.GetButtonDown("Jump");
    }

    private void Move()
    {   
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        prevFrameIsGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, whatIsGround);

        if(!prevFrameIsGrounded && isGrounded)
        {
            GameObject effect = Instantiate(jumpEffect, groundCheckTransform.position, Quaternion.identity);
            Destroy(effect, 0.75f);
        }

        if (isGrounded && isJumping)
        {
            rb.AddForce(transform.up.normalized * jumpForce, ForceMode2D.Impulse);
            GameObject effect = Instantiate(jumpEffect, groundCheckTransform.position, Quaternion.identity);
            Destroy(effect, 0.75f);
        }
    }

    private void PlayerAnimation()
    {
        anim.SetFloat("velX", horizontal);
        anim.SetBool("isGrounded", isGrounded);
    }

    public IEnumerator PlayerDashAttack(Vector2 dashDirection, float dashPower, float dashTime, float dashCooldown, TrailRenderer tr, GameObject hitbox)
    {
        canDash = false;
        isDashing = true;
        float origGravity = rb.gravityScale;
        camAnim.SetTrigger("Shake");
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        tr.emitting = true;
        hitbox.SetActive(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        rb.velocity = dashDirection.normalized * dashPower;
        yield return new WaitForSeconds(dashTime);
        gameObject.GetComponent<Collider2D>().enabled = true;
        rb.gravityScale = origGravity;
        isDashing = false;
        tr.emitting = false;
        hitbox.SetActive(false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
