using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoGiapLuaController : MonoBehaviour
{
    public GameObject aoGiapLuaCanvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private void Start()
    {

    }

    private void Update()
    {

    }
    public void ReturnInvenTory()
    {
        aoGiapLuaCanvas.SetActive(false);
    }

    // Hàm ShowInfoKiem1 (nếu bạn muốn gọi từ nơi khác)
    public void ShowInfoAoGiapLua()
    {
        aoGiapLuaCanvas.SetActive(true);
        InfoKhien();
    }
    public void InfoKhien()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.aoGiapLuaHp = 20000; // Giá trị Damage từ CungSonController
        statusPlayer.aoGiapLuaStamina = 500;
        statusPlayer.aoGiapLuaArmor = 2000;
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
