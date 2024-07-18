using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private bool isRestarting = false; // Biến để kiểm tra xem đã khởi động lại chưa

    // Gọi khi người chơi nhấn nút Replay
    public void Replay()
    {
        if (!isRestarting)
        {
            RestartGame();
        }
    }

    // Gọi khi người chơi nhấn nút MainMenu
    public void MainMenu()
    {
        if (!isRestarting)
        {
            LoadMainMenu();
        }
    }

    private void RestartGame()
    {
        isRestarting = true;
        Debug.Log("Replay button clicked!");

        // Tải lại Scene hiện tại (Scene vừa chơi)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Khôi phục thời gian chạy của game
        Time.timeScale = 1f;

        isRestarting = false;
    }

    private void LoadMainMenu()
    {
        isRestarting = true;

        Debug.Log("MainMenu button clicked!");
        // Chuyển đến Scene Menu
        SceneManager.LoadScene("MainMenu"); // Thay "MenuScene" bằng tên Scene Menu của bạn

        // Khôi phục thời gian chạy của game
        Time.timeScale = 1f;

        isRestarting = false;
    }
}
