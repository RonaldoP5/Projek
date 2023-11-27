using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Menyimpan checkpoint terakhir
    private Vector3 lastCheckpointPosition;

    // Referensi ke objek player
    private GameObject player;

    private void Start()
    {
        // Temukan objek player di awal permainan
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Set respawn point awal
            lastCheckpointPosition = player.transform.position;
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }
    }

    // Fungsi untuk memanggil saat player mati
    public void GameOver()
    {
        // Menampilkan panel game over
        // (Anda bisa menggunakan animasi atau fungsi lain untuk menampilkan panel)

        // Menyimpan checkpoint terakhir
        lastCheckpointPosition = player.GetComponent<PlayerHealth>().GetRespawnPoint();
    }

    // Fungsi untuk retry dari checkpoint terakhir
    public void Retry()
    {
        // Memuat ulang level dengan menggunakan checkpoint terakhir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Atur posisi player ke checkpoint terakhir
        player.transform.position = lastCheckpointPosition;
    }

    // Fungsi untuk kembali ke main menu
    public void ExitToMainMenu()
    {
        // Memuat main menu
        SceneManager.LoadScene("MainMenuScene");
    }
}