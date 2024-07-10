/*using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float healthLife = 100f; // Tổng lượng máu của nhân vật
    public float currentHealth; // Lượng máu hiện tại của nhân vật
    private bool dead; // Trạng thái của nhân vật (sống hay chết)
    public bool isVunerable = true;

    [Header("iFrames")]
    [SerializeField] private float iFramesDur; // Thời gian của iFrames (trạng thái bất tử)
    [SerializeField] private int numberOfflashes; // Số lần nhấp nháy khi ở trạng thái bất tử
    private SpriteRenderer spriteRend; // Thành phần SpriteRenderer để thay đổi màu sắc nhân vật


	public AudioClip[] audioClips;
	private Dictionary<string, AudioClip> soundClips = new Dictionary<string, AudioClip>();

    private PlayerController player;

	private void Awake()
    {
        currentHealth = healthLife; // Đặt lượng máu hiện tại bằng lượng máu tối đa khi bắt đầu
       
        spriteRend = GetComponent<SpriteRenderer>(); // Lấy thành phần SpriteRenderer của nhân vật

        player = GetComponent<PlayerController>();
		player.OnMaxHealthChange += Player_OnMaxHealthChange;
    }

	private void Player_OnMaxHealthChange(object sender, PlayerController.OnMaxHealthChangeEventArgs e)
	{
		healthLife = e.Health;
        currentHealth = healthLife;
        FindObjectOfType<UI_HealthBar>().UpdateHealthbar(currentHealth);
	}

	private void Start()
	{
		soundClips.Add("hit", audioClips[0]);
		soundClips.Add("fail", audioClips[1]);
	}


	public void PlaySound(string soundName)
	{
		if (soundClips.ContainsKey(soundName))
		{
			AudioSource.PlayClipAtPoint(soundClips[soundName], transform.position);
		}
	}

    public void RecoverHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, healthLife);
        FindObjectOfType<UI_HealthBar>().UpdateHealthbar(currentHealth);
    }

	public void TakeDamage(float damage)
    {
        PlaySound("hit");
		
		currentHealth = Mathf.Clamp(currentHealth - damage, 0, healthLife); // Giảm lượng máu của nhân vật khi bị tấn công
		FindObjectOfType<UI_HealthBar>().UpdateHealthbar(currentHealth);
		if (currentHealth > 0)
        {
            // Khi nhân vật còn máu
            // Kích hoạt trạng thái bất tử
            StartCoroutine(Ivunerability());
        }
        else
        {
            // Khi nhân vật chết
            if (!dead) // Nếu nhân vật chết, tắt điều khiển
            {
                GetComponent<PlayerController>().enabled = false; // Vô hiệu hóa script điều khiển nhân vật
                dead = true; // Đặt trạng thái của nhân vật là đã chết
                // Gọi hàm xử lý khi nhân vật chết
                StartCoroutine(HandleDeadth());
            }
        }
    }

    private IEnumerator HandleDeadth()
    {
        // Thêm hiệu ứng hoặc âm thanh nếu cần
        // Ví dụ: hiệu ứng hủy hoặc âm thanh chết

        // Chờ một khoảng thời gian ngắn để cho phép các hiệu ứng xảy ra
        PlaySound("fail");
        FindObjectOfType<UI_Manager>().GameOver();
        yield return new WaitForSeconds(0f);

        // Hủy tag của đối tượng Player
        gameObject.tag = "Player";
        var pc = GetComponent<PlayerController>();
        // Hủy đối tượng
        Destroy(pc);// Hủy đối tượng nhân vật
    }

    private IEnumerator Ivunerability()
    {
        isVunerable = false;
        Physics2D.IgnoreLayerCollision(6, 9, true); // Bỏ qua va chạm giữa các lớp nhân vật và kẻ thù
        for (int i = 0; i < numberOfflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Thay đổi màu nhân vật để hiển thị trạng thái bị thương
            yield return new WaitForSeconds(iFramesDur / (numberOfflashes * 2)); // Chờ một khoảng thời gian
            spriteRend.color = Color.white; // Đặt lại màu trắng cho nhân vật
            yield return new WaitForSeconds(iFramesDur / (numberOfflashes * 2)); // Chờ một khoảng thời gian
        }
        Physics2D.IgnoreLayerCollision(6, 9, false); // Bật lại va chạm giữa các lớp nhân vật và kẻ thù
        isVunerable = true; 
    }
}
*/