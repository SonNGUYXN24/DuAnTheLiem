using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Coin
        if (collision.gameObject.CompareTag("Coin"))
        {
            // Dong xu bien mat
            Destroy(collision.gameObject);
            // Phat ra am thanh
            //_audioSource.PlayOneShot(_coinCollectSXF);
            //
            //_score += 1;
            //_scoreText.text = _score.ToString();

        }
    }

}
