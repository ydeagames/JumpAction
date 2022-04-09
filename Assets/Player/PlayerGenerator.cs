using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public GameObject playerPrefab;
    private BoxCollider2D areacollider;
    [Tooltip("スポーン間隔（秒）")]
    public float timeInterval = 1;
    private float _timeFromPreviousSpawn;

    // Start is called before the first frame update
    void Start()
    {
        areacollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 一定時間ごとに実行
        _timeFromPreviousSpawn += Time.deltaTime;
        if(_timeFromPreviousSpawn < timeInterval)
        {
            return;
        }
        _timeFromPreviousSpawn -= timeInterval;

        // 範囲を取得
        var b = areacollider.bounds;
        // 範囲内でランダムに位置を設定
        var pos = new Vector3( 
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            0
        );
        // インスタンス生成
        var obj = Instantiate(playerPrefab, transform);
        // 位置を設定
        obj.transform.position = pos;
    }

    // FPS に依存しない。物理法則向き
    private void FixedUpdate()
    {
        
    }
}
