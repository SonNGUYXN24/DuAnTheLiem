using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiemSonController : MonoBehaviour
{
    public GameObject kiem1Canvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private bool infoButtonClicked = false;
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
        if (!infoButtonClicked) // Kiểm tra xem nút đã được nhấp chưa
        {
            kiem1Canvas.SetActive(true);
            InfoKiem();
            infoButtonClicked = true; // Đánh dấu nút đã được nhấp
        }
        else
        {
            // Tùy chọn, bạn có thể cung cấp phản hồi cho người dùng (ví dụ: hiển thị thông báo)
            Debug.Log("Nút thông tin đã được nhấp trước đó!");
        }
    }
    public void InfoKiem()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.currentDamage += 100; // Giá trị Damage từ KiemSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
