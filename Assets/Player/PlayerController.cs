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
        // 入力をもらってリジッドに力を加える
        var xMove = Input.GetAxis("Horizontal");
        var velocityX = xMove * Vector2.right * moveForce * Time.deltaTime;
        _rigidbody.velocity = new Vector2(velocityX.x, _rigidbody.velocity.y);

        // ジャンプ
        if (Input.GetButtonDown("Jump"))
        {
            // 地面と接触しているかどうかチェック
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

    // キャラクターがステージと接触しているかどうかを判定する
    bool GroundChk()
    {
        Vector3 startposition = transform.position;                     // Playerの中心を始点とする
        Vector3 endposition = transform.position - transform.up * endLinecastRatio; // Playerの足元を終点とする

        // Debug用に始点と終点を表示する
        Debug.DrawLine(startposition, endposition, Color.red);

        // Physics2D.Linecastを使い、ベクトルとStageLayerが接触していたらTrueを返す
        return Physics2D.Linecast(startposition, endposition, stageLayer);
    }
}
