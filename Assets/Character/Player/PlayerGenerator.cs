using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public GameObject playerPrefab;
    private BoxCollider2D areacollider;
    [Tooltip("�X�|�[���Ԋu�i�b�j")]
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
        // ��莞�Ԃ��ƂɎ��s
        _timeFromPreviousSpawn += Time.deltaTime;
        if(_timeFromPreviousSpawn < timeInterval)
        {
            return;
        }
        _timeFromPreviousSpawn -= timeInterval;

        // �͈͂��擾
        var b = areacollider.bounds;
        // �͈͓��Ń����_���Ɉʒu��ݒ�
        var pos = new Vector3( 
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            0
        );
        // �C���X�^���X����
        var obj = Instantiate(playerPrefab, transform);
        // �ʒu��ݒ�
        obj.transform.position = pos;
    }

    // FPS �Ɉˑ����Ȃ��B�����@������
    private void FixedUpdate()
    {
        
    }
}
