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
    private bool isAttacking = false; // Biến để kiểm tra xem người chơi đang thực hiện cuộc tấn công hay không
    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D swordCollider;
    private float xInput;
    public AudioSource audioSource;
    /*public AudioClip moveSound;
    public AudioClip jumpSound;
    public AudioClip swordSound;
    public AudioClip skill1Sound;
    public AudioClip skill2Sound;*/
    public int climbSpeed;
    public BoxCollider2D blockCollider; // Biến để tham chiếu đến BoxCollider2D của chặn đòn
    private bool isClimbing = false; // Kiểm tra xem người chơi có đang leo hay không
    private bool isGrounded; // Kiểm tra xem người chơi có đang đứng trên mặt đất không
    public GameObject fireballPrefab; // Prefab của quả cầu lửa
    [SerializeField] private float fireballSpeed = 2.20f; // Tốc độ bắn của quả cầu lửa
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
        Block();
        Attack();
        Climb();
        SkillFireBall();
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
                //audioSource.PlayOneShot(moveSound);
            }
        }
        else
        {
            anim.SetBool("IsRunning", false); // Khi đứng yên, animation di chuyển là false
        }
    }
    public void Climb()
    {
        // Kiểm tra nếu người chơi đang leo
        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            // Điều khiển người chơi lên hoặc xuống
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
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
        if (Input.GetKey(KeyCode.F))
        {
            // Người chơi nhấn phím F và không đang trong trạng thái tấn công
            // và đã qua thời gian cooldown
            // Tạo một số ngẫu nhiên để chọn animation tấn công
            int randomAttack = UnityEngine.Random.Range(1, 4); // Chọn từ 1 đến 3

            // Kích hoạt trigger tấn công ngẫu nhiên (Attack1, Attack2, Attack3)
            anim.SetBool($"Attack{randomAttack}", true);
            anim.SetBool("IsStaying", false);
            // Kích hoạt BoxCollider2D của thanh kiếm
            swordCollider.enabled = true;
            isAttacking = true;

        }
        else
        {
            // Người chơi không nhấn phím F hoặc đang trong trạng thái tấn công
            // hoặc chưa qua thời gian cooldown
            // Tạo một số ngẫu nhiên để chọn animation tấn công
            // Kích hoạt trigger tấn công ngẫu nhiên (Attack1, Attack2, Attack3)
            anim.SetBool($"Attack1", false);
            anim.SetBool($"Attack2", false);
            anim.SetBool($"Attack3", false);
            //anim.ResetTrigger("IsAttacking"); // Đặt lại trigger IsAttacking
            isAttacking = false;
            // Vô hiệu hóa BoxCollider2D của thanh kiếm
            swordCollider.enabled = false;
            // Kích hoạt trigger IsStaying
            anim.SetBool("IsStaying", true);
        }
    }
    public void SkillFireBall()
    {
        // Kiểm tra khi người chơi nhấn phím kích hoạt Skill 1
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Tạo ra quả cầu lửa
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
            // Đặt hướng di chuyển của quả cầu lửa theo hướng của người chơi
            fireballRb.velocity = new Vector2(transform.localScale.x * fireballSpeed, 0f);

            // Flip quả cầu lửa theo hướng của người chơi
            SpriteRenderer fireballSprite = fireball.GetComponent<SpriteRenderer>();
            fireballSprite.flipX = (transform.localScale.x < 0); // Nếu Player quay mặt sang trái, flip quả cầu lửa

            // Hủy bỏ quả cầu lửa sau 2 giây
            Destroy(fireball, 2f);
        }
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

    public void OnCollisionEnter2D(Collision2D collision)
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climb"))
        {
            // Người chơi chạm vào tag "Climb", cho phép leo
            isClimbing = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climb"))
        {
            // Người chơi không còn chạm vào tag "Climb", ngừng leo
            isClimbing = false;
        }
    }
}
