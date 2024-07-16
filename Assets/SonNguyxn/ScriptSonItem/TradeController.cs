using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeController : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI greenDiamondText;
    public TextMeshProUGUI purpleDiamondText;
    public TextMeshProUGUI coinsText;
    public StatusPlayer statusPlayer;
    public GameObject warning2Canvas;
    private int moneyPlayer;
    private int greenDiamonds = 0;
    private int purpleDiamonds = 0;
    private int coins = 0;
    void Start()
    {
        moneyPlayer = statusPlayer.currentMoney;  
    }
    void Update()
    {
        // Cập nhật thông tin trên giao diện
        moneyText.text = $"{statusPlayer.currentMoney}";
        greenDiamondText.text = $"{greenDiamonds}";
        purpleDiamondText.text = $"{purpleDiamonds}";
        coinsText.text = $"{coins}";
    }

    public void TradeGreenDiamond()
    {
        if (greenDiamonds > 0)
        {
            moneyPlayer += 1000; // Giả sử mỗi Diamond xanh trao đổi được 100$
            greenDiamonds -= 1; // Trừ đi một Diamond xanh
            Update();
        }
        // Có thể thêm xử lý khi không có đủ Diamond xanh ở đây
    }

    public void TradePurpleDiamond()
    {
        if (purpleDiamonds > 0)
        {
            moneyPlayer += 700; // Giả sử mỗi Diamond tím trao đổi được 70$
            purpleDiamonds -= 1; // Trừ đi một Diamond tím
            Update();
        }
        else
        {
            warning2Canvas.SetActive(true);
        }
        // Có thể thêm xử lý khi không có đủ Diamond tím ở đây
    }

    public void TradeCoins()
    {
        if (coins > 0)
        {
            moneyPlayer += 200; // Giả sử mỗi Coin trao đổi được 20$
            coins -= 1; // Trừ đi một Coin
            Update();
        }
        else
        {
            warning2Canvas.SetActive(true);
        }
        // Có thể thêm xử lý khi không có đủ Coin ở đây
    }
    public void ReturnTradeCanvas()
    {
        warning2Canvas.SetActive(false);
    }
}
