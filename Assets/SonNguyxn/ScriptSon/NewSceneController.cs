using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneController : MonoBehaviour
{
    private void Start()
    {
        // Lấy dữ liệu từ Singleton
        int hp = PlayerData.Instance.playerHp;
        int stamina = PlayerData.Instance.playerStamina;
        int money = PlayerData.Instance.playerMoney;
        int armor = PlayerData.Instance.playerArmor;
        int damage = PlayerData.Instance.playerDamage;
        // ... (lấy các biến khác)

        Debug.Log($"Player HP in Scene 2: {hp}");
        Debug.Log($"Player Stamina in Scene 2: {stamina}");
        // ... (hiển thị các biến khác)
    }
}
