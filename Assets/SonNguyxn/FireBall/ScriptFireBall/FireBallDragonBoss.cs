using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDragonBoss : MonoBehaviour
{
    public float speed = 15f;
    private Transform player;
    public GameObject explosionEffect; // Prefab của hiệu ứng nổ

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Dừng lại và kích hoạt hiệu ứng nổ
            speed = 0f;
            Instantiate(explosionEffect, transform.position, transform.rotation);
            StartCoroutine(DestroyAfterDelay(2f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
