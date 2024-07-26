using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class StatusPlayer : MonoBehaviour
{
    public Slider hpSlider; // Thanh Slider cho HP
    public Slider staminaSlider; // Thanh Slider cho Stamina
    public TextMeshProUGUI damageText; // TextMeshPro cho Damage
    public TextMeshProUGUI armorText; // TextMeshPro cho Armor
    public TextMeshProUGUI hpText; // TextMeshPro cho hiển thị HP
    public TextMeshProUGUI staminaText; // TextMeshPro cho hiển thị Stamina
    public TextMeshProUGUI moneyCount; //TextMeshPro hiển thị Money của người chơi.
    public TextMeshProUGUI coinstext;
    public TextMeshProUGUI diamondGText;
    public TextMeshProUGUI diamondPText;
    public TextMeshProUGUI bloodText;
    public Animator anim;
    public GameObject gameOver;
    public GameObject bloodButton;
    public int maxHp = 100; // Máu tối đa
    public int maxStamina = 100; // Stamina tối đa
    public int currentMoney;
    public int currentHp; // Máu hiện tại
    public int currentStamina; // Stamina hiện tại
    public int currentDamage; // Damage hiện tại
    public int currentArmor; // Armor hiện tại
    [SerializeField] private int baseDamage = 100; // Damage cơ bản
    private int baseArmor = 100; // Armor cơ bản
    public int baseMoney = 10000; // Money cơ bản
    private float staminaRegenRate = 1f; // Tốc độ hồi Stamina mỗi giây
    private float timeSinceLastStaminaRegen; // Thời gian kể từ lần hồi Stamina cuối cùng
    public float timeSinceLastHeal;
    private float healInterval = 2f; // Hồi máu sau mỗi 2 giây
    private bool canJump = true; // Cho phép nhảy hay không
    public int diamondGreen = 0;
    public int diamondPurple = 0;
    public int coins = 0;
    public int bloods = 0;
    public int currentCoins;
    public int currentdiamondG;
    public int currentdiamondP;
    public int currentBloods;
    public ParticleSystem playerBloodEffect;
    private void Start()
    {
        currentHp = maxHp;
        currentStamina = maxStamina;
        currentDamage = baseDamage;
        currentArmor = baseArmor;
        currentMoney = baseMoney;
        currentCoins = coins;
        currentdiamondG = diamondGreen;
        currentdiamondP = diamondPurple;
        currentBloods = bloods;
        UpdateUI();
        anim = GetComponent<Animator>();
        playerBloodEffect.Stop();
    }

    private void Update()
    {
        if(currentBloods > 0)
        {
            bloodButton.SetActive(true);
        }
        else
        {
            bloodButton.SetActive(false);
        }
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
        if (currentStamina < (maxStamina))
        {
            timeSinceLastStaminaRegen += Time.deltaTime;
            if (timeSinceLastStaminaRegen >= 0.1f)
            {
                RegenerateStamina(1);
                timeSinceLastStaminaRegen = 0f;
            }
        }
        // Hồi máu theo thời gian thực
        if(currentHp < (maxHp))
        {
            timeSinceLastHeal += Time.deltaTime;
            if (timeSinceLastHeal >= healInterval)
            {
                Heal(1); // Hồi 1 máu
                timeSinceLastHeal = 0f;
            }
        }
     
        // Kích hoạt animation IsDeathing khi HP về 0
        if (currentHp <= 0)
        {
            anim.SetBool("IsDeathing", true);
            gameObject.SetActive(false);
            gameOver.SetActive(true);
            // Tạm dừng game
            Time.timeScale = 0f;
        }
        else
        {
            gameObject.SetActive(true);
            gameOver.SetActive(false);
        }
        
    }

    // Hàm giảm Stamina
    public void DecreaseStamina(int amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateUI();
    }
    // Hàm hồi máu
    public void Heal(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        UpdateUI();
    }
    // Hàm cộng máu
    public void IncreaseHealth(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, (maxHp + 50000000));
        UpdateUI();
    }
    // Hàm hồi Stamina
    public void RegenerateStamina(int amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateUI();
    }
    
    // Hàm cập nhật giao diện
    public void UpdateUI()
    {
        hpSlider.value = (float)currentHp /(maxHp);
        staminaSlider.value = (float)currentStamina  / maxStamina;
        hpText.text = $"{currentHp}/{maxHp} ";
        staminaText.text = $"{currentStamina}/{maxStamina }";
        damageText.text = $"Damage: {currentDamage }"; // Cộng dồn với Damage của Player
        armorText.text = $"Armor: {currentArmor}"; // Có thể cộng dồn với các vật phẩm khác
        moneyCount.text = $"{currentMoney}";
        coinstext.text = $"{currentCoins}";
        diamondGText.text = $"{currentdiamondG}";
        diamondPText.text = $"{currentdiamondP}";
        bloodText.text = $"{currentBloods}";
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KnifeEnemy"))
        {
            currentHp -= 50;
            playerBloodEffect.Play();
            UpdateUI();
        }
        if (other.gameObject.CompareTag("DiamondGr"))
        {
            currentdiamondG += 1;
            UpdateUI();
        }
        if (other.gameObject.CompareTag("DiamondPurle"))
        {
            currentdiamondP += 1;
            UpdateUI();
        }
        if (other.gameObject.CompareTag("DiamondGr"))
        {
            currentCoins += 1;
            UpdateUI();
        }
        if (other.gameObject.CompareTag("Dead"))
        {
            currentHp -= 100000000;
            UpdateUI();
        }
        if (other.gameObject.CompareTag("BossSkill1") || other.gameObject.CompareTag("BossSkill2"))
        {
            if (!inSKillBoss)
            {
                StartCoroutine(ApplyContinuousDamage());
            }
        }
    }
    private bool inSKillBoss = false;
    private IEnumerator ApplyContinuousDamage()
    {
        inSKillBoss = true;
        while (currentHp > 0)
        {
            currentHp -= 200;
            UpdateUI();
            yield return new WaitForSeconds(0.5f);
        }
        inSKillBoss = false;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BossSkill1") || other.gameObject.CompareTag("BossSkill2"))
        {
            inSKillBoss = false;
        }
    }
    /*public void CollectItem()
    {
        // Lấy giá trị từ StatusPlayer và gán vào PlayerData
        PlayerData.Instance.playerHp = currentHp;
        PlayerData.Instance.playerStamina = currentStamina;
        PlayerData.Instance.playerDamage = currentDamage;
        PlayerData.Instance.playerArmor = currentArmor;
        PlayerData.Instance.playerMoney = currentMoney;
        // ... (gán các biến khác)
    }*/
}