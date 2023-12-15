using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float bossHealth = 100f;
    public GameObject enemyPrefab;
    public Transform summonPoint;
    public int maxSummonedEnemies = 5;
    public float lineOfSightRadius = 10f;

    private bool isBossDead = false;
    private bool hasSummoned = false;

    void Update()
    {
        if (!isBossDead)
        {
            CheckPlayerInLineOfSight();
            CheckPlayerAttack();
        }
    }

    void CheckPlayerInLineOfSight()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, lineOfSightRadius, LayerMask.GetMask("Player"));

        if (detectedObjects.Length > 0 && !hasSummoned)
        {
            hasSummoned = true;
            SummonEnemies();
        }
    }

    void CheckPlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, 2.0f, LayerMask.GetMask("Player"));

            foreach (Collider2D collider in detectedObjects)
            {
                PlayerCombatController playerCombat = collider.transform.GetComponent<PlayerCombatController>();

                if (playerCombat != null && playerCombat.IsAttacking())
                {
                    TakeDamage(playerCombat.GetAttackDamage());
                }
            }
        }
    }

    void TakeDamage(float damage)
    {
        bossHealth -= damage;

        if (bossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isBossDead = true;

        Destroy(gameObject);

        // Add logic for boss death (e.g., play death animation, spawn particles, etc.)
        // Note: You may want to disable the BossController component or set it to inactive to prevent further updates.;
    }

    void SummonEnemies()
    {
        int summonedEnemyCount = 0;

        while (summonedEnemyCount < maxSummonedEnemies)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Vector3 summonPosition = summonPoint.position + randomOffset;
            Instantiate(enemyPrefab, summonPosition, Quaternion.identity);

            summonedEnemyCount++;
            // Adjust rotation or other parameters as needed
        }

    }
}
