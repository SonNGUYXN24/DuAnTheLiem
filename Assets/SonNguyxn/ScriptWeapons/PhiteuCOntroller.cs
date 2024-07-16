using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhiteuCOntroller : MonoBehaviour
{
    public GameObject phiTieuCanvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
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
        phiTieuCanvas.SetActive(true);
        InfoKhien();
    }
    public void InfoKhien()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.phiTieuInfo = 1000; // Giá trị Damage từ CungSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
