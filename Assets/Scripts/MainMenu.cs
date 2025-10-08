using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera playerCam;
    public GameObject main;
    public GameObject opt;
    private EscControl escControl;

    void Start()
    {
        escControl = playerCam.GetComponent<EscControl>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToGame()
    {
        if (escControl != null)
        {
            escControl.isPaused = false;
        }
    }

    public void Settings()
    {
        // Start the animation coroutine
        StartCoroutine(MoveSmoothlyM(main.transform, new Vector3(0, 2, 0), 1f));
        StartCoroutine(MoveSmoothlyS(opt.transform, new Vector3(0, 0, 0), 1f));
    }

    public void SettingsBack()
    {
        StartCoroutine(MoveSmoothlyS(opt.transform, new Vector3(0, 2, 0), 1f));
        StartCoroutine(MoveSmoothlyM(main.transform, new Vector3(0, 0, 0), 1f));
    }

    private IEnumerator MoveSmoothlyM(Transform target, Vector3 endPos, float duration)
    {
        Vector3 startPos = target.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Ease-out (slow at the end)
            float t = elapsed / duration;
            t = 1 - Mathf.Pow(1 - t, 3); // smooth cubic ease-out

            target.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        target.position = endPos; // ensure exact final position
    }

    private IEnumerator MoveSmoothlyS(Transform target, Vector3 endPos, float duration)
    {
        Vector3 startPos = target.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Ease-out (slow at the end)
            float t = elapsed / duration;
            t = 1 - Mathf.Pow(1 - t, 3); // smooth cubic ease-out

            target.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        target.position = endPos; // ensure exact final position
    }
}
