using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum MOVE_HORIZONTAL
    {
        STOP,
        RIGHT,
        LEFT,
    }

    [Tooltip("�����̋����F���ړ�")]
    public float moveForce = 1;
    [Tooltip("�W�����v�̋���")]
    public float jumpForce = 1;
    private Rigidbody2D _rigidbody;
    [SerializeField] LayerMask stageLayer;
    [SerializeField] float endLinecastRatio = 0.3f;
    [SerializeField] float moveAttenuationRatio = 0.9f;

    // ���[�U���̓p�����[�^
    MOVE_HORIZONTAL moveHoraizon = MOVE_HORIZONTAL.STOP;
    bool isJumped = false;

    // �f�o�b�O�p�ϐ�
    bool isDebugging = false;



    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���͂�������ă��W�b�h�ɗ͂�������
        var horizonkey = Input.GetAxis("Horizontal");
        if(horizonkey == 0)
        {
            moveHoraizon = MOVE_HORIZONTAL.STOP;
        }
        else if (horizonkey > 0)
        {
            moveHoraizon = MOVE_HORIZONTAL.RIGHT;
        }
        else if (horizonkey < 0)
        {
            moveHoraizon = MOVE_HORIZONTAL.LEFT;
        }

        // �W�����v
        if (Input.GetButtonDown("Jump"))
        {
            // �n�ʂƐڐG���Ă��邩�ǂ����`�F�b�N
            if (GroundChk())
            {
                isJumped = true;

            }
        }

        // �f�o�b�O�p�{�^��
        if (Input.GetKeyDown(KeyCode.G))
        {
            isDebugging = true;
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            isDebugging = false;
        }
    }

    // �����@���Ɋ֘A���鏈���͂������ŏ���
    private void FixedUpdate()
    {
        Move();

        if (isJumped)
        {
            isJumped = false;
            Jump();
        }
    }

    void Move()
    {
        float speed = 0;
        Vector3 scale = transform.localScale;
        switch (moveHoraizon)
        {
            case MOVE_HORIZONTAL.STOP:
                speed = 0;
                break;
            case MOVE_HORIZONTAL.RIGHT:
                speed = 1;
                break;
            case MOVE_HORIZONTAL.LEFT:
                speed = -1;
                break;
            default:
                break;
        }

        // �������ɗ͂�������
        transform.localScale = scale; // scale����
        _rigidbody.AddForce(moveForce * speed * Vector2.right);
        // �������̑��x������������
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * moveAttenuationRatio, _rigidbody.velocity.y);

        if (isDebugging)
        {
            Debug.Log(_rigidbody.velocity);
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(jumpForce * Vector2.up);
    }

    // �L�����N�^�[���X�e�[�W�ƐڐG���Ă��邩�ǂ����𔻒肷��
    bool GroundChk()
    {
        Vector3 startposition = transform.position;                     // Player�̒��S���n�_�Ƃ���
        Vector3 endposition = transform.position - transform.up * endLinecastRatio; // Player�̑������I�_�Ƃ���

        // Debug�p�Ɏn�_�ƏI�_��\������
        Debug.DrawLine(startposition, endposition, Color.red);
        Debug.Log("Jump  start:" + startposition.ToString() + "end" + endposition.ToString());

        // Physics2D.Linecast���g���A�x�N�g����StageLayer���ڐG���Ă�����True��Ԃ�
        return Physics2D.Linecast(startposition, endposition, stageLayer);
    }
}
