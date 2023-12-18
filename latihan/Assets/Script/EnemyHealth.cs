using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    AudioManager audioManager;
    public float maxHealth = 100f;
    public float currentHealth;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public float fadeDuration = 1f;
    private bool isFading = false;
    private bool isPlayingAudio = false;

    private HealthBarEnemy _healthBar;


    // Properti tambahan untuk mendapatkan current health
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        spriteRend = GetComponent<SpriteRenderer>();
        _healthBar = GetComponentInChildren<HealthBarEnemy>();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(Invulnerability());

        _healthBar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
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

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Setelah fade selesai, hancurkan objek
        Destroy(gameObject);

        isFading = false;
        if (!isPlayingAudio)
        {
            isPlayingAudio = false;
            audioManager.StopBgmCombat();
            audioManager.PlayMainBGM();
        }
    }
}