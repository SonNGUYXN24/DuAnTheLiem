using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodController : MonoBehaviour
{
    public StatusPlayer statusPlayer; // Tham chiếu đến script StatusPlayer
    private float timeSinceLastHeal;
    private float healInterval = 2f; // Hồi máu sau mỗi 2 giây

    // Hàm xử lý khi người chơi nhấn nút "Blood"
    public void OnBloodButtonClicked()
    {
        statusPlayer.IncreaseHealth(200); // Cộng 20 máu
        statusPlayer.currentBloods -= 1;
        statusPlayer.UpdateUI();
    }

    void Update()
    {
        // Hồi máu theo thời gian thực
        timeSinceLastHeal += Time.deltaTime;
        if (timeSinceLastHeal >= healInterval)
        {
            statusPlayer.Heal(2); // Hồi 1 máu
            timeSinceLastHeal = 0f;
        }
    }
}
