using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliverArrowController : MonoBehaviour
{
    public GameObject sliverCanvas; // Tham chiếu đến canvas kiem1Canvas
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
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
        sliverCanvas.SetActive(true);
        InfoCung();
    }
    public void InfoCung()
    {
        // Cập nhật giá trị Damage của Player
        statusPlayer.sliverArDamage = 500; // Giá trị Damage từ CungSonController
        statusPlayer.UpdateUI(); // Cập nhật giao diện
    }
}
