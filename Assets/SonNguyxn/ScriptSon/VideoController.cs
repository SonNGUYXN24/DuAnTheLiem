using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public string introSceneName = "StoryScene"; // Tên của Scene intro
    public string nextSceneName = "GameScene"; // Tên của Scene tiếp theo sau intro
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnIntroEnd;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Nhấn phím Space để bỏ qua intro và chuyển đến Scene tiếp theo
            LoadNextScene();
        }
    }
    public void PlayIntro()
    {
        // Chuyển đến Scene intro
        SceneManager.LoadScene(introSceneName);
    }

    private void OnIntroEnd(VideoPlayer vp)
    {
        // Gọi khi intro kết thúc
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        // Gọi khi người dùng nhấn phím Space hoặc khi intro kết thúc
        SceneManager.LoadScene(nextSceneName);
    }
}
