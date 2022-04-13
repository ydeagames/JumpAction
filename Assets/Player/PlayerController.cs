using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [Tooltip("�����̋����F���ړ�")]
    public float moveForce = 1;
    [Tooltip("�W�����v�̋���")]
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
        // ���͂����炤
        xMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            bJump = true;
        }
    }

    void FixedUpdate()
    {
        // ���[�J���v���C���[�̎�
        if (isLocalPlayer)
        {
            // ���͂�������ăR�}���h�֐����s
            CmdMovePlayer(xMove, bJump);
            bJump = false;
        }
    }

    // ���̈ړ�
    [Command]
    void CmdMovePlayer(float xMove, bool bJump)
    {
        // ���W�b�h�ɗ͂�������
        _rigidbody.AddForce(xMove * Vector2.right * moveForce * Time.deltaTime);

        if (bJump)
        {
            _rigidbody.AddForce(jumpForce * Vector2.up * Time.deltaTime);
        }
    }
}
