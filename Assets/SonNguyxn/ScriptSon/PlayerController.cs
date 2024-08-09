using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;




public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    public float currentSpeed;
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
    public AudioClip ultimateSound;
    public AudioClip explosionSound;
    private bool isFireing = true;
    private bool isUltimating = false;
    private bool isExplosing = false;
    public CinemachineVirtualCamera virtualCamera;
    public ParticleSystem ultimateEffect;
    public ParticleSystem explosionEffect;
    private bool isCameraZoomed = false;
    public BoxCollider2D ultimateTrigger;
    public BoxCollider2D explosionTrigger;
    public StatusPlayer playerStatus;
    //public AudioClip skill2Sound;
    public int climbSpeed;
    public BoxCollider2D blockCollider; // Biến để tham chiếu đến BoxCollider2D của chặn đòn
    private bool isClimbing = false; // Kiểm tra xem người chơi có đang leo hay không
    private bool isGrounded; // Kiểm tra xem người chơi có đang đứng trên mặt đất không
    public GameObject fireballPrefab; // Prefab của quả cầu lửa
    [SerializeField] private float fireballSpeed = 2.20f; // Tốc độ bắn của quả cầu lửa
    private bool canJump = true; // Thêm biến kiểm tra có thể nhảy lần thứ hai hay không
    private bool isJumping = false; // Biến kiểm tra đang nhảy
    private float jumpCooldown = 0.2f; // Thời gian chờ giữa các lần nhảy
    private float lastJumpTime; // Thời điểm cuối cùng nhảy
    private int jumpCount = 2; // Số lần nhảy tối đa
    public float attackCooldown = 0.07f; // Thời gian delay giữa các lần tấn công
    private bool isCooldown = false; // Trạng thái cooldown
    public bool facingRight = true;
    public GameObject darkBallPrefab; // Prefab của DarkBall
    public float darkBallSpeed = 10f; // Tốc độ di chuyển của DarkBall
    private bool canUseDarkBall = true; // Biến kiểm tra có thể sử dụng DarkBall hay không
    private float darkBallCooldown = 60f; // Thời gian hồi chiêu của DarkBall
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = moveSpeed;
        ultimateEffect.Stop();
        explosionEffect.Stop();
    }

    void Update()
    {
        Move();
        JumpAndCheckGround();
        Block();
        Attack();
        Climb();
        SkillFireBall();
        Ultimate();
        SkillExplosion();
        SkillExplosion();
        SkillDarkBall();
    }
    public void SkillDarkBall()
    {
        // Kiểm tra khi người chơi nhấn phím kích hoạt Skill DarkBall
        if (canUseDarkBall && Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(DarkBallCooldown());
            // Tạo ra quả cầu DarkBall
            GameObject darkBall = Instantiate(darkBallPrefab, transform.position, Quaternion.identity);
            Rigidbody2D darkBallRb = darkBall.GetComponent<Rigidbody2D>();
            // Đặt hướng di chuyển của quả cầu DarkBall theo hướng của người chơi
            darkBallRb.velocity = new Vector2(transform.localScale.x * darkBallSpeed, 0f);
            audioSource.PlayOneShot(skill1Sound); // Sử dụng âm thanh của skill1Sound hoặc thay bằng âm thanh khác nếu có
                                                  // Flip quả cầu DarkBall theo hướng của người chơi
            SpriteRenderer darkBallSprite = darkBall.GetComponent<SpriteRenderer>();
            darkBallSprite.flipX = (transform.localScale.x < 0); // Nếu Player quay mặt sang trái, flip quả cầu DarkBall

            // Di chuyển camera theo quả cầu DarkBall
            StartCoroutine(FollowDarkBall(darkBall.transform));

            // Hủy bỏ quả cầu DarkBall sau 5 giây (10f / 2f = 5 giây)
            Destroy(darkBall, 5f);
        }
    }

    private IEnumerator DarkBallCooldown()
    {
        canUseDarkBall = false;
        yield return new WaitForSeconds(darkBallCooldown);
        canUseDarkBall = true;
    }

    private IEnumerator FollowDarkBall(Transform darkBallTransform)
    {
        // Di chuyển camera theo quả cầu DarkBall
        virtualCamera.Follow = darkBallTransform;

        // Chờ cho đến khi quả cầu DarkBall biến mất
        yield return new WaitForSeconds(5f);

        // Phóng to camera lên 6f trong 0.5 giây
        StartCoroutine(SmoothZoom(6f, 0.5f));

        // Chờ 0.5 giây
        yield return new WaitForSeconds(0.5f);

        // Đặt lại camera theo Player
        virtualCamera.Follow = transform;

        // Thu nhỏ camera lại thành 2.93f trong 0.5 giây
        StartCoroutine(SmoothZoom(2.93f, 0.5f));
    }

    private IEnumerator SmoothZoom(float targetSize, float duration)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;
    }
    public void Move()
    {
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * currentSpeed, rb.velocity.y);

        if (xInput != 0)
        {
            // Cập nhật facingRight dựa trên hướng di chuyển
            facingRight = xInput > 0;

            // Cập nhật scale của nhân vật để đối mặt đúng hướng
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
    public void SkillExplosion()
    {
        if(playerStatus.currentStamina >= 50 && playerStatus.currentHp >= 20)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isExplosing = true;
                playerStatus.currentStamina -= 50;
                playerStatus.currentHp -= 10;
                explosionTrigger.enabled = true;
                audioSource.PlayOneShot(explosionSound);
                // Thu nhỏ virtual camera
                virtualCamera.m_Lens.OrthographicSize = 7f; // Điều chỉnh kích thước theo mong muốn
                isCameraZoomed = true;
                // Đóng băng trục X và trục Y của Player
                currentSpeed -= 6f;
                Debug.Log("Current Orthographic Size: " + virtualCamera.m_Lens.OrthographicSize);
                // Kết thúc hiệu ứng Ultimate
                StartCoroutine(WaitSkill());
                explosionEffect.Play();
                // Đợi thêm 0.5 giây
                StartCoroutine(ResetVirtualCamera2());
                
            }
        }
        else
        {
            isExplosing = false;
        }
    }
    private IEnumerator WaitSkill()
    {
        yield return new WaitForSeconds(0.85f);
    }
    public void Attack()
    {
        if (Input.GetKey(KeyCode.F) && !isCooldown)
        {
            StartCoroutine(PerformAttack());
        }
        else
        {
            // Người chơi không nhấn phím F hoặc đang trong trạng thái tấn công
            // hoặc chưa qua thời gian cooldown
            anim.SetBool($"Attack1", false);
            anim.SetBool($"Attack2", false);
            anim.SetBool($"Attack3", false);
            isAttacking = false;
            swordCollider.enabled = false;
            anim.SetBool("IsStaying", true);
        }
    }

    private IEnumerator PerformAttack()
    {
        // Người chơi nhấn phím F và không đang trong trạng thái tấn công
        // và đã qua thời gian cooldown
        isCooldown = true; // Bắt đầu cooldown
        int randomAttack = UnityEngine.Random.Range(1, 4); // Chọn từ 1 đến 3
        audioSource.PlayOneShot(swordSound);
        anim.SetBool($"Attack{randomAttack}", true);
        anim.SetBool("IsStaying", false);
        swordCollider.enabled = true;
        isAttacking = true;

        yield return new WaitForSeconds(attackCooldown); // Đợi thời gian cooldown

        isCooldown = false; // Kết thúc cooldown
    }

    public void Ultimate()
    {
        if (playerStatus.currentStamina >= 100)
        { 
            if (Input.GetKeyDown(KeyCode.X))
            {
                isUltimating = true;
                playerStatus.currentStamina -=100;
                ultimateTrigger.enabled = true;
                audioSource.PlayOneShot(ultimateSound);
                 // Thu nhỏ virtual camera
                 virtualCamera.m_Lens.OrthographicSize = 7f; // Điều chỉnh kích thước theo mong muốn
                 isCameraZoomed = true;
                    // Đóng băng trục X và trục Y của Player
                    currentSpeed -= 4f;
                    Debug.Log("Current Orthographic Size: " + virtualCamera.m_Lens.OrthographicSize);
                    // Kết thúc hiệu ứng Ultimate
                    ultimateEffect.Play();
                    // Đợi thêm 0.5 giây
                    StartCoroutine(ResetVirtualCamera());
            }
 
        }
        else
        {
            isUltimating = false;
        }
    }
    private IEnumerator ResetVirtualCamera()
    {
        yield return new WaitForSeconds(7f); // Đợi 0.2 giây
                                               // Trở lại trạng thái ban đầu của virtual camera
        ultimateTrigger.enabled = false;
        virtualCamera.m_Lens.OrthographicSize = 2.93f; // Khôi phục zoom mặc định
        isUltimating = false;
        isCameraZoomed = false;
        currentSpeed += 4f;
    }
    private IEnumerator ResetVirtualCamera2()
    {
        yield return new WaitForSeconds(1.5f); // Đợi 0.2 giây
                                             // Trở lại trạng thái ban đầu của virtual camera
        explosionTrigger.enabled = false;
        virtualCamera.m_Lens.OrthographicSize = 2.93f; // Khôi phục zoom mặc định
        isExplosing = false;
        isCameraZoomed = false;
        currentSpeed += 6f;
    }

    public void SkillFireBall()
    {
        // Kiểm tra khi người chơi nhấn phím kích hoạt Skill 1
        if(playerStatus.currentStamina >= 30)
        {
            isFireing = true;
            if (Input.GetKeyDown(KeyCode.C))
            {
                playerStatus.currentStamina -= 30;
                // Tạo ra quả cầu lửa
                GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
                // Đặt hướng di chuyển của quả cầu lửa theo hướng của người chơi
                fireballRb.velocity = new Vector2(transform.localScale.x * fireballSpeed, 0f);
                audioSource.PlayOneShot(skill1Sound);
                // Flip quả cầu lửa theo hướng của người chơi
                SpriteRenderer fireballSprite = fireball.GetComponent<SpriteRenderer>();
                fireballSprite.flipX = (transform.localScale.x < 0); // Nếu Player quay mặt sang trái, flip quả cầu lửa

                // Hủy bỏ quả cầu lửa sau 2 giây
                Destroy(fireball, 1f);
            }
        }
        else
        {
            isFireing = false;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                audioSource.PlayOneShot(jumpSound);
                anim.SetBool("IsJumping", true);
                jumpCount--;
            }
            else if (!isGrounded && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                anim.SetBool("IsJumping", false); // Kết thúc animation nhảy
                anim.SetBool("IsRolling", true); // Bắt đầu animation lăn
                jumpCount--;
                StartCoroutine(WaitToJump());
            }
        }
    }
    private IEnumerator WaitToJump()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("IsRolling", false);
        anim.SetBool("IsJumping", true);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsStaying", true);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsRolling", false);
            jumpCount = 2; // Reset số lần nhảy khi đặt chân xuống mặt đất
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
