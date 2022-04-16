using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] private GameObject targetCharacter; //カメラを追従させる対象
    [SerializeField] float lerpRatio = 0.001f;

    private void Start()
    {
        // 操作する対象のカメラを取得しておく
        mainCamera = Camera.main;
    }

    private void Update()
    {
        { // カメラをターゲットに追従させる
          // 元々のカメラ位置を取得
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 targetPosition = targetCharacter.transform.position;
            Vector2 newPosition = Vector2.Lerp(cameraPosition, targetPosition, Time.deltaTime * 60 * lerpRatio);
            Vector3 movingCameraPosition = cameraPosition;

            // ターゲットの座標で上書き（x, y）
            movingCameraPosition.x = newPosition.x;
            movingCameraPosition.y = newPosition.y;

            // カメラ位置更新
            mainCamera.transform.position = movingCameraPosition;
        }
    }
}
