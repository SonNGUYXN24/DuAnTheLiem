using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueController : MonoBehaviour
{
    private string lastSceneName; // Lưu tên của Scene trước đó
    public ContinueController1 controller1;
    private void Start()
    {
        
    }

    public void GoToMainMenu()
    {
        // Load Scene MainMenu
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        // Load lại Scene trước đó (Scene4 trong trường hợp này)
        SceneManager.LoadScene(lastSceneName);
    }
}
