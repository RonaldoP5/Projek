using System.Collections;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    AudioManager audioManager;
    public float speed;
    public float lineOfSight;
    public float attackRange;
    public float attackCooldown = 2.0f;
    public float preAttackDuration = 1.0f;

    private Transform player;
    private bool isAttacking = false;
    private float timeSinceLastAttack = 0.0f;
    private bool isPlayerInLineOfSight = false;
    private float originalSize;
    public float currentDamage = 10f;
    private float sizeIncreaseAmount = 0.1f;
    private float damageIncreaseAmount = 5f;
    private int attackCount = 0;
    private int maxAttacks = 2;
    private PlayerHealth playerHealth;

    private Animator animator;
    void Start()
    {
        Debug.Log("Enemy Start");
        animator = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalSize = transform.localScale.x;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        Flip();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSight)
        {
            if (!isPlayerInLineOfSight)
            {
                isPlayerInLineOfSight = true;
                audioManager.BgmCombat();
                audioManager.StopMainBGM();
            }

            if (distanceFromPlayer <= attackRange)
            {
                if (!isAttacking && timeSinceLastAttack >= attackCooldown)
                {
                    StartCoroutine(PreAttack());
                }
            }
            else
            {
                timeSinceLastAttack = 0.0f;
                StopCoroutine(PreAttack());
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
        else
        {
            StopAttackPlayer();
        }

        if (playerHealth.GetCurrentHealth() <= 0)
        {
            StopAttackPlayer();
        }

        timeSinceLastAttack += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator PreAttack()
    {
        Debug.Log("PreAttack");
        isAttacking = true;

        yield return new WaitForSeconds(preAttackDuration);
        animator.SetTrigger("IsAttackingEnemy");


        AttackPlayer();

        //animator.SetTrigger("IdleEnemy");

        yield return new WaitForSeconds(attackCooldown - preAttackDuration);
        isAttacking = false;
    }

    public void StopAttackPlayer()
    {
        Debug.Log("StopAttackPlayer() called");
        if (isPlayerInLineOfSight && playerHealth.GetCurrentHealth() > 0)
        {
            isPlayerInLineOfSight = false;
            audioManager.StopBgmCombat();
            audioManager.PlayMainBGM();
        }
        animator.SetTrigger("IdleEnemy");
        timeSinceLastAttack = 0.0f;
        StopCoroutine(PreAttack());
    }

    void AttackPlayer()
    {
        if (attackCount < maxAttacks)
        {
            currentDamage += damageIncreaseAmount;
            float newSize = transform.localScale.x + sizeIncreaseAmount;
            transform.localScale = new Vector3(newSize, newSize, 1f);
            attackCount++;
        }

        player.GetComponent<PlayerHealth>().TakeDamage(currentDamage, false);
    }
}
