using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyP : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] public float attackRange = 1.5f;
    [SerializeField] public float attackCooldown = 2f;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;

    private Rigidbody2D rb;

    //giả sử quái dg di chuyển sang phải
    private bool isRight = true;

    //public GameObject coinPrefab; // Kéo Prefab của đồng xu vào đây

    public Transform player; // Kéo và thả đối tượng người chơi vào trường này
    public float distanceToPlayer = 2f; // Khoảng cách để xác định quái vật gặp người chơi

    private Animator animator;
    private float lastAttackTime;

    /*public Transform[] waypoints; // Danh sách các điểm đánh dấu (waypoints) để quái vật tuần tra
    public float viewRange = 10f; // Khoảng cách tầm nhìn
    public float fovAngle = 45f; // Góc tầm nhìn (độ)*/

    // Start is called before the first frame update
    void Start()
    {
        //Death();

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        diChuyenNgang();
        hienTai();


        // Tính khoảng cách giữa quái vật và người chơi
        float distance = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            // Tấn công người chơi
            Attack();
            
        }
        else
        {
            // Di chuyển theo người chơi
            MoveTowardsPlayer();
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

        // Nếu khoảng cách nhỏ hơn hoặc bằng distanceToPlayer, quái vật gặp người chơi
        if (distance <= distanceToPlayer)
        {
            // Xử lý tình huống khi quái vật gặp người chơi ở đây
            Debug.Log("Quai vat gap nguoi choi!");
            
        }

    }

    private void Attack()
    {
        

        // Kích hoạt animation tấn công
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Thực hiện tấn công (ví dụ: gọi hàm đánh)
            Debug.Log("Quai vat tan cong nguoi choi!");
            animator.SetTrigger("Attack1");
            lastAttackTime = Time.time;
        }
        else
        {
            // Kích hoạt animation tấn công
           
        }

    }

    private void MoveTowardsPlayer()
    {
        animator.SetBool("isMoving", true);
        Vector3 direction = player.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        
    }

    private void diChuyenNgang()
    {
        animator.SetBool("isMoving", true);


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
