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

    [Tooltip("動きの強さ：横移動")]
    public float moveForce = 1;
    [Tooltip("ジャンプの強さ")]
    public float jumpForce = 1;
    private Rigidbody2D _rigidbody;
    [SerializeField] LayerMask stageLayer;
    [SerializeField] float endLinecastRatio = 0.3f;
    [SerializeField] float moveAttenuationRatio = 0.9f;

    // ユーザ入力パラメータ
    MOVE_HORIZONTAL moveHoraizon = MOVE_HORIZONTAL.STOP;
    bool isJumped = false;

    // デバッグ用変数
    bool isDebugging = false;



    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 入力をもらってリジッドに力を加える
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

        // ジャンプ
        if (Input.GetButtonDown("Jump"))
        {
            // 地面と接触しているかどうかチェック
            if (GroundChk())
            {
                isJumped = true;

            }
        }

        // デバッグ用ボタン
        if (Input.GetKeyDown(KeyCode.G))
        {
            isDebugging = true;
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            isDebugging = false;
        }
    }

    // 物理法則に関連する処理はこっちで処理
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

        // 横方向に力を加える
        transform.localScale = scale; // scaleを代入
        _rigidbody.AddForce(moveForce * speed * Vector2.right);
        // 横方向の速度を減衰させる
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

    // キャラクターがステージと接触しているかどうかを判定する
    bool GroundChk()
    {
        Vector3 startposition = transform.position;                     // Playerの中心を始点とする
        Vector3 endposition = transform.position - transform.up * endLinecastRatio; // Playerの足元を終点とする

        // Debug用に始点と終点を表示する
        Debug.DrawLine(startposition, endposition, Color.red);
        Debug.Log("Jump  start:" + startposition.ToString() + "end" + endposition.ToString());

        // Physics2D.Linecastを使い、ベクトルとStageLayerが接触していたらTrueを返す
        return Physics2D.Linecast(startposition, endposition, stageLayer);
    }
}
