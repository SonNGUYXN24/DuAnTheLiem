using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            // Nếu va chạm với tag "Enemies", hủy bỏ đối tượng FireBall
            Destroy(gameObject);
        }
    }
}
