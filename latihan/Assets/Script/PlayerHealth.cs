using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    
    private Animator animator;

    AudioManager audioManager;
    public float maxHealth = 100f;
    public float currentHealth;
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

    public Bergerak bergerak;

    public GameManager gameManager;

    private bool isDead;

    private Rigidbody2D playerRb;
    public EnemyManager enemyManager;

    private void Start()
    {

        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        GameObject player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        bergerak = GetComponent<Bergerak>();
        originalMoveSpeed = bergerak.jalan;
        spriteRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SlowedTrap();
        PlayerIsDead();
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void SlowedTrap()
    {
        if (isSlowed && Time.time >= slowEndTime)
        {
            isSlowed = false;
            bergerak.jalan = originalMoveSpeed;
        }
    }

    public void PlayerIsDead()
    {
        if (currentHealth <= 0 && !isDead)
        {
            playerRb.velocity = Vector2.zero;
            // Trigger animasi kematian
            isDead = true;
            animator.SetTrigger("isDead");
            audioManager.PlayerStopSfx();

            // Tunggu sebentar sebelum memanggil game over
            StartCoroutine(GameOverAfterDeathAnimation());

            bergerak.enabled = false;
        }

    }

    void OnDestroy()
    {
        // Panggil metode EnemyDestroyed di EnemyManager saat musuh dihancurkan
        enemyManager.EnemyDestroyed(gameObject);
    }
    IEnumerator GameOverAfterDeathAnimation()
    {
        // Tunggu beberapa detik sebelum memanggil game over
        yield return new WaitForSeconds(1);
        

        
        // Munculkan game over setelah animasi kematian selesai
        gameManager.gameOver();
    }


    public void RespawnPlayer()
    {
        if (isDead)  // Only respawn if the player is dead
        {
            bergerak.enabled = true;
            animator.Play("Idle");
            transform.position = respawnPoint;
            currentHealth = maxHealth;
            StartCoroutine(RespawnCoroutine());
            healthBar.SetHealth(currentHealth);
            isDead = false;  // Reset isDead flag when the player respawns
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f);  // Sesuaikan waktu berdasarkan durasi animasi respawn Anda
        animator.Play("Idle");  // Ganti dengan nama animasi idle yang sesuai
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
            audioManager.PlaySFX(audioManager.trap);
            audioManager.PlaySFX(audioManager.trap2);
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