using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer m_VideoPlayer;
    public GameObject videoPlayerCanvas;
    public VideoPlayer nextVideoPlayer;

    public bool isMovieFinishedEventActive = true;

    void Awake()
    {
        m_VideoPlayer = GetComponent<VideoPlayer>();
        m_VideoPlayer.loopPointReached += OnMovieFinished;
    }

    void OnMovieFinished(VideoPlayer player)
    {
        if (isMovieFinishedEventActive)
        {
            Debug.Log("Event for movie end called");
            PlayNext();
        }
    }

    public void PlayNext()
    {
        m_VideoPlayer.Stop();
        nextVideoPlayer.Play();
        videoPlayerCanvas.SetActive(false);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    // Fungsi untuk menonaktifkan event OnMovieFinished
    public void DisableMovieFinishedEvent()
    {
        isMovieFinishedEventActive = false;
    }

    // Fungsi untuk mengaktifkan event OnMovieFinished
    public void EnableMovieFinishedEvent()
    {
        isMovieFinishedEventActive = true;
    }
}
