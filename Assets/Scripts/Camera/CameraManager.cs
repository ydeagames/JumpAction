using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] private GameObject targetCharacter; //�J������Ǐ]������Ώ�

    private void Start()
    {
        // ���삷��Ώۂ̃J�������擾���Ă���
        mainCamera = Camera.main;
    }

    private void Update()
    {
        { // �J�������^�[�Q�b�g�ɒǏ]������
            // ���X�̃J�����ʒu���擾
            Vector3 movingCameraPosition = mainCamera.transform.position;

            // �^�[�Q�b�g�̍��W�ŏ㏑���ix, y�j
            movingCameraPosition.x = targetCharacter.transform.position.x;
            movingCameraPosition.y = targetCharacter.transform.position.y;

            // �J�����ʒu�X�V
            mainCamera.transform.position = movingCameraPosition;
        }
    }
}
