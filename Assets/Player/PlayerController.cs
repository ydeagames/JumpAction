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
    [SerializeField] LayerMask stageLayer;
    [SerializeField] float endLinecastRatio = 0.3f;

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
        var velocityX = xMove * Vector2.right * moveForce * Time.deltaTime;
        _rigidbody.velocity = new Vector2(velocityX.x, _rigidbody.velocity.y);

        // �W�����v
        if (Input.GetButtonDown("Jump"))
        {
            // �n�ʂƐڐG���Ă��邩�ǂ����`�F�b�N
            if (GroundChk())
            {
                Debug.Log("Jump");
                Jump();

            }
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(jumpForce * Vector2.up * Time.deltaTime);
    }

    // �L�����N�^�[���X�e�[�W�ƐڐG���Ă��邩�ǂ����𔻒肷��
    bool GroundChk()
    {
        Vector3 startposition = transform.position;                     // Player�̒��S���n�_�Ƃ���
        Vector3 endposition = transform.position - transform.up * endLinecastRatio; // Player�̑������I�_�Ƃ���

        // Debug�p�Ɏn�_�ƏI�_��\������
        Debug.DrawLine(startposition, endposition, Color.red);

        // Physics2D.Linecast���g���A�x�N�g����StageLayer���ڐG���Ă�����True��Ԃ�
        return Physics2D.Linecast(startposition, endposition, stageLayer);
    }
}
