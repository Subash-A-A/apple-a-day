using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public float attackDamage;
    public float delayBetweenAttacks;
    [SerializeField] float detectionRange;
    [SerializeField] float stopDistance;
    [SerializeField] Transform uiCanvas;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] Transform patrolSensor;
    [SerializeField] LayerMask whatIsGround;

    [Header("Particles")]
    [SerializeField] private GameObject jumpEffect;

    private Rigidbody2D rb;
    private Animator anim;
    private GManager gManager;
    private Transform followTarget;

    private float dist;
    private float xDist;
    private float yDist;

    private int direction;
    private bool isGrounded;
    private bool prevFrameIsGrounded;
    private bool canJump;
    private bool isChasing;
    private bool isTargetInRange;
    private bool canAttack = true;
    private bool isFacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gManager = FindObjectOfType<GManager>();

        if (gManager)
        {
            followTarget = gManager.GetPlayerTransform();
        }

        canAttack = true;
        canJump = true;
    }

    private void Update()
    {
        if (followTarget)
        {
            Sensors();
            FaceDirection();
            EnemyAnimations();
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (isGrounded && yDist > gameObject.GetComponent<BoxCollider2D>().size.y && canJump && Random.value >= 0.3f && isTargetInRange)
        {
            StartCoroutine(Jump());
        }
    }
    private void Move()
    {
        rb.velocity = (isTargetInRange) ? new Vector2(movementSpeed * direction, rb.velocity.y) : new Vector2(isGrounded?0f:rb.velocity.x * 0.5f, rb.velocity.y);
        isChasing = rb.velocity.magnitude >= 0.01f;
    }

    private void FaceDirection()
    {
        direction = followTarget.position.x > transform.position.x ? 1 : -1;

        if (followTarget.position.x > transform.position.x)
        {
            transform.localRotation = Quaternion.identity;
            uiCanvas.localRotation = Quaternion.identity;
            isFacingRight = true;
        }
        else if (followTarget.position.x < transform.position.x && isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            uiCanvas.localRotation = Quaternion.Euler(0, 180, 0);

            isFacingRight = false;
        }
    }

    private void GroundCheck()
    {
        prevFrameIsGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, whatIsGround);

        if (!prevFrameIsGrounded && isGrounded)
        {
            GameObject effect = Instantiate(jumpEffect, groundCheckTransform.position, Quaternion.identity);
            Destroy(effect, 0.75f);
        }
    }

    private void EnemyAnimations()
    {   
        anim.SetBool("isChasing", isChasing);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Sensors()
    {
        dist = Vector2.Distance(transform.position, followTarget.position);
        xDist = Mathf.Abs(transform.position.x - followTarget.position.x);
        yDist = followTarget.position.y - transform.position.y;
        isTargetInRange =  dist <= detectionRange && xDist >= stopDistance;
        
        GroundCheck();
    }

    private IEnumerator Jump()
    {
        canJump = false;
        Vector2 jumpDirection = transform.up + direction * transform.right;
        rb.AddForce(jumpForce * rb.mass * jumpDirection.normalized, ForceMode2D.Impulse);
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        canJump = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && canAttack)
        {
            StartCoroutine(DealDamage(collision.gameObject));
        }
    }
    
    IEnumerator DealDamage(GameObject obj)
    {
        obj.GetComponent<Health>().TakeDamage(attackDamage);
        canAttack = false;
        yield return new WaitForSeconds(delayBetweenAttacks);
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MeeleWeapon"))
        {
            gameObject.GetComponent<Health>().TakeDamage(25f);
        }
    }
}
