using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyC : MonoBehaviour
{
    public float detectionRangeAttack = 2.5f;  // Phạm vi phát hiện người chơi
    public float detectionRange = 7f;  // Phạm vi phát hiện người chơi
    private float stopRange=0.5f;
    public StatusPlayer statusPlayer;
    public Transform Player;//follow player
    public ParticleSystem fireBallHit;
    public ParticleSystem bloodEffect;
    public ParticleSystem swordEffect;
    private float TimeAttackRate = 2f;
    private float timeAttack;
    private bool isDead = false; // Thêm biến để kiểm tra xem quái vật đã chết chưa
    private bool right;

    public Slider healthSlider;//slider hp boss
    public int health;
    public int currentHPEnemy;
    public int maxHP;
    Animator animator;
    Rigidbody2D rb;

    public Transform Knifedamage;
    public GameObject hitbox;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHPEnemy = health;
        UpdateHP();
    }
    public void UpdateHP()
    {
        healthSlider.value = (float)currentHPEnemy / maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        TimeAttack();
        followPlayer();
        statusPlayer.UpdateUI();
    }
    void TimeAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer < detectionRangeAttack)
        {

            int attackType = Random.Range(0, 2);  // Random số nguyên từ 0 tới 2 (0 hoặc 2)

            timeAttack -= Time.deltaTime;

            if (attackType == 0)
            {

                if (timeAttack <= 0)
                {
                    //animation tấn công
                    animator.SetTrigger("Attack1");
                    var oneSkill = Instantiate(hitbox, Knifedamage.position, Quaternion.identity);
                    Destroy(oneSkill, 0.2f);
                    timeAttack = TimeAttackRate;
                }
            }if (attackType == 1)
            {
                if (timeAttack <= 0)
                {
                    animator.SetTrigger("Attack2");
                    var oneSkill = Instantiate(hitbox, Knifedamage.position, Quaternion.identity);
                    Destroy(oneSkill, 0.2f);
                    timeAttack = TimeAttackRate;
                }
            }
        }
    }
    private void followPlayer()//thấy player thì chạy theo
    {
        // Tính khoảng cách giữa quái vật và người chơi
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        // Nếu khoảng cách nhỏ hơn phạm vi phát hiện, quái vật sẽ đuổi theo người chơi
        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > stopRange)
            {
                Vector2 moveDirection = (Player.position - transform.position).normalized;
                rb.velocity = moveDirection * 3f;// Tốc độ di chuyển

                animator.SetBool("isMoving", true);


                //xoay mặt
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
                animator.SetBool("isMoving", false);
            }

        }
        else
        {
            animator.SetBool("isMoving", false);
        }
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
                StartCoroutine(PlayBloodEffectAndDestroy());
            }
        }
        if (collision.gameObject.CompareTag("FireBall"))
        {
            currentHPEnemy -= 100;
            fireBallHit.Play();
            UpdateHP();

            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(PlayBloodEffectAndDestroy());
            }
        }
        if (collision.gameObject.CompareTag("Ultimate"))
        {
            currentHPEnemy -= 800;
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(PlayBloodEffectAndDestroy());
            }
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            currentHPEnemy -= 500;
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(PlayBloodEffectAndDestroy());
            }
        }
    }
    private IEnumerator PlayBloodEffectAndDestroy()
    {
        isDead = true; // Đánh dấu quái vật đã chết

        // Phát hiệu ứng bloodEffect
        bloodEffect.Play();

        // Đợi một khoảng thời gian (ví dụ: 0.1 giây)
        yield return new WaitForSeconds(0.3f);

        // Hủy đối tượng quái vật
        Destroy(gameObject);
    }
}
