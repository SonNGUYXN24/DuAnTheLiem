using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 13f;
    public float dodgeSpeed = 4f;
    public float dodgeDirection = 0.5f;
    private float dodgeTime;
    private bool isDodging = false;

    public Rigidbody2D rb;
    public Animator anim;
    private float xInput;
    public AudioSource audioSource;
    public AudioClip moveSound;

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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("IsRolling", true);
            dodgeTime = dodgeDirection;
            isDodging = true;
        }
        else
        {
            anim.SetBool("IsRolling", false);
            isDodging= false;
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
