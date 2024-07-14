using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 13f;
    [SerializeField] private float attackCooldown = 0.5f; // Thời gian cooldown giữa các lần tấn công
    [SerializeField] private float currentCooldown = 0f; // Thời gian cooldown hiện tại

    public float dodgeSpeed = 4f;
    public float dodgeDirection = 0.5f;
    private float dodgeTime;
    private bool isDodging = false;
    private bool isAttacking = false; // Biến để kiểm tra xem người chơi đang thực hiện cuộc tấn công hay không
    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D swordCollider;
    private float xInput;
    public AudioSource audioSource;
    public AudioClip moveSound;
    public AudioClip jumpSound;
    public AudioClip swordSound;
    public AudioClip skill1Sound;
    public AudioClip skill2Sound;

    public BoxCollider2D blockCollider; // Biến để tham chiếu đến BoxCollider2D của chặn đòn

    private bool isGrounded; // Kiểm tra xem người chơi có đang đứng trên mặt đất không

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        JumpAndCheckGround();
        Dodge();
    }

    public void Move()
    {
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        if (xInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(xInput), 1, 1);
            anim.SetBool("IsRunning", true);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(moveSound);
            }
        }
        else
        {
            anim.SetBool("IsRunning", false); // Khi đứng yên, animation di chuyển là false
        }
    }

    public void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDodging)
        {
            // Người chơi nhấn phím Shift và không đang trong trạng thái lộn

            // Kích hoạt IsDodging (nếu bạn đã đặt trigger này trong animator)
            anim.SetBool("IsRolling", true);

            // Thay đổi vận tốc theo trục X để thực hiện lộn
            rb.velocity = new Vector2(dodgeDirection * dodgeSpeed, rb.velocity.y);

            // Đánh dấu là đang lộn
            isDodging = true;
        }
        else
        {
            // Người chơi không nhấn phím Shift hoặc đang trong trạng thái lộn
            anim.SetBool("IsRolling",false); // Đặt lại IsDodging
            isDodging = false;
        }
    }
    public void Attack()
    {
        if (Input.GetKey(KeyCode.F) && !isAttacking && currentCooldown <= 0f)
        {
            // Người chơi nhấn phím F và không đang trong trạng thái tấn công
            // và đã qua thời gian cooldown

            // Kích hoạt trigger IsAttacking
            anim.SetTrigger("IsAttacking");

            // Kích hoạt BoxCollider2D của thanh kiếm
            swordCollider.enabled = true;

            // Gán thời gian cooldown
            currentCooldown = attackCooldown;
            isAttacking = true;

        }
        else
        {
            // Người chơi không nhấn phím F hoặc đang trong trạng thái tấn công
            // hoặc chưa qua thời gian cooldown
            anim.ResetTrigger("IsAttacking"); // Đặt lại trigger IsAttacking
            isAttacking = false;
            // Vô hiệu hóa BoxCollider2D của thanh kiếm
            swordCollider.enabled = false;
            // Kích hoạt trigger IsStaying
            anim.SetTrigger("IsStaying");
        }

        // Giảm thời gian cooldown
        currentCooldown -= Time.deltaTime;
    }
    public void Block()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            // Người chơi giữ phím Q

            // Kích hoạt trigger IsBlocking (nếu bạn đã đặt trigger này trong animator)
            anim.SetBool("IsBlocking", true);

            // Kích hoạt BoxCollider2D của chặn đòn
            blockCollider.enabled = true;
        }
        else
        {
            // Người chơi thả phím Q ra

            // Vô hiệu hóa BoxCollider2D của chặn đòn
            blockCollider.enabled = false;

            // Kết thúc hành động chặn đòn
            anim.SetBool("IsBlocking", false);
        }
    }
    public void JumpAndCheckGround()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            anim.SetBool("IsJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Khi va chạm với mặt đất, đánh dấu là đang đứng trên mặt đất
            anim.SetBool("IsJumping", false); // Khi chạm đất, animation nhảy là false
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Khi rời khỏi mặt đất, đánh dấu không đứng trên mặt đất
        }
    }
}
