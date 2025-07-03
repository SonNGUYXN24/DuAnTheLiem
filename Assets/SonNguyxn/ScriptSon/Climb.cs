using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public Rigidbody2D rb;
    public float climbSpeed = 3f;
    private bool isClimbing = false; // Kiểm tra Player có đang trèo thang hay không
    private bool nearLadder = false; // Kiểm tra Player có gần cầu thang hay không
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Kiểm tra nếu người chơi đang ở gần cầu thang và nhấn phím leo lên hoặc xuống
        if (nearLadder)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                isClimbing = true;
            }

            if (isClimbing)
            {
                rb.gravityScale = 0f;
                anim.SetBool("isClimbing", true);
            }
            else
            {
                rb.gravityScale = 4f;
                anim.SetBool("isClimbing", false);
            }
        }
        else
        {
            isClimbing = false;
            rb.gravityScale = 4f;
            anim.SetBool("isClimbing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            nearLadder = true; // Đánh dấu Player đang gần cầu thang
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            nearLadder = false; // Đánh dấu Player không còn gần cầu thang
            isClimbing = false;
            rb.gravityScale = 4f;
            anim.SetBool("isClimbing", false);
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            float climbInput = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                climbInput = 1f; // Leo lên
            }
            else if (Input.GetKey(KeyCode.S))
            {
                climbInput = -1f; // Leo xuống
            }
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbInput * climbSpeed);
        }
        else if (nearLadder)
        {
            // Nếu đang gần cầu thang nhưng không nhấn phím leo, giữ nguyên vị trí y
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }
    }
}
