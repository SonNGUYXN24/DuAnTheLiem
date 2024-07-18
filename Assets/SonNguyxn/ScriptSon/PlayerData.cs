using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    // Các biến dữ liệu của người chơi
    public int playerHp;
    public int playerStamina;
    public int playerDamage;
    public int playerArmor;
    public int playerMoney;
    // ... (thêm các biến khác)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
