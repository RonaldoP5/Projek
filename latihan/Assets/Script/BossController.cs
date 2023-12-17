using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float bossHealth = 100f;
    public float currentHealth;
    public GameObject enemyPrefab;
    public Transform summonPoint;
    public int maxSummonedEnemies = 5;
    public float lineOfSightRadius = 10f;

    private bool isBossDead = false;
    private bool hasSummoned = false;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private HealthBarEnemy _healthBar;

    void Start()
    {
        currentHealth = bossHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        _healthBar = GetComponentInChildren<HealthBarEnemy>();
    }

    void Update()
    {
        if (!isBossDead)
        {
            CheckPlayerInLineOfSight();
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        StartCoroutine(Invulnerability());
        _healthBar.UpdateHealthBar(bossHealth, currentHealth);

        if (currentHealth <= 0)
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