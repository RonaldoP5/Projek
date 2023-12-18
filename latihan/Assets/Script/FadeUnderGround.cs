using System.Collections;
using UnityEngine;

public class FadeUnderGround : MonoBehaviour
{
    public float fadeDuration = 1f;
    private SpriteRenderer spriteRenderer;
    private bool isFading = false;

    private void Start()
    {
        // Mengambil SpriteRenderer dari child object dengan nama tertentu
        Transform mainCameraTransform = Camera.main.transform;
        Transform desiredChild = mainCameraTransform.Find("gradient"); // Ganti "NamaChildObjek" dengan nama yang sesuai

        if (desiredChild != null)
        {
            spriteRenderer = desiredChild.GetComponent<SpriteRenderer>();

            // Menyembunyikan objek pada saat awal permainan
            spriteRenderer.enabled = false;
        }
        else
        {
            Debug.LogError("Child object with the specified name not found.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        isFading = true;

        // Menyalakan objek ketika fade dimulai
        spriteRenderer.enabled = true;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        isFading = false;
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Menyembunyikan objek setelah fade selesai
        spriteRenderer.enabled = false;

        isFading = false;
    }
}