using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerP : MonoBehaviour
{
    public int coinAmount = 0; // Số lượng đồng xu
    public TMPro.TextMeshProUGUI coinText; // Giao diện hiển thị số lượng đồng xu

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Xử lý khi va chạm với đồng xu
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinAmount++;
            Destroy(other.gameObject); // Hủy đồng xu
        }  
    }
    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + coinAmount.ToString();
    }
}
