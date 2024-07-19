using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueController1 : MonoBehaviour
{
    public string lastSceneName; // Lưu tên của Scene trước đó

    private void Update()
    {
        // Lấy tên của Scene hiện tại
        lastSceneName = SceneManager.GetActiveScene().name;
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
