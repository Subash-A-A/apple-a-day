using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDamage;
    [SerializeField] float delayBetweenAttacks;
    [SerializeField] float detectionRange;
    [SerializeField] float stopDistance;
    [SerializeField] float jumpForce;
    [SerializeField] Transform uiCanvas;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;
    private GManager gManager;
    private Transform followTarget;

    private float dist;
    private float xDist;
    private float yDist;

    private int direction;
    private bool isGrounded;
    private bool canJump;
    private bool isChasing;
    private bool isTargetInRange;
    private bool canAttack = true;

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
        FaceDirection();
        Sensors();
        FaceDirection();
        EnemyAnimations();
    }

    private void FixedUpdate()
    {
        Move();
        
        if (isGrounded && yDist > gameObject.GetComponent<BoxCollider2D>().size.y && canJump && Random.value >= 0.3f)
        {
            StartCoroutine(Jump());
        }
    }

    private void Move()
    {   
        rb.velocity = (isTargetInRange) ? new Vector2(movementSpeed * direction, rb.velocity.y) : new Vector2(isGrounded?0f:rb.velocity.x, rb.velocity.y);
        isChasing = rb.velocity.magnitude >= 0.01f;
    }

    private void FaceDirection()
    {
        direction = followTarget.position.x > transform.position.x ? 1 : -1;
        if (isTargetInRange)
        {
            transform.localScale = new Vector3(direction, 1, 1);
            uiCanvas.localScale = new Vector3(direction, 1, 1);
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
        dist = Vector2.Distance(transform.position, followTarget.position);
        xDist = Mathf.Abs(transform.position.x - followTarget.position.x);
        yDist = Mathf.Abs(transform.position.y - followTarget.position.y);
        isTargetInRange =  dist <= detectionRange && xDist >= stopDistance;
        GroundCheck();
    }

    private IEnumerator Jump()
    {
        canJump = false;
        Vector2 jumpDirection = transform.up + direction * transform.right;
        rb.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);
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
