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
    public float fadeDuration = 1f;
    private bool isFading = false;

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
    private void Die()
    {
        // Tambahkan logika kematian enemy di sini, misalnya memanggil animasi atau menghancurkan GameObject
        if (!isFading)
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }
    private IEnumerator FadeOutAndDestroy()
    {
        isFading = true;
        float timer = 0f;

        // Get all SpriteRenderers in the hierarchy
        SpriteRenderer[] allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            // Set the alpha for all SpriteRenderers
            foreach (var spriteRenderer in allSpriteRenderers)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Setelah fade selesai, hancurkan objek
        Destroy(gameObject);

        isBossDead = true;
        isFading = false;
    }

}