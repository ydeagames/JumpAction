using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [Tooltip("動きの強さ：横移動")]
    public float moveForce = 1;
    [Tooltip("ジャンプの強さ")]
    public float jumpForce = 1;

    private Rigidbody2D _rigidbody;

    float xMove;
    bool bJump;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // 入力をもらう
        xMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            bJump = true;
        }
    }

    void FixedUpdate()
    {
        // ローカルプレイヤーの時
        if (isLocalPlayer)
        {
            // 入力をもらってコマンド関数実行
            CmdMovePlayer(xMove, bJump);
            bJump = false;
        }
    }

    // 球の移動
    [Command]
    void CmdMovePlayer(float xMove, bool bJump)
    {
        // リジッドに力を加える
        _rigidbody.AddForce(xMove * Vector2.right * moveForce * Time.deltaTime);

        if (bJump)
        {
            _rigidbody.AddForce(jumpForce * Vector2.up * Time.deltaTime);
        }
    }
}
