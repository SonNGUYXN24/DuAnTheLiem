using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loi_Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;//van toc di chuyen
    [SerializeField]
    private float _jumpVelocity = 5.0f;//van toc nhay
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;//huong htai nhan vat
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        //bat sk nhan phim ngang
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        //kiem tra huong di chuyen
        if (horizontalInput < 0)
        {
            _facingRight = false;
        }
        else if (horizontalInput > 0)
        {
            _facingRight = true;
        }
        
        Flip();


    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        if (scale.x > 0 && !_facingRight)
        {
            scale.x *= -1;
            transform.localScale = scale;
            return;
        }
        if (scale.x < 0 && _facingRight)
        {
            scale.x *= -1;
            transform.localScale = scale;
            return;
        }
        return;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //nhay
            _rigidbody2D.linearVelocity = Vector2.up * _jumpVelocity;
        }
    }
}
