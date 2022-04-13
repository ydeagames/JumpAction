using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] private GameObject targetCharacter; //カメラを追従させる対象

    private void Start()
    {
        // 操作する対象のカメラを取得しておく
        mainCamera = Camera.main;
    }

    private void Update()
    {
        { // カメラをターゲットに追従させる
            // 元々のカメラ位置を取得
            Vector3 movingCameraPosition = mainCamera.transform.position;

            // ターゲットの座標で上書き（x, y）
            movingCameraPosition.x = targetCharacter.transform.position.x;
            movingCameraPosition.y = targetCharacter.transform.position.y;

            // カメラ位置更新
            mainCamera.transform.position = movingCameraPosition;
        }
    }
}
