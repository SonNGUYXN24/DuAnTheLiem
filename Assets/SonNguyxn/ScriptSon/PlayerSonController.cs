using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSonController : MonoBehaviour
{
    public float runSpeed = 5f; // Tốc độ chạy
    public float jumpForce = 10f; // Lực nhảy
    public LayerMask platformLayer; // Layer "Platform" để xác định va chạm

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Xử lý đầu vào cho việc chạy
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * runSpeed, rb.velocity.y);

        // Xử lý đầu vào cho việc nhảy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else
            {
                // Khi Player đang chạy và nhảy, kích hoạt animation Nhảy
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            // Khi Player dừng lại, kích hoạt animation Đứng yên
            animator.SetBool("IsRunning", move != 0);
            animator.SetBool("IsJumping", false);
        }

        // Xử lý hướng quay mặt
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Quay mặt sang phải
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Quay mặt sang trái
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            // Khi Player đứng yên và nhảy, kích hoạt animation Nhảy
            animator.SetBool("IsJumping", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
