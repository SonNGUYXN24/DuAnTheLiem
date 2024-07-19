using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource bossBattle;
    public string playerTag = "Player"; // Tag của Player (hãy đảm bảo đúng Tag)
    public float triggerDistance = 1f; // Khoảng cách để kích hoạt âm thanh
    private bool bossBattlePlaying = false; // Biến để kiểm tra xem âm thanh bossBattle đang phát hay không

    private void Update()
    {
        // Kiểm tra xem có GameObject nào có Tag "Player" ở gần không
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= triggerDistance)
            {
                if (!bossBattlePlaying)
                {
                    bossBattle.Play();
                    bossBattlePlaying = true;
                    backgroundMusic.Stop();
                }
            }
            else
            {
                if (bossBattlePlaying)
                {
                    bossBattle.Stop();
                    bossBattlePlaying = false;
                    backgroundMusic.Play();
                }
            }
        }
        else
        {
            // Nếu không tìm thấy GameObject với Tag "Player," tiếp tục phát nhạc background
            if (bossBattlePlaying)
            {
                bossBattle.Stop();
                bossBattlePlaying = false;
                backgroundMusic.Play();
            }
        }
    }
}
