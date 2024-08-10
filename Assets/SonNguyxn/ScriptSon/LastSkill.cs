using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastSkill : MonoBehaviour
{
    public float speed = 50f;
    public float dashDistance = 20f; // Khoảng cách dịch chuyển
    public float skillDuration = 1f; // Thời gian chờ trước khi quay về trạng thái bình thường
    private bool isUsingSkill = false; // Trạng thái sử dụng kỹ năng
    public Animator anim; // Biến Animator
    public Rigidbody2D rb; // Biến Rigidbody2D
    public BoxCollider2D trigger;
    public CinemachineVirtualCamera virtualCamera;
    private bool isCameraZoomed = false;
    public PlayerController playerController;
    public AudioClip lastSkillSound;
    public AudioSource lastSkillAudioSource;
    public ParticleSystem lastSkillEffect;

    void Start()
    {
        lastSkillEffect.Stop();
    }

    void Update()
    {
        Skill();
    }

    public void Skill()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isUsingSkill)
        {
            StartCoroutine(UseLastSkill());
        }
    }

    private IEnumerator UseLastSkill()
    {
        Debug.Log("Starting UseLastSkill coroutine");
        StartCoroutine(SmoothZoom(1.25f, 0.75f, new Vector2(0.54f, 0.64f))); // Phóng to camera mượt mà trong 0.75 giây
        isCameraZoomed = true;

        yield return new WaitForSeconds(0.75f);
        Debug.Log("Waited for 0.75 seconds");
        trigger.enabled = true;
        isUsingSkill = true;
        anim.SetBool("IsLastSkill", true);
        // Dịch chuyển Player tới phía trước
        lastSkillAudioSource.PlayOneShot(lastSkillSound);
        lastSkillEffect.Play();

        // Kiểm tra lớp trước khi bỏ qua va chạm
        int playerLayer = LayerMask.NameToLayer("Player");
        int obstaclesLayer = LayerMask.NameToLayer("Enemies");

        if (playerLayer != -1 && obstaclesLayer != -1)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, obstaclesLayer, true);
        }
        else
        {
            Debug.LogError("Player or Obstacles layer not found");
        }

        yield return new WaitForSeconds(0.01f);

        // Xác định hướng dash
        Vector2 dashDirection = playerController.facingRight ? Vector2.right : Vector2.left;
        StartCoroutine(SmoothDash(rb.position + dashDirection * dashDistance, 0.75f)); // Dash mượt mà trong 0.3 giây
        StartCoroutine(SmoothZoom(7.5f, 0.75f, new Vector2(0.54f, 0.64f))); // Phóng to camera mượt mà trong 0.3 giây

        trigger.enabled = true;
        playerController.currentSpeed -= 6;
        yield return new WaitForSeconds(2f);
        Debug.Log("Waited for skill duration");
        playerController.currentSpeed += 6;
        StartCoroutine(SmoothZoom(2.93f, 0.5f, new Vector2(0.37f, 0.64f))); // Thu nhỏ camera mượt mà trong 0.5 giây
        isCameraZoomed = false;
        trigger.enabled = false;
        anim.SetBool("IsLastSkill", false);
        isUsingSkill = false;

        // Kích hoạt lại va chạm giữa Player và các vật cản
        if (playerLayer != -1 && obstaclesLayer != -1)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, obstaclesLayer, false);
        }

        Debug.Log("Finished UseLastSkill coroutine");
    }

    private IEnumerator SmoothZoom(float targetSize, float duration, Vector2 targetScreenPosition)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        Vector2 startScreenPosition = new Vector2(virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX, virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = Mathf.Lerp(startScreenPosition.x, targetScreenPosition.x, elapsed / duration);
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(startScreenPosition.y, targetScreenPosition.y, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = targetScreenPosition.x;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = targetScreenPosition.y;
    }

    private IEnumerator SmoothDash(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = rb.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);
    }
}
