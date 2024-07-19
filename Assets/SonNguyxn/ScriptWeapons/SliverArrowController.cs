using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliverArrowController : MonoBehaviour
{
    public GameObject sliverCanvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private bool infoButtonClicked = false;
    private void Start()
    {

    }

    private void Update()
    {

    }
    public void ReturnInvenTory()
    {
        sliverCanvas.SetActive(false);
    }

    // Hàm ShowInfoKiem1 (nếu bạn muốn gọi từ nơi khác)
    public void ShowInfoSliverArrow()
    {
        if (!infoButtonClicked) // Kiểm tra xem nút đã được nhấp chưa
        {
            sliverCanvas.SetActive(true);
            InfoCung();
            infoButtonClicked = true; // Đánh dấu nút đã được nhấp
        }
        else
        {
            // Tùy chọn, bạn có thể cung cấp phản hồi cho người dùng (ví dụ: hiển thị thông báo)
            Debug.Log("Nút thông tin đã được nhấp trước đó!");
        }
    }
    public void InfoCung()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.currentDamage += 500; // Giá trị Damage từ CungSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
