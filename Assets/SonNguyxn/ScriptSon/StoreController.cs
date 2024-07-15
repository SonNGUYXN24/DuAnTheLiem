using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public GameObject storeCanvas; // Tham chiếu đến canvas Store

    void Update()
    {
        // Hiển thị canvas khi người chơi nhấn phím "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            storeCanvas.SetActive(true);
        }

        // Đóng canvas khi người chơi nhấn phím "ESC"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            storeCanvas.SetActive(false);
        }
    }
}
