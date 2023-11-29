using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverUI;

    public AudioManager audioManager;
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


    public void gameOver()
    {
        audioManager.StopAllAudio();
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Menghentikan waktu saat game over
    }

    /*public void PlayerDied()
    {
        // Menampilkan layar kalah (tampilkan UI kalah di sini jika diperlukan)
        UIManager.Instance.ShowLoseMenu();
    }*/

    public void ContinueGame()
    {
        // Pemanggilan dari tombol "Continue" di UI kalah
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.RespawnPlayer();
            audioManager.PlayAllAudio();
            gameOverUI.SetActive(false);
            Time.timeScale = 1f;
        }

 
    }

    public void ReturnToMainMenu()
    {
        // Pemanggilan dari tombol "Return to Main Menu" di UI kalah
        SceneManager.LoadScene(0); // Ubah angka sesuai dengan indeks scene menu utama
    }

 

    // ... (method lainnya)
}
