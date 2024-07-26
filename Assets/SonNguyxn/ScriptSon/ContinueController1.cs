using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueController1 : MonoBehaviour
{
    private static string lastSceneName; // Biến static để lưu tên cảnh cuối cùng

    // Gọi phương thức này khi chuyển đến cảnh mới
    public static void SetLastScene(string sceneName)
    {
        lastSceneName = sceneName;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    // Gọi phương thức này để tiếp tục đến cảnh cuối cùng đã thăm
    public static void ContinueToLastScene()
    {
        if (!string.IsNullOrEmpty(lastSceneName))
        {
            SceneManager.LoadScene(lastSceneName);
            // Khôi phục thời gian chạy của game
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogWarning("Không có cảnh cuối cùng được ghi lại. Không thể tiếp tục.");
        }
    }
}
