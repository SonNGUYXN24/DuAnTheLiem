using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBall : MonoBehaviour
{
    public BoxCollider2D darkBallTrigger;
    private Rigidbody2D rb;
    public ParticleSystem darkBallEffect;
    public AudioSource darkBallSound;
    public AudioClip darkBallSounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        darkBallEffect.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goblin"))
        {
            // Dừng DarkBall
            rb.linearVelocity = Vector2.zero;
            darkBallEffect.Play();
            darkBallSound.PlayOneShot(darkBallSounds);

            // Kích hoạt trigger
            StartCoroutine(ActivateTrigger());
        }
    }

    private IEnumerator ActivateTrigger()
    {
        darkBallTrigger.enabled = true;

        // Chờ 2 giây
        yield return new WaitForSeconds(2.5f);

        // Hủy đối tượng DarkBall
        Destroy(gameObject);
    }
}
