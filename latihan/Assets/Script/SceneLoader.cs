using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    // Waktu yang diinginkan untuk loading bar naik dari 0 hingga 1
    public float duration = 1f;

    public void LoadScene(int level1index)
    {
        StartCoroutine(ShowLoadingScreenAndLoadScene(level1index));
    }

    IEnumerator ShowLoadingScreenAndLoadScene(int level1index)
    {
        // Tunda waktu sebelum menampilkan loading screen (contoh: 2 detik)
        yield return new WaitForSeconds(0.1f);

        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(level1index);

        float startTime = Time.time;

        while (!operation.isDone)
        {
            // Menghitung berapa lama loading sudah berlangsung
            float elapsed = Time.time - startTime;

            // Menghitung target value berdasarkan durasi waktu yang diinginkan
            float t = Mathf.Clamp01(elapsed / duration);

            // Menggunakan nilai waktu yang dihitung sebagai faktor langsung
            loadingBar.value = t;

            // Mengecek apakah loading sudah selesai
            if (elapsed >= duration)
            {
                break;
            }

            yield return null;
        }
    }
}
