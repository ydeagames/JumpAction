using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] private GameObject targetCharacter; //�J������Ǐ]������Ώ�
    [SerializeField] float lerpRatio = 0.001f;

    private void Start()
    {
        // ���삷��Ώۂ̃J�������擾���Ă���
        mainCamera = Camera.main;
    }

    private void Update()
    {
        { // �J�������^�[�Q�b�g�ɒǏ]������
          // ���X�̃J�����ʒu���擾
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 targetPosition = targetCharacter.transform.position;
            Vector2 newPosition = Vector2.Lerp(cameraPosition, targetPosition, Time.deltaTime * 60 * lerpRatio);
            Vector3 movingCameraPosition = cameraPosition;

            // �^�[�Q�b�g�̍��W�ŏ㏑���ix, y�j
            movingCameraPosition.x = newPosition.x;
            movingCameraPosition.y = newPosition.y;

            // �J�����ʒu�X�V
            mainCamera.transform.position = movingCameraPosition;
        }
    }
}
