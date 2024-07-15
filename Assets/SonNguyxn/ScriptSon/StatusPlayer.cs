using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusPlayer : MonoBehaviour
{
    public Slider hpSlider; // Thanh Slider cho HP
    public Slider staminaSlider; // Thanh Slider cho Stamina
    public TextMeshProUGUI damageText; // TextMeshPro cho Damage
    public TextMeshProUGUI armorText; // TextMeshPro cho Armor
    public TextMeshProUGUI hpText; // TextMeshPro cho hiển thị HP
    public TextMeshProUGUI staminaText; // TextMeshPro cho hiển thị Stamina
    public TextMeshProUGUI moneyCount; //TextMeshPro hiển thị Money của người chơi.
    public Animator anim;
    private int maxHp = 100; // Máu tối đa
    private int maxStamina = 100; // Stamina tối đa
    public int kiemDamage; // Giá trị Damage của Kiem
    public int cungDamage; // Giá trị Damage của Kiem
    private int currentHp; // Máu hiện tại
    private int currentStamina; // Stamina hiện tại
    private int currentDamage; // Damage hiện tại
    private int currentArmor; // Armor hiện tại
    private int baseDamage = 100; // Damage cơ bản
    private int baseArmor = 100; // Armor cơ bản
    private int baseMoney = 100; // Money cơ bản
    private float staminaRegenRate = 1f; // Tốc độ hồi Stamina mỗi giây
    private float timeSinceLastStaminaRegen; // Thời gian kể từ lần hồi Stamina cuối cùng
    private bool canJump = true; // Cho phép nhảy hay không
    private void Start()
    {
        currentHp = maxHp;
        currentStamina = maxStamina;
        currentDamage = baseDamage;
        currentArmor = baseArmor;
        UpdateUI();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Giảm Stamina khi kích hoạt animation Jump (ví dụ)
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            DecreaseStamina(20); // Giảm 20 Stamina (số tùy chọn)
            
        }
        if(currentStamina <= 0)
        {
            canJump = false;
        }
        // Hồi Stamina theo thời gian
        if (currentStamina < maxStamina)
        {
            timeSinceLastStaminaRegen += Time.deltaTime;
            if (timeSinceLastStaminaRegen >= 0.1f)
            {
                RegenerateStamina();
                timeSinceLastStaminaRegen = 0f;
            }
        }

        // Kích hoạt animation IsDeathing khi HP về 0
        if (currentHp <= 0)
        {
            anim.SetBool("IsDeathing", true);
        }
    }

    // Hàm giảm Stamina
    private void DecreaseStamina(int amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateUI();
    }

    // Hàm hồi Stamina
    private void RegenerateStamina()
    {
        currentStamina += Mathf.RoundToInt(staminaRegenRate);
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateUI();
    }

    // Hàm cập nhật giao diện
    public void UpdateUI()
    {
        hpSlider.value = (float)currentHp / maxHp;
        staminaSlider.value = (float)currentStamina / maxStamina;
        hpText.text = $"{currentHp}/{maxHp}";
        staminaText.text = $"{currentStamina}/{maxStamina}";
        damageText.text = $"Damage: {currentDamage + kiemDamage + cungDamage}"; // Cộng dồn với Damage của Player
        armorText.text = $"Armor: {currentArmor}"; // Có thể cộng dồn với các vật phẩm khác
        moneyCount.text = $"{baseMoney}";
    }
}