using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject pauseMenu;
    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Pause()
    {
        audioManager.StopAllAudio();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Continue()
    {
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
