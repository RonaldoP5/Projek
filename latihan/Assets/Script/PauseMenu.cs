using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject pauseMenu;

    public GameObject healthBar;
    public bool isPaused;

    public bool option;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !option)
            {
                Continue();
                return;
            }
            if (!isPaused)
            {
                Pause();
                return;
            }
        }
    }

    public void SetOption(bool condition)
    {
        option = condition;
    }
    public void Pause()
    {
        Debug.Log("Pause function called");
        audioManager.SetBackgroundVolume(0.3f);
        audioManager.PlayerStopSfx();
        pauseMenu.SetActive(true);
        healthBar.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        audioManager.AttackStopSFX();
    }

    public void Continue()
    {
        Debug.Log("Continue function called");
        healthBar.SetActive(true);
        audioManager.PlayAllAudio();
        pauseMenu.SetActive(false); 
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturnToMainMenu()
    {
        // Menambahkan pemanggilan kembali ke menu utama dari tombol Pause Menu
        GameManager.Instance.ReturnToMainMenu();
    }
}
