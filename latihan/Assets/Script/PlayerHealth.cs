using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
     AudioManager audioManager;
    public float maxHealth = 100f;
    private float currentHealth;
    private Vector3 respawnPoint;

    [SerializeField] private HealthBarUI healthBar;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float slowFactor = 0.5f;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private float originalMoveSpeed;
    private bool isSlowed = false;
    private float slowEndTime;

    private bool isTrap = false;

    private Bergerak bergerak;

    public GameManager gameManager;

    private bool isDead;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        bergerak = GetComponent<Bergerak>();
        originalMoveSpeed = bergerak.jalan;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isSlowed && Time.time >= slowEndTime)
        {
            isSlowed = false;
            bergerak.jalan = originalMoveSpeed;
        }

        if (currentHealth <= 0 && !isDead)
            if (isSlowed && Time.time >= slowEndTime)
            {
                isSlowed = false;
                bergerak.jalan = originalMoveSpeed;
            }

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            RespawnPlayer();
            gameManager.gameOver();
        }
        else if (currentHealth > 0 && isDead)
        {
            isDead = false; // Setelah respawn, atur isDead kembali ke false
        }
    }

    public void RespawnPlayer()
    {
        transform.position = respawnPoint;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public void SetRespawnPoint(Vector3 checkpointPosition)
    {
        respawnPoint = checkpointPosition;
    }

    public Vector3 GetRespawnPoint()
    {
        return respawnPoint;
    }

    public void TakeDamage(float damageAmount, bool isTrap)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        StartCoroutine(Invulnerability());

        if (isTrap)
        {
            isSlowed = true;
            bergerak.jalan *= slowFactor;
            slowEndTime = Time.time + slowDuration;
        }
    }

    public void Heal(float healAmount)
    {
        audioManager.PlaySFX(audioManager.potion);
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        isTrap = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObstacleTrap"))
        {
            isTrap = true;
            TakeDamage(10f, isTrap);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("HealingItem"))
        {
            isTrap = false;
            Heal(60f);
            Destroy(other.gameObject);
        }
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
}
