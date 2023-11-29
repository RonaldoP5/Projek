using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ... (variabel lainnya)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied()
    {
        // Menampilkan layar kalah (tampilkan UI kalah di sini jika diperlukan)
        UIManager.Instance.ShowLoseMenu();
    }

    public void ContinueGame()
    {
        // Pemanggilan dari tombol "Continue" di UI kalah
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.RespawnPlayer();
        }
    }

    public void ReturnToMainMenu()
    {
        // Pemanggilan dari tombol "Return to Main Menu" di UI kalah
        SceneManager.LoadScene(0); // Ubah angka sesuai dengan indeks scene menu utama
    }

    // ... (method lainnya)
}
