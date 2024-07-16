using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyController : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public StatusPlayer statusPlayer;
    public GameObject waringCanvas;
    // Các biến giá tiền cho từng trang bị
    public int kiemCost = 1000;
    public int cungCost = 1500;
    public int sliverArCost = 3000;
    public int phiTieuCost = 9000;
    public int khienXinCost = 1500;
    public int helmetCost = 3000;
    public int aoGiapCost = 5000;
    public int chiecNhanCost = 10000;
    // ... (Thêm biến cho các trang bị khác)

    // Gọi hàm này khi người chơi nhấn vào nút "Buy" của một trang bị
    void Update()
    {
       
    }
    public void BuyKiemItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            kiemCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    public void BuyCungItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            cungCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    public void BuySliverArItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            sliverArCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    public void BuyKhienItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            khienXinCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    public void BuyPhiTieuItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            phiTieuCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    public void BuyAoGiapItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            aoGiapCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    public void BuyNhanItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            chiecNhanCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    public void BuyHelmetItem(int itemCost)
    {
        if (statusPlayer.currentMoney >= itemCost)
        {
            helmetCost = itemCost;
            statusPlayer.currentMoney -= itemCost;
            // Thêm logic để cấp phát trang bị cho người chơi ở đây
            statusPlayer.UpdateUI(); // Cập nhật giao diện trong StatusPlayer
            UpdateMoneyDisplay();
        }
        else
        {
            waringCanvas.SetActive(true);
        }
    }
    // Cập nhật hiển thị số tiền
    public void UpdateMoneyDisplay()
    {
        moneyText.text = $"{statusPlayer.currentMoney}";
    }
    public void ReturnBuyCanvas()
    {
        waringCanvas.SetActive(false);
    }
}
