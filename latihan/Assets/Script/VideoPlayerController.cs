using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer m_VideoPlayer;
    public GameObject videoPlayerCanvas;

    public VideoPlayer nextVideoPlayer;

    void Awake()
    {
        m_VideoPlayer = GetComponent<VideoPlayer>();
        m_VideoPlayer.loopPointReached += OnMovieFinished; // loopPointReached is the event for the end of the video
    }

    void OnMovieFinished(VideoPlayer player)
    {
        Debug.Log("Event for movie end called");
        PlayNext();
    }

    public void PlayNext()
    {

        m_VideoPlayer.Stop();
        nextVideoPlayer.Play();
        videoPlayerCanvas.SetActive(false);
    }



}
