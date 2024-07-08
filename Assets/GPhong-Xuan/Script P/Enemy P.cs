using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyP : MonoBehaviour
{

    public GameObject coinPrefab; // Kéo Prefab của đồng xu vào đây

    // Start is called before the first frame update
    void Start()
    {
        Death();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        // Rơi ra đồng xu
        Instantiate(coinPrefab, transform.position, Quaternion.identity);

        // Hủy quái
        Destroy(gameObject);
    }

}
