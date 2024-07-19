using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhiteuCOntroller : MonoBehaviour
{
    public GameObject phiTieuCanvas; // Tham chiếu đến canvas kiem1Canvas
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
        phiTieuCanvas.SetActive(false);
    }

    // Hàm ShowInfoKiem1 (nếu bạn muốn gọi từ nơi khác)
    public void ShowInfoPhiTieu()
    {
        if (!infoButtonClicked) // Kiểm tra xem nút đã được nhấp chưa
        {
            phiTieuCanvas.SetActive(true);
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
        statusPlayer.currentDamage += 1000; // Giá trị Damage từ CungSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
