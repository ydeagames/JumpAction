using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("�����̋����F���ړ�")]
    public float moveForce = 1;
    [Tooltip("�W�����v�̋���")]
    public float jumpForce = 1;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���͂�������ă��W�b�h�ɗ͂�������
        var xMove = Input.GetAxis("Horizontal");
        _rigidbody.AddForce(xMove * Vector2.right * moveForce * Time.deltaTime);

        // �W�����v
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(jumpForce * Vector2.up * Time.deltaTime);
        }
    }
}
