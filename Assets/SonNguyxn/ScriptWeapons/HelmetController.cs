using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetController : MonoBehaviour
{
    public GameObject helmetCanvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private bool infoButtonClicked = false; // Cờ để theo dõi xem nút đã được nhấp hay chưa
    private void Start()
    {

    }

    private void Update()
    {

    }
    public void ReturnInvenTory()
    {
        helmetCanvas.SetActive(false);
    }

    // Hàm ShowInfoKiem1 (nếu bạn muốn gọi từ nơi khác)
    public void ShowInfoHelmet()
    {
        if (!infoButtonClicked) // Kiểm tra xem nút đã được nhấp chưa
        {
            helmetCanvas.SetActive(true);
            InfoKhien();
            infoButtonClicked = true; // Đánh dấu nút đã được nhấp
        }
        else
        {
            // Tùy chọn, bạn có thể cung cấp phản hồi cho người dùng (ví dụ: hiển thị thông báo)
            Debug.Log("Nút thông tin đã được nhấp trước đó!");
        }
    }
    public void InfoKhien()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.currentArmor += 200; // Giá trị Damage từ CungSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
