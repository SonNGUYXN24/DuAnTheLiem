using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public GameObject storeCanvas; // Tham chiếu đến canvas Store
    public GameObject buyCanvas;
    public GameObject tradeCanvas;
    public GameObject pauseController;
    void Update()
    {
        // Hiển thị canvas khi người chơi nhấn phím "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            storeCanvas.SetActive(true);
            buyCanvas.SetActive(true);
            pauseController.SetActive(false);
        }

        // Đóng canvas khi người chơi nhấn phím "ESC"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            storeCanvas.SetActive(false);
            buyCanvas.SetActive(false);
            tradeCanvas.SetActive(false);
        }
    }
    public void ShowBuyCanvas()
    {
        buyCanvas.SetActive(true);
        tradeCanvas.SetActive(false);
    }
    public void ShowTradeCanvas()
    {
        buyCanvas.SetActive(false);
        tradeCanvas.SetActive(true);
    }
}
