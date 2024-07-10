using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 13f;
    public float dodgeSpeed = 4f;
    public float dodgeDirection = 0.5f;
    private float dodgeTime;
    private bool isDodging = false;
    public float groundDecay = 0.2f;

    public Rigidbody2D rb;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Animator anim;
    private bool grounded;
    private float xInput;
    public AudioSource audioSource;
    public AudioClip moveSound; // Thêm biến AudioClip để lưu trữ âm thanh di chuyển

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
        JumpAndCheckGround();
        Dodge();
    }

    void Move()
    {
        xInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        if (xInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(xInput), 1, 1);
            anim.SetBool("IsRunning", true); // Đặt trạng thái animation di chuyển thành true
            if (!audioSource.isPlaying) // Kiểm tra xem âm thanh có đang phát hay không
            {
                audioSource.PlayOneShot(moveSound); // Phát âm thanh di chuyển
            }
        }
        else
        {
            anim.SetBool("IsRunning", false); // Đặt trạng thái animation di chuyển thành false
        }
    }


    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dodgeTime <= 0) // Khi nhấn phím Shift và thời gian dodge đã hết
        {
            anim.SetBool("IsRolling", true); // Kích hoạt animation dodge
            moveSpeed += dodgeSpeed; // Tăng tốc độ di chuyển
            dodgeTime = dodgeDirection; // Đặt lại thời gian dodge
            isDodging = true; // Đánh dấu trạng thái đang dodge
        }

        if (dodgeTime <= 0 && isDodging == true) // Khi thời gian dodge hết và đang trong trạng thái dodge
        {
            anim.SetBool("IsRolling", false); // Tắt animation dodge
            moveSpeed -= dodgeSpeed; // Trả lại tốc độ di chuyển ban đầu
            isDodging = false; // Đặt lại trạng thái dodge
        }
        else
        {
            dodgeTime -= Time.deltaTime; // Giảm thời gian dodge theo thời gian thực
        }
    }
    public void TakeDamage(int damage)
    {
        if (isDodging) // Nếu đang dodge thì không nhận damage
        {
            Debug.Log("Player dodged the attack!");
            return;
        }

        // Giảm máu hoặc xử lý khác khi nhận damage
        Debug.Log($"Player took {damage} damage!");
        // Implement health reduction or death logic here
    }
    void JumpAndCheckGround()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) // Khi nhấn phím Space và đang trên mặt đất
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed); // Cập nhật vận tốc để nhảy lên
            anim.SetBool("IsJumping", true); // Kích hoạt animation nhảy
            grounded = false; // Đánh dấu rằng đang trong quá trình nhảy
        }
        else
        {
            anim.SetBool("IsJumping", false); // Tắt animation nhảy
        }

        RaycastHit2D raycastHit2D = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, Mathf.Infinity);
        Debug.DrawRay(groundCheck.transform.position, Vector2.down * raycastHit2D.distance, Color.red);
        if (raycastHit2D.collider != null && raycastHit2D.collider.CompareTag("Platform")) // Kiểm tra va chạm với tag "Ground"
        {
            grounded = true; // Đang trên mặt đất
        }
        else
        {
            grounded = false; // Không đang trên mặt đất
        }
    }

}
