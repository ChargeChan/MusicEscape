using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvasScript : MonoBehaviour
{
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelHelper());
    }

    public void Quit()
    {
        StartCoroutine (QuitHelper());
    }

    IEnumerator LoadLevelHelper()
    {
        transition.SetTrigger("CrossfadeExit");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(1);
    }

    IEnumerator QuitHelper()
    {
        transition.SetTrigger("CrossfadeExit");

        yield return new WaitForSeconds(1);

        Application.Quit();
    }
}
