using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float detectionRangeAttack = 2.5f;  // Phạm vi phát hiện người chơi
    public float detectionRange = 15f;  // Phạm vi phát hiện người chơi
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
    private bool isDead;

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
                rb.velocity = moveDirection * 5f;// Tốc độ di chuyển

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
            currentHPEnemy -= 500;
            UpdateHP();
            if (currentHPEnemy <= 0 && !isDead)
            {
                StartCoroutine(DeadEffect());
            }
        }
    }
    private IEnumerator  DeadEffect()
    {
        isDead = true;
        deadEffect.Play();
        bloodEffect.Play();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        portalEnd.SetActive(true);
    }    
}
