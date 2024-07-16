using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRingController : MonoBehaviour
{
    public GameObject magicRingCanvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private void Start()
    {

    }

    private void Update()
    {

    }
    public void ReturnInvenTory()
    {
        magicRingCanvas.SetActive(false);
    }

    // Hàm ShowInfoKiem1 (nếu bạn muốn gọi từ nơi khác)
    public void ShowInfoMagicRing()
    {
        magicRingCanvas.SetActive(true);
        InfoKhien();
    }
    public void InfoKhien()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.ringInfo = 10000; // Giá trị Damage từ CungSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
