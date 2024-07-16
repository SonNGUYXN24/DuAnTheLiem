using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoincontrollerP : MonoBehaviour
{
    // Start is called before the first frame update
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

            // Hủy đối tượng đồng xu
            Destroy(gameObject);
        }
    }
}
