using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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

    public void ShowLoseMenu()
    {
        // Tampilkan UI kalah di sini (aktifkan panel, tampilkan teks, dll.)
    }

    public void ContinueGame()
    {
        // Pemanggilan dari tombol "Continue" di UI kalah
        GameManager.Instance.ContinueGame();
    }

    public void ReturnToMainMenu()
    {
        // Pemanggilan dari tombol "Return to Main Menu" di UI kalah
        GameManager.Instance.ReturnToMainMenu();
    }

    // ... (method lainnya)
}
