using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PortalP : MonoBehaviour
{
    public string nextSceneName; // Tên của cảnh tiếp theo
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Khi người chơi va chạm với cổng, chuyển đến cảnh tiếp theo
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
