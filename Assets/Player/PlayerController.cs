using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("動きの強さ：横移動")]
    public float moveForce = 1;
    [Tooltip("ジャンプの強さ")]
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
        // 入力をもらってリジッドに力を加える
        var xMove = Input.GetAxis("Horizontal");
        _rigidbody.AddForce(xMove * Vector2.right * moveForce * Time.deltaTime);

        // ジャンプ
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(jumpForce * Vector2.up * Time.deltaTime);
        }
    }
}
