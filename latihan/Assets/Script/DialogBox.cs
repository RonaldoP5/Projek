using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public string nextSceneName;
    private int index;
    public VideoPlayerController videoPlayerController;
    

    // Start is called before the first frame update
    void Start()
    {   
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(TransitionToNextScene());
        }
    }

    IEnumerator TransitionToNextScene()
    {
        // Wait for a short delay before transitioning to the next scene
        yield return new WaitForSeconds(0.2f);

        // Load the next scene
        videoPlayerController.PlayNext();
    }
    public void NextDialogue()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }
    //----------------------------------------
        void NextLineToMainScene()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(TransitionToMainScene());
        }
    }
        IEnumerator TransitionToMainScene()
    {
        // Wait for a short delay before transitioning to the next scene
        yield return new WaitForSeconds(0.2f);

        // Load the next scene
        videoPlayerController.LoadNextScene();
    }
    public void NextDialogueToMainScene()
    {
        if (textComponent.text == lines[index])
        {
            NextLineToMainScene();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    //----------------------------------------
    void NextLineToMainMenu()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(TransitionToMainMenu());
        }
    }
    IEnumerator TransitionToMainMenu()
    {
        // Wait for a short delay before transitioning to the next scene
        yield return new WaitForSeconds(0.2f);

        // Load the next scene
        videoPlayerController.LoadMainMenu();
    }
    public void NextDialogueToMainMenu()
    {
        if (textComponent.text == lines[index])
        {
            NextLineToMainMenu();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }
}