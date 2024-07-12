using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyP : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    //giả sử quái dg di chuyển sang phải
    private bool isRight = true;

    //public GameObject coinPrefab; // Kéo Prefab của đồng xu vào đây

    public Transform[] waypoints; // Danh sách các điểm đánh dấu (waypoints) để quái vật tuần tra

    public float viewRange = 10f; // Khoảng cách tầm nhìn
    public float fovAngle = 45f; // Góc tầm nhìn (độ)

    // Start is called before the first frame update
    void Start()
    {
        //Death();
    }

    // Update is called once per frame
    void Update()
    {

        diChuyenNgang();
        hienTai();
        
    }


    private void diChuyenNgang()
    {
        //di chuyển ngang
        //(1, 0, 0) * 1 * 0.02 = (0.02, 0, 0)

        //var direction = isRight ? Vector3.right : Vector3.left; cách 1
        var direction = Vector3.right; //cách 2
        if ((isRight == false))
        {
            direction = Vector3.left;
        }
        {

        }
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    private void hienTai()
    {
        //currentPosition: vi tri hien tai
        var currentPosition = transform.localPosition;
        if (currentPosition.x > rightBoundary)
        {
            isRight = false;
        }
        else if (currentPosition.x < leftBoundary)
        {
            isRight = true;
        }
        //scale hiện tai
        var currentScale = transform.localScale;
        if (isRight == true && currentScale.x > 0 || isRight == false && currentScale.x < 0)
        {
            currentScale.x *= -1;
        }
        transform.localScale = currentScale;
    }

    /*public void Death()
    {
        // Rơi ra đồng xu
        //Instantiate(coinPrefab, transform.position, Quaternion.identity);

        // Hủy quái
        Destroy(gameObject);
    }*/

}
