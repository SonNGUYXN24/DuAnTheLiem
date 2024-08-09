using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBall : MonoBehaviour
{
    public BoxCollider2D darkBallTrigger;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goblin"))
        {
            // Dừng DarkBall
            rb.velocity = Vector2.zero;

            // Kích hoạt trigger
            StartCoroutine(ActivateTrigger());
        }
    }

    private IEnumerator ActivateTrigger()
    {
        darkBallTrigger.enabled = true;

        // Chờ 2 giây
        yield return new WaitForSeconds(2f);

        // Hủy đối tượng DarkBall
        Destroy(gameObject);
    }
}
