using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour
{
    public float detectionRangeAttack = 2.5f;  // Phạm vi phát hiện người chơi
    public float detectionRange = 7f;  // Phạm vi phát hiện người chơi
    private float stopRange=0.5f;
    public Transform Player;//follow player

    private float TimeAttackRate = 2f;
    private float timeAttack;

    private bool right;

    Animator animator;
    Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
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
                animator.SetTrigger("Attack1");
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
                rb.velocity = moveDirection * 1.5f;// Tốc độ di chuyển

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
}
