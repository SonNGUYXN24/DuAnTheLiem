using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausecanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausecanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void ContinueClick()
    {
        pausecanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
