using System.Collections;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    private Animator anim;
    private Transform followTarget;
    private bool canAttack = true;
    [SerializeField] GameObject[] hitBoxes;
    [SerializeField] float delayBetweenAttacks = 5f;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject PulseBullet;

    private bool isFacingRight = true;

    private void Start()
    {
        followTarget = FindObjectOfType<PlayerMovement>().transform;
        anim = GetComponent<Animator>();
        canAttack = true;
        isFacingRight = true;
    }

    private void Update()
    {
        if (canAttack)
        {
            StartCoroutine(BossAttack());
        }
        FaceDirection();
    }

    private void FaceDirection()
    {
        if (followTarget.position.x > transform.position.x)
        {
            transform.localRotation = Quaternion.identity;
            // uiCanvas.localRotation = Quaternion.identity;
            isFacingRight = true;
        }
        else if (followTarget.position.x < transform.position.x && isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            // uiCanvas.localRotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }
    }

    IEnumerator BossAttack()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(delayBetweenAttacks);
        canAttack = true;
    }

    public void AttackInit()
    {

    }

    public void SlashStart()
    {
        foreach (var hitBox in hitBoxes)
        {
            hitBox.SetActive(true);
        }
    }

    public void SlashEnd()
    {
        foreach (var hitBox in hitBoxes)
        {
            hitBox.SetActive(false);
        }
        GameObject bullet = Instantiate(PulseBullet, attackPoint.position, transform.rotation);
    }

    public void AttackEnd()
    {

    }
}
