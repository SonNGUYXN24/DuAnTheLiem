using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CungSon : MonoBehaviour
{
    public GameObject cungCanvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private void Start()
    {

    }

    private void Update()
    {

    }
    public void ReturnInvenTory()
    {
        cungCanvas.SetActive(false);
    }

    // Hàm ShowInfoKiem1 (nếu bạn muốn gọi từ nơi khác)
    public void ShowInfoCung1()
    {
        cungCanvas.SetActive(true);
        InfoCung();
    }
    public void InfoCung()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.cungDamage = 300; // Giá trị Damage từ CungSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
