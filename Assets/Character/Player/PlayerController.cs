using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
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
    [Tooltip("���˂̋���")]
    public float shootForce = 1;
    private Rigidbody2D _rigidbody;
    [SerializeField] LayerMask stageLayer;
    [SerializeField] float endLinecastRatio = 0.3f;
    [SerializeField] float moveAttenuationRatio = 0.9f;
    [SerializeField] GameObject ballPrefab;

    // ���[�U���̓p�����[�^
    MOVE_HORIZONTAL m_moveHoraizon = MOVE_HORIZONTAL.STOP;
    bool m_isJumped = false;
    bool m_isShoot = false;
    Vector2 m_shootAngle = Vector2.zero;

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
        if (!isLocalPlayer)
        {
            return;
        }

        // ���͂�������ă��W�b�h�ɗ͂�������
        var horizonkey = Input.GetAxis("Horizontal");
        if(horizonkey == 0)
        {
            m_moveHoraizon = MOVE_HORIZONTAL.STOP;
        }
        else if (horizonkey > 0)
        {
            m_moveHoraizon = MOVE_HORIZONTAL.RIGHT;
        }
        else if (horizonkey < 0)
        {
            m_moveHoraizon = MOVE_HORIZONTAL.LEFT;
        }

        // �W�����v
        if (Input.GetButtonDown("Jump"))
        {
            // �n�ʂƐڐG���Ă��邩�ǂ����`�F�b�N
            if (GroundChk())
            {
                m_isJumped = true;

            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            var to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var from = transform.position;
            m_shootAngle = (to - from).normalized;
            m_isShoot = true;
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
        if (!isLocalPlayer)
        {
            return;
        }

        CmdMove(m_moveHoraizon, m_isJumped, m_isShoot, m_shootAngle);
        m_isJumped = false;
        m_isShoot = false;
    }

    [Command]
    void CmdMove(MOVE_HORIZONTAL moveHoraizon, bool isJumped, bool isShoot, Vector2 shootAngle)
    {
        Move(moveHoraizon);

        if (isJumped)
        {
            Jump();
        }

        if (isShoot)
        {
            Shoot(shootAngle);
        }
    }


    void Move(MOVE_HORIZONTAL moveHoraizon)
    {
        float speed = 0;
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

    void Shoot(Vector2 angle)
    {
        var ball = Instantiate(ballPrefab);
        ball.transform.position = transform.position;
        ball.GetComponent<Rigidbody2D>().AddForce(shootForce * angle);
        NetworkServer.Spawn(ball);
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
