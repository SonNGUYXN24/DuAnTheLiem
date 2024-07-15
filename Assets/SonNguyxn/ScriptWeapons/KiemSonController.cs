using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiemSonController : MonoBehaviour
{
    public GameObject kiem1Canvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private void Start()
    {

    }

    private void Update()
    {
        // Tắt canvas khi người dùng nhấn phím "ESC"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            kiem1Canvas.SetActive(false);
        }
    }
    public void ReturnInvenTory()
    {
        kiem1Canvas.SetActive(false);
    }

// Hàm ShowInfoKiem1 (nếu bạn muốn gọi từ nơi khác)
    public void ShowInfoKiem1()
    {
        kiem1Canvas.SetActive(true);
        InfoKiem();
    }
    public void InfoKiem()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.kiemDamage = 100; // Giá trị Damage từ KiemSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
