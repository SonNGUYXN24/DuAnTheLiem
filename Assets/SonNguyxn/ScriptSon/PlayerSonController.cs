/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyNamespace;

public class PlayerSonController : MonoBehaviour
{
    public event EventHandler<OnMaxHealthChangeEventArgs> OnMaxHealthChange;
    public class OnMaxHealthChangeEventArgs : EventArgs
    {
        public float Health;
    }



    [SerializeField] private float moveSpeed = 5f; // Tốc độ di chuyển của nhân vật
    [SerializeField] private float jumpSpeed = 13f; // Tốc độ nhảy của nhân vật

    public float dodgeSpeed = 4f; // Tốc độ khi dodge (lộn)
    public float dodgeDirection = 0.5f; // Thời gian dodge
    private float dodgeTime; // Thời gian còn lại của dodge
    private bool isDodging = false; // Trạng thái dodge (đang dodge hay không)

    public float groundDecay = 0.2f; // Để giảm tốc độ khi đang trên mặt đất

    public Rigidbody2D rb; // Thành phần Rigidbody2D của nhân vật
    public BoxCollider2D groundCheck; // Collider để kiểm tra nhân vật có trên mặt đất không
    public LayerMask groundMask; // Lớp mặt đất
    public Animator anim; // Thành phần Animator để điều khiển animation

    public GameObject arrowPrefab; // Prefab của mũi tên
    public Transform bowPosition; // Vị trí của cung trên nhân vật
    public float arrowSpeed = 10f; // Tốc độ của mũi tên
    public float arrowLifetime = 5f; // Thời gian tồn tại của mũi tên

    public float shootCooldown = 1f; // Thời gian hồi chiêu của bắn cung
    private float lastShotTime; // Thời gian của lần bắn cuối cùng

    private bool grounded; // Trạng thái kiểm tra nhân vật có đang trên mặt đất không
    private float xInput; // Biến để nhận giá trị đầu vào từ bàn phím

    public AudioClip[] audioClips;
    private Dictionary<string, AudioClip> soundClips = new Dictionary<string, AudioClip>();


    private float damage = 20;
    private float health;

    private void Awake()
    {
        var uiPlayerStat = FindObjectOfType<UI_PlayerStat>();
        uiPlayerStat?.SetPlayerController(this);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D
        anim = GetComponent<Animator>(); // Lấy thành phần Animator
        //groundCheck = GetComponent<BoxCollider2D>(); // Lấy thành phần BoxCollider2D

        soundClips.Add("footstep", audioClips[0]);


        Debug.Log("Start");
    }

    public void PlaySound(string soundName)
    {
        if (soundClips.ContainsKey(soundName))
        {
            AudioSource.PlayClipAtPoint(soundClips[soundName], transform.position);
        }
    }

    void Update()
    {
        GetInput(); // Nhận đầu vào
        MoveWithInput(); // Di chuyển nhân vật
        Jump(); // Nhảy
        Dodge(); // Lộn


        //Shoot(); // Bắn cung
    }

    void FixedUpdate()
    {
        CheckGround(); // Kiểm tra xem nhân vật có đang trên mặt đất không
        //ApplyFriction(); // Áp dụng ma sát
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal"); // Nhận giá trị đầu vào từ trục ngang
    }

    void MoveWithInput()
    {
        // Cập nhật vận tốc theo hướng ngang
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        // Lật hình ảnh nhân vật theo hướng di chuyển
        if (xInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(xInput), 1, 1);
        }
        // Cập nhật animation di chuyển
        anim.SetBool("IsRunning", xInput != 0);
    }

    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dodgeTime <= 0) // Khi nhấn phím Shift và thời gian dodge đã hết
        {
            anim.SetBool("IsRolling", true); // Kích hoạt animation dodge
            moveSpeed += dodgeSpeed; // Tăng tốc độ di chuyển
            dodgeTime = dodgeDirection; // Đặt lại thời gian dodge
            isDodging = true; // Đánh dấu trạng thái đang 
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

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) // Khi nhấn phím Space và đang trên mặt đất
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed); // Cập nhật vận tốc để nhảy lên
            anim.SetBool("IsJumping", true); // Kích hoạt animation nhảy
            grounded = false; // Đánh dấu rằng đang trong quá trình nhảy
        }
    }

    void CheckGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, Mathf.Infinity, groundMask);
        Debug.DrawRay(groundCheck.transform.position, Vector2.down * raycastHit2D.distance, Color.red);
        if (raycastHit2D.distance < 0.1f)
        {
            anim.SetBool("IsJumping", false);
            grounded = true;
        }
        else
        {
            anim.SetBool("IsJumping", true);
            grounded = false;
        }
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0 && rb.velocity.y <= 0) // Nếu nhân vật đang trên mặt đất và không di chuyển
        {
            rb.velocity *= groundDecay; // Giảm tốc độ di chuyển theo hệ số ma sát
        }
    }

    void Shoot()
    {
        if (!isDodging && Input.GetKeyDown(KeyCode.F) && Time.time >= lastShotTime + shootCooldown) // Khi nhấn phím F và thời gian hồi chiêu đã hết
        {
            lastShotTime = Time.time; // Cập nhập thời gian bắn cuối cùng 
            anim.SetTrigger("IsAttacking"); // Kích hoạt animation bắn cung
            Invoke(nameof(FireArrow), 0.1f); // Gọi hàm FireArrow sau một khoảng thời gian ngắn để khớp với animation
        }
    }

    void FireArrow()
    {
        if (bowPosition == null) // Kiểm tra bowPosition đã được gán chưa
        {
            Debug.LogError("Bow Position chưa được gán trong Inspector.");
            return;
        }

        GameObject arrow = Instantiate(arrowPrefab, bowPosition.position, Quaternion.identity); // Tạo mũi tên từ prefab
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D của mũi tên
        arrowRb.velocity = new Vector2(transform.localScale.x * arrowSpeed, 0); // Điều chỉnh hướng bay của mũi tên dựa trên hướng của nhân vật

        // Kiểm tra hướng của nhân vật và flip mũi tên nếu cần
        Vector3 arrowScale = arrow.transform.localScale;
        if (transform.localScale.x < 0)
        {
            arrowScale.x = -Mathf.Abs(arrowScale.x); // Flip mũi tên nếu nhân vật đang quay về bên trái
        }
        else
        {
            arrowScale.x = Mathf.Abs(arrowScale.x); // Đảm bảo mũi tên không bị flip nếu nhân vật đang quay về bên phải
        }
        arrow.transform.localScale = arrowScale;

        anim.SetTrigger("isStaying"); // Đảm bảo nhân vật trở về trạng thái đứng im
        Destroy(arrow, arrowLifetime); // Hủy mũi tên sau khoảng thời gian


    }

    public void SetMaxHealth(int maxHealth)
    {
        OnMaxHealthChange?.Invoke(this, new OnMaxHealthChangeEventArgs { Health = maxHealth });
        Debug.Log(maxHealth);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
        Debug.Log(damage);
    }

    public float GetDamage()
    {
        return damage;
    }

}
*/