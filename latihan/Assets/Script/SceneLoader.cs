using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public float duration = 1f;
    public float fadeInDuration = 0.2f;

    public void LoadScene(int level1index)
    {
        StartCoroutine(ShowLoadingScreenAndLoadScene(level1index));
    }

    IEnumerator ShowLoadingScreenAndLoadScene(int level1index)
    {
        loadingScreen.SetActive(true);

        // Animasi loading bar dari 0 ke 1 selama fadeInDuration
        float startTime = Time.time;
        float endTime = startTime + fadeInDuration;

        while (Time.time < endTime)
        {
            float elapsed = Time.time - startTime;
            float t = Mathf.Clamp01(elapsed / fadeInDuration);
            loadingBar.value = t;
            yield return null;
        }

        // Menunggu sebentar sebelum memulai loading scene
        yield return new WaitForSeconds(0.2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(level1index);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalisasi nilai progress
            loadingBar.value = progress;
            yield return null;
        }
    }
}
