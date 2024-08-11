using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class DragonBoss : MonoBehaviour
{
    public float detectionRangeAttack = 2.5f;  // Phạm vi phát hiện người chơi
    public float detectionRange = 10f;  // Phạm vi phát hiện người chơi
    public float fireBallRange = 20f;  // Phạm vi bắn FireBall
    public float fireBallSpeed = 5f;  // Tốc độ của FireBall
    private bool inSkillCooldown = false; // Biến kiểm tra xem đang trong thời gian cooldown của skill hay không
    private float stopRange = 0.5f;
    public StatusPlayer statusPlayer;
    public Transform Player;//follow player
    public Transform Player2; // xác định vị trí của Player để bắn FireBall
    private float TimeAttackRate = 2f;
    private float timeAttack;
    public GameObject portalEnd;
    private bool right;
    public Slider healthSlider;//slider hp boss
    public int health;
    public int currentHPEnemy;
    public int maxHP;
    Animator animator;
    Rigidbody2D rb;
    public Transform Knifedamage;
    public GameObject hitbox;
    public ParticleSystem deadEffect;
    public ParticleSystem bloodEffect;
    public ParticleSystem swordEffect;
    private bool isDead;
    public TextMeshProUGUI hpBossText;
    public AudioSource dragonBossAudio;
    public CameraController cameraController;
    public GameObject fireBallPrefab; // Prefab của viên đạn FireBall
    public Transform firePoint; // Vị trí bắn FireBall
    private float fireBallCooldown = 2f; // Thời gian hồi chiêu của FireBall
    private float fireBallTimer;
    public GameObject fireWall;
    public AudioClip fireWallSounds;
    public CinemachineVirtualCamera virtualCamera; // Thêm biến cho Virtual Camera
    public float originalCameraSize;
    private bool hasTriggeredFireWall = false; // Biến kiểm tra xem đã kích hoạt fireWall chưa
    public Transform originalCameraFollow; // Lưu trữ đối tượng theo dõi ban đầu của camera
    public float originalScreenX;
    public float originalScreenY;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHPEnemy = health;
        UpdateHP();
        fireBallTimer = fireBallCooldown;
        originalCameraSize = virtualCamera.m_Lens.OrthographicSize; // Lưu kích thước ban đầu của camera
        originalCameraFollow = virtualCamera.Follow; // Lưu đối tượng theo dõi ban đầu của camera
        originalScreenX = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX;
        originalScreenY = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
    }

    public void UpdateHP()
    {
        healthSlider.value = (float)currentHPEnemy / maxHP;
        hpBossText.text = $"{currentHPEnemy}/{maxHP}";
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
        if (currentHPEnemy <= 100000 && !hasTriggeredFireWall)
        {
            StartCoroutine(HandleFireWall());
            FlameAttack();
        }
        else if (currentHPEnemy > 100000 && currentHPEnemy <= 200000)
        {
            NormalAttack();
        }
        if (currentHPEnemy <= 0)
        {
            dragonBossAudio.Stop();
            cameraController.hpBossCanvas.SetActive(false);
            fireWall.SetActive(false); // Tắt fireWall khi DragonBoss chết
        }
        if (currentHPEnemy <= 100000)
        {
            FlameAttack();
        }
    }

    void NormalAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer < detectionRangeAttack)
        {
            timeAttack -= Time.deltaTime;

            if (timeAttack <= 0)
            {
                // animation tấn công
                animator.SetTrigger("IsNormalAttack");
                var oneSkill = Instantiate(hitbox, Knifedamage.position, Quaternion.identity);
                Destroy(oneSkill, 0.1f);
                timeAttack = TimeAttackRate;
            }
        }
        else
        {
            animator.SetBool("IsIdiel", true);
        }
        if (currentHPEnemy <= 0)
        {
            cameraController.bossBattleMusic.Stop();
        }
    }

    private void followPlayer() // thấy player thì chạy theo
    {
        // Tính khoảng cách giữa quái vật và người chơi
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        // Nếu khoảng cách nhỏ hơn phạm vi phát hiện, quái vật sẽ đuổi theo người chơi
        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > stopRange)
            {
                Vector2 moveDirection = new Vector2(Player.position.x - transform.position.x, 0f).normalized;
                rb.velocity = moveDirection * 3f; // Tốc độ di chuyển

                animator.SetBool("IsRunning", true);

                // xoay mặt
                if (right && moveDirection.x < 0 || !right && moveDirection.x > 0)
                {
                    right = !right;
                    Vector3 kichThuoc = transform.localScale;
                    kichThuoc.x = kichThuoc.x * -1;
                    transform.localScale = kichThuoc;
                }
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void FlameAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer <= fireBallRange)
        {
            fireBallTimer -= Time.deltaTime;

            if (fireBallTimer <= 0)
            {
                // animation bắn FireBall
                animator.SetTrigger("IsFlameAttack");
                var fireBall = Instantiate(fireBallPrefab, firePoint.position, Quaternion.identity);

                // Xác định hướng bắn của FireBall
                Vector2 direction = (Player.position - firePoint.position).normalized;
                fireBall.GetComponent<Rigidbody2D>().velocity = direction * fireBallSpeed;

                // Xoay hướng của FireBall theo hướng của DragonBoss
                if (direction.x < 0)
                {
                    fireBall.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    fireBall.transform.localScale = new Vector3(1, 1, 1);
                }

                fireBallTimer = fireBallCooldown;
            }
        }
        else
        {
            animator.SetBool("IsIdiel2", true);
        }
    }

    private IEnumerator HandleFireWall()
    {
        hasTriggeredFireWall = true;

        // Chuyển camera sang DragonBoss
        virtualCamera.Follow = transform;
        yield return new WaitForSeconds(1.5f);

        // Tạm dừng hoạt động của Player và DragonBoss
        Player.GetComponent<PlayerController>().enabled = false;
        rb.velocity = Vector2.zero; // Đóng băng DragonBoss
        animator.enabled = false; // Tắt animator của DragonBoss

        // Phóng to camera và di chuyển vị trí
        float elapsedTime = 0f;
        float targetSize = 15f;
        var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        while (elapsedTime < 2f)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(originalCameraSize, targetSize, elapsedTime / 2f);
            framingTransposer.m_ScreenX = Mathf.Lerp(originalScreenX, 0.45f, elapsedTime / 1f);
            framingTransposer.m_ScreenY = Mathf.Lerp(originalScreenY, 0.8f, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = targetSize;
        framingTransposer.m_ScreenX = 0.45f;
        framingTransposer.m_ScreenY = 0.8f;

        // Kích hoạt fireWall
        fireWall.SetActive(true);
        yield return new WaitForSeconds(2f);

        // Thu nhỏ camera trở lại và di chuyển vị trí về ban đầu
        elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(targetSize, originalCameraSize, elapsedTime / 2f);
            framingTransposer.m_ScreenX = Mathf.Lerp(0.45f, originalScreenX, elapsedTime / 1f);
            framingTransposer.m_ScreenY = Mathf.Lerp(0.8f, originalScreenY, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = originalCameraSize;
        framingTransposer.m_ScreenX = originalScreenX;
        framingTransposer.m_ScreenY = originalScreenY;

        // Trả lại camera cho Player
        virtualCamera.Follow = originalCameraFollow;

        // Kích hoạt lại hoạt động của Player và DragonBoss
        Player.GetComponent<PlayerController>().enabled = true;
        animator.enabled = true; // Bật lại animator của DragonBoss
        this.enabled = true;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            currentHPEnemy -= statusPlayer.currentDamage;
            swordEffect.Play();
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
        if (collision.gameObject.CompareTag("FireBall"))
        {
            currentHPEnemy -= 500;
            UpdateHP();

            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
        if (collision.gameObject.CompareTag("Ultimate"))
        {
            currentHPEnemy -= 800;
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            currentHPEnemy -= 1000;
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
        if (collision.gameObject.CompareTag("LastSkill"))
        {
            StartCoroutine(WaitLastSkill());
            currentHPEnemy -= 10000;
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
        if (collision.gameObject.CompareTag("DarkBall"))
        {
            currentHPEnemy -= 200; // Số lượng sát thương của DarkBall
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
        if (collision.gameObject.CompareTag("DarkBallTrigger"))
        {
            currentHPEnemy -= 10000; // Số lượng sát thương của DarkBall
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
    }
    private IEnumerator WaitLastSkill()
    {
        yield return new WaitForSeconds(2f);
        // Thêm các hành động khác nếu cần sau khi chờ 2.5 giây
    }

    private IEnumerator DeadEffect()
    {
        isDead = true;
        deadEffect.Play();
        bloodEffect.Play();
        yield return new WaitForSeconds(1f);
        portalEnd.SetActive(true);
        Destroy(gameObject);
    }
}
