using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Menambahkan callback ketika video selesai
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        // Callback ini akan dipanggil ketika video selesai
        LoadNextScene();
    }

    void LoadNextScene()
    {
        // Lakukan logika untuk memuat scene berikutnya
        // Contoh: Pindah ke scene dengan nama "GameplayScene"
        SceneManager.LoadScene(1);
    }
}
