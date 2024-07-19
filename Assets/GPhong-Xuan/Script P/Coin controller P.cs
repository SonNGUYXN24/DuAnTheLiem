using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoincontrollerP : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip coinsSound;
    public ParticleSystem itemsEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Phương thức này được gọi khi người chơi va chạm với đồng xu
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Thêm logic ở đây (ví dụ: phát âm thanh hoặc hiệu ứng hạt)
            StartCoroutine(PlaySound());
            // Hủy đối tượng đồng xu   
        }
    }
    private IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(coinsSound);
        itemsEffect.Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
