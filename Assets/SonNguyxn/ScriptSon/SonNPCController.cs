using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SonNPCController : MonoBehaviour
{
    public GameObject canvasHint; // Tham chiếu đến canvas hint
    public GameObject storeNPC;
    private bool playerInRange = false; // Biến kiểm tra xem Player có ở gần NPC hay không

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            canvasHint.SetActive(true); // Hiển thị canvas hint
            storeNPC.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            canvasHint.SetActive(false); // Vô hiệu hóa canvas hint
            storeNPC.SetActive(false);
        }
    }
}
