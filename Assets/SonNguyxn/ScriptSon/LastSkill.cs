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
        virtualCamera.m_Lens.OrthographicSize = 8f; // Điều chỉnh kích thước theo mong muốn
        isCameraZoomed = true;
        
        yield return new WaitForSeconds(1f);
        Debug.Log("Waited for 1 second");
        trigger.enabled = true;
        isUsingSkill = true;
        anim.SetBool("IsLastSkill", true);
        // Dịch chuyển Player tới phía trước
        lastSkillAudioSource.PlayOneShot(lastSkillSound);
        lastSkillEffect.Play();
        Vector2 dashPosition = rb.position + Vector2.right * dashDistance;
        rb.MovePosition(dashPosition);
        trigger.enabled = true;
        playerController.currentSpeed -= 6;
        yield return new WaitForSeconds(2f);
        Debug.Log("Waited for skill duration");
        playerController.currentSpeed += 6;
        virtualCamera.m_Lens.OrthographicSize = 2.93f; // Khôi phục zoom mặc định
        isCameraZoomed = false;
        trigger.enabled = false;
        anim.SetBool("IsLastSkill", false);
        isUsingSkill = false;
        Debug.Log("Finished UseLastSkill coroutine");
    }
}
