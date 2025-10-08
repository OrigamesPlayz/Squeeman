using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscControl : MonoBehaviour
{
    public bool isPaused = false;
    public Transform paused;
    public Transform canvas;
    // Start is called before the first frame update
    void Start()
    {
        paused.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        ShowMenu();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                paused.transform.SetParent(canvas);
            }
        }

        if (isPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    private void ShowMenu()
    {
        if (isPaused == false)
        {
            paused.transform.SetParent(null);
        }
    }
}
