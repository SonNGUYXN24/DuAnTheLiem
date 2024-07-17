using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Gọi khi người chơi nhấn nút Replay
    public void Replay()
    {
        // Tải lại Scene hiện tại (Scene vừa chơi)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Khôi phục thời gian chạy của game
        Time.timeScale = 1f;
    }

    // Gọi khi người chơi nhấn nút MainMenu
    public void MainMenu()
    {
        // Chuyển đến Scene Menu
        SceneManager.LoadScene("MainMenu"); // Thay "MenuScene" bằng tên Scene Menu của bạn

        // Khôi phục thời gian chạy của game
        Time.timeScale = 1f;
    }
}
