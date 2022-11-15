using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDamage;
    [SerializeField] float detectionRange;
    [SerializeField] float stopDistance;
    [SerializeField] float jumpForce;
    [SerializeField] Transform target;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;

    private float dist;
    private float xDist;
    private float yDist;

    private int direction;
    private bool isGrounded;
    private bool isChasing;
    private bool isTargetInRange;
    private bool canJumpChase;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        FaceDirection();
        Sensors();
        FaceDirection();
        EnemyAnimations();

        if (isGrounded && target.position.y > transform.position.y && yDist > gameObject.GetComponent<BoxCollider2D>().size.y)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {   
        rb.velocity = (isTargetInRange) ? new Vector2(movementSpeed * direction, rb.velocity.y) : new Vector2(isGrounded?0f:rb.velocity.x, rb.velocity.y);
        isChasing = rb.velocity.magnitude >= 0.01f;
    }

    private void FaceDirection()
    {
        direction = target.position.x > transform.position.x ? 1 : -1;
        if (isTargetInRange)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, whatIsGround);
    }

    private void EnemyAnimations()
    {
        anim.SetBool("isChasing", isChasing);
    }

    private void Sensors()
    {
        dist = Vector2.Distance(transform.position, target.position);
        xDist = Mathf.Abs(transform.position.x - target.position.x);
        yDist = Mathf.Abs(transform.position.y - target.position.y);
        isTargetInRange =  dist <= detectionRange && xDist >= stopDistance;
        GroundCheck();
    }

    private void Jump()
    {
        Vector2 jumpDirection = transform.up + direction * transform.right;
        rb.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);
    }
}
