using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Các biến lưu trữ thông tin
    private int coins;
    private int greenDiamonds;
    private int purpleDiamonds;
    private int money;
    private int blood;

    // Các biến lưu trữ trạng thái trang bị đã mua
    private bool hasKiem;
    private bool hasGiap;
    private bool hasHelmet;
    private bool hasCung;
    private bool hasSliverArrow;
    private bool hasKhien;
    private bool hasNhan;
    private bool hasPhiTieu;
    private bool hasAoGiap;

    // Hàm lưu trạng thái
    public void SavePlayerInventory()
    {
        // Lưu thông tin vào PlayerPrefs hoặc nơi lưu trữ khác
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("GreenDiamonds", greenDiamonds);
        PlayerPrefs.SetInt("PurpleDiamonds", purpleDiamonds);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Blood", blood);

        // Lưu trạng thái trang bị đã mua
        PlayerPrefs.SetInt("HasKiem", hasKiem ? 1 : 0);
        PlayerPrefs.SetInt("HasGiap", hasGiap ? 1 : 0);
        PlayerPrefs.SetInt("HasCung", hasCung ? 1 : 0);
        PlayerPrefs.SetInt("HasSliverArrow", hasSliverArrow ? 1 : 0);
        PlayerPrefs.SetInt("HasKhien", hasKhien ? 1 : 0);
        PlayerPrefs.SetInt("HasNhan", hasNhan ? 1 : 0);
        PlayerPrefs.SetInt("HasHelmet", hasHelmet ? 1 : 0);
        PlayerPrefs.SetInt("HasPhiTieu", hasPhiTieu ? 1 : 0);
        PlayerPrefs.SetInt("HasAoGiap", hasAoGiap ? 1 : 0);

    }

    // Hàm load trạng thái
    public void LoadPlayerInventory()
    {
        // Load từ PlayerPrefs hoặc nơi lưu trữ khác
        coins = PlayerPrefs.GetInt("Coins");
        greenDiamonds = PlayerPrefs.GetInt("GreenDiamonds");
        purpleDiamonds = PlayerPrefs.GetInt("PurpleDiamonds");
        money = PlayerPrefs.GetInt("Money");
        blood = PlayerPrefs.GetInt("Blood");

        // Load trạng thái trang bị đã mua
        hasKiem = PlayerPrefs.GetInt("HasKiem") == 1;
        hasGiap = PlayerPrefs.GetInt("HasGiap") == 1;
        hasHelmet = PlayerPrefs.GetInt("HasHelmet") == 1;
        // ... (Load các trạng thái khác tương tự)
    }
}
