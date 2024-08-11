using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HideWall : MonoBehaviour
{
    public GameObject spike1;
    public GameObject spike2;
    public AudioSource wallAudio;
    public AudioClip wallSounds;
    public AudioClip spikeSounds;
    public GameObject blockWall1;
    public GameObject blockWall2;
    public CinemachineVirtualCamera virtualCamera;
    public Transform player;
    public DragonBoss dragonBoss;

    private bool hasTriggered = false;
    private float originalScreenX;
    private float originalScreenY;
    private float originalCameraSize;
    private Vector3 originalBlockWall1Position;
    private Vector3 originalBlockWall2Position;
    private Vector3 originalSpike1Position;
    private Vector3 originalSpike2Position;

    void Start()
    {
        var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        originalScreenX = framingTransposer.m_ScreenX;
        originalScreenY = framingTransposer.m_ScreenY;
        originalCameraSize = virtualCamera.m_Lens.OrthographicSize;
        originalBlockWall1Position = blockWall1.transform.position;
        originalBlockWall2Position = blockWall2.transform.position;
        originalSpike1Position = spike1.transform.position;
        originalSpike2Position = spike2.transform.position;
    }

    void Update()
    {
        if (dragonBoss.currentHPEnemy <= 0 && hasTriggered)
        {
            StartCoroutine(ResetWallsAndSpikes());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BossBattle") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(HandleBossBattle());
        }
    }

    private IEnumerator HandleBossBattle()
    {
        // Di chuyển và phóng to camera
        var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        float elapsedTime = 0f;
        float targetSize = 15f;
        while (elapsedTime < 1f)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(originalCameraSize, targetSize, elapsedTime / 1f);
            framingTransposer.m_ScreenX = Mathf.Lerp(originalScreenX, 0.40f, elapsedTime / 1f);
            framingTransposer.m_ScreenY = Mathf.Lerp(originalScreenY, 0.8f, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = targetSize;
        framingTransposer.m_ScreenX = 0.40f;
        framingTransposer.m_ScreenY = 0.8f;
        yield return new WaitForSeconds(1f);

        // Di chuyển blockWall1 và blockWall2 xuống
        wallAudio.PlayOneShot(wallSounds);
        elapsedTime = 0f;
        Vector3 targetPosition1 = new Vector3(blockWall1.transform.position.x, -3.4f, blockWall1.transform.position.z);
        Vector3 targetPosition2 = new Vector3(blockWall2.transform.position.x, -3.4f, blockWall2.transform.position.z);
        while (elapsedTime < 3f)
        {
            blockWall1.transform.position = Vector3.Lerp(blockWall1.transform.position, targetPosition1, elapsedTime / 3f);
            blockWall2.transform.position = Vector3.Lerp(blockWall2.transform.position, targetPosition2, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        blockWall1.transform.position = targetPosition1;
        blockWall2.transform.position = targetPosition2;

        // Di chuyển spike1 và spike2
        wallAudio.PlayOneShot(spikeSounds);
        elapsedTime = 0f;
        Vector3 spike1TargetPosition = new Vector3(0.9f, spike1.transform.position.y, spike1.transform.position.z);
        Vector3 spike2TargetPosition = new Vector3(-1.8f, spike2.transform.position.y, spike2.transform.position.z);
        while (elapsedTime < 0.5f)
        {
            spike1.transform.position = Vector3.Lerp(spike1.transform.position, spike1TargetPosition, elapsedTime / 0.5f);
            spike2.transform.position = Vector3.Lerp(spike2.transform.position, spike2TargetPosition, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spike1.transform.position = spike1TargetPosition;
        spike2.transform.position = spike2TargetPosition;

        // Thu nhỏ camera về vị trí ban đầu
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(targetSize, originalCameraSize, elapsedTime / 1f);
            framingTransposer.m_ScreenX = Mathf.Lerp(0.40f, originalScreenX, elapsedTime / 1f);
            framingTransposer.m_ScreenY = Mathf.Lerp(0.8f, originalScreenY, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = originalCameraSize;
        framingTransposer.m_ScreenX = originalScreenX;
        framingTransposer.m_ScreenY = originalScreenY;
    }


    private IEnumerator ResetWallsAndSpikes()
    {
        // Đặt spike1 và spike2 về trạng thái không hoạt động
        spike1.SetActive(false);
        spike2.SetActive(false);

        // Đặt blockWall1 và blockWall2 về trạng thái không hoạt động
        blockWall1.SetActive(false);
        blockWall2.SetActive(false);

        yield return null;
    }

}
