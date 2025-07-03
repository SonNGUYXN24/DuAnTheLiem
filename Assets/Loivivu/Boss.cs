using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    public float detectionRangeAttack = 2.5f;  // Phạm vi phát hiện người chơi
    public float detectionRange = 15f;  // Phạm vi phát hiện người chơi
    private bool inSkillCooldown = false; // Biến kiểm tra xem đang trong thời gian cooldown của skill hay không
    private float stopRange = 0.5f;
    public StatusPlayer statusPlayer;
    public Transform Player;//follow player
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
    public ParticleSystem fireBallHit;
    public ParticleSystem bloodEffect;
    public ParticleSystem swordEffect;
    public ParticleSystem skill1;
    public ParticleSystem skill2;
    private bool isDead;
    public BoxCollider2D skill1Trigger;
    public BoxCollider2D skill2Trigger;
    public TextMeshProUGUI hpBossText;
    public GameObject lastSkillText;
    public AudioSource bossAudio;
    public AudioClip bossSkill1;
    public AudioClip bossSkill2;
    public CameraController cameraController;
    public GameObject lastSkillController;
    public ParticleSystem lastSkillEffect;
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
        hpBossText.text = $"{currentHPEnemy}/{maxHP}";
    }
    // Update is called once per frame
    void Update()
    {
        TimeAttack();
        followPlayer();
        Skill();
        if(currentHPEnemy <=0)
        {
            bossAudio.Stop();
            cameraController.hpBossCanvas.SetActive(false);
        }
        skillLast();
    }

    public void skillLast()
    {
        if(currentHPEnemy <=30000 && currentHPEnemy > 0)
        {
            lastSkillText.SetActive(true);
            lastSkillController.SetActive(true);
        }
    }

    void TimeAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer < detectionRangeAttack)
        {
            timeAttack -= Time.deltaTime;

                if (timeAttack <= 0)
                {
                    //animation tấn công
                    animator.SetTrigger("Attack");
                    var oneSkill = Instantiate(hitbox, Knifedamage.position, Quaternion.identity);
                    Destroy(oneSkill, 0.1f);
                    timeAttack = TimeAttackRate;
                }
        }
        else
        {
            animator.SetBool("isidiel", true);
        }
        if(currentHPEnemy <=0)
        {
            cameraController.bossBattleMusic.Stop();
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

                Vector2 moveDirection = new Vector2(Player.position.x - transform.position.x, 0f).normalized;
                rb.linearVelocity = moveDirection * 3f;// Tốc độ di chuyển

                animator.SetBool("ismove", true);


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
                animator.SetBool("ismove", false);
            }

        }
        else
        {
         
            animator.SetBool("ismove", false);
        }
    }
    public void Skill()
    {
        if (!inSkillCooldown)
        {
            if (currentHPEnemy <= 70000 && currentHPEnemy > 0)
            {
                skill1Trigger.enabled = true;
                bossAudio.PlayOneShot(bossSkill1);
                StartCoroutine(ActivateSkill(skill1, 5f)); // Kích hoạt Skill1 trong 5 giây
            }
            if (currentHPEnemy <= 30000 && currentHPEnemy > 0)
            {
                skill1Trigger.enabled = false;
                skill2Trigger.enabled = true;
                bossAudio.PlayOneShot(bossSkill2);
                StartCoroutine(ActivateSkill(skill2, 5f)); // Kích hoạt Skill2 trong 5 giây
            }
            inSkillCooldown = true;
            StartCoroutine(SkillCooldown());
        }
    }

    private IEnumerator ActivateSkill(ParticleSystem skill, float duration)
    {
        skill.Play();
        yield return new WaitForSeconds(duration);
        skill.Stop();
        skill1Trigger.enabled = false; // Tắt trigger sau khi kết thúc skill
        skill2Trigger.enabled = false; // Tắt trigger sau khi kết thúc skill
    }

    private IEnumerator SkillCooldown()
    {
        yield return new WaitForSeconds(0.75f);
        inSkillCooldown = false;
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
            currentHPEnemy -= 100;
            fireBallHit.Play();
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
            detectionRange -= 15f;
            lastSkillEffect.Play();
            bossAudio.Stop();
            StartCoroutine(WaitLastSkill());
            currentHPEnemy -= 1000000;
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
        lastSkillText.SetActive(false);
        
        yield return new WaitForSeconds(1.5f);
        
        lastSkillController.SetActive(false);
        portalEnd.SetActive(true);
        Destroy(gameObject);
    }
}
