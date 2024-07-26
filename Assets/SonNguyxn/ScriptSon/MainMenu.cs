using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class MainMenu : MonoBehaviour
{
    public GameObject settingsCanvas;
    public GameObject mainMenuCanvas;// Gán Canvas "Settings" vào đây
    public Slider volumeSlider; // Gán trong Inspector
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public Material imageMaterial;
    public GameObject quitCanvas;
    public GameObject moreCanvas;
    public GameObject storyCanvas;
    public GameObject tutorialCanvas;
    public GameObject canvasVolume;
    public GameObject playCanvas;
    public GameObject languageCanvas;
    private bool inVolumeCanvas = false;
    private bool inTutorialCanvas = false;
    private bool inLanguageCanvas = false;
    public void PlayIntro()
    {
        // Chuyển đến Scene intro
        SceneManager.LoadScene("StoryGame");
        Time.timeScale = 1.0f;
    }
    public void LoadResume()
    {

    }
    public void Options()
    {
        settingsCanvas.SetActive(true); // Hiển thị Canvas "Settings"
        mainMenuCanvas.SetActive(false); // Ẩn MainMenu
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        quitCanvas.SetActive(false);
        canvasVolume.SetActive(false);
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }
    private void OnVolumeChanged(float value)
    {
        // Xử lý thay đổi âm lượng
        float volume = value; // Giá trị từ 0 đến 1
        // Áp dụng giá trị âm lượng vào âm thanh trong trò chơi của bạn
        audioSource.volume = volume;
    }
    public void ShowQuitConfirmation()
    {
        quitCanvas.SetActive(true); // Hiển thị QuitCanvas
    }
    public void ReturnToMainMenu()
    {
        if (inVolumeCanvas)
        {
            canvasVolume.SetActive(false);
            inVolumeCanvas = false;
        }
        else if(inTutorialCanvas)
        {
            tutorialCanvas.SetActive(false);
            inTutorialCanvas=false;
        }
        else if (inLanguageCanvas)
        {
            languageCanvas.SetActive(false);
            inLanguageCanvas = false;
        }
        else
        {
            quitCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            moreCanvas.SetActive(false);
            storyCanvas.SetActive(false);
            playCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
    }
    public void ShowVolumeCanvas()
    {
        canvasVolume.SetActive(true);
        inVolumeCanvas = true;
    }
    public void ShowMoreCanvas()
    {
        moreCanvas.SetActive(true);
    }
    public void ShowTutorialCanvas()
    {
        tutorialCanvas.SetActive(true);
        inTutorialCanvas = true;
    }
    public void ShowStoryCanvas()
    {
        storyCanvas.SetActive(true);
    }
    public void ShowPlayCanvas()
    {
        playCanvas.SetActive(true);
    }
    public void ShowLanguageCanvas()
    {
        languageCanvas.SetActive(true);
        inLanguageCanvas = true;
    }
}
