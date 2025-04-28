using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    [Header("----�Ǐ]�̃X���[�Y��----")]
    [SerializeField] private float smoothSpeed = 0.125f;

    private Transform player;

    private Vector3 offset; //�J�����̃I�t�Z�b�g

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //player���ݒ肳��Ă��Ȃ��Ƃ��͂Ȃɂ����Ȃ�
        if (player == null) return;

        //�^�[�Q�b�g�̈ʒu�ɃI�t�Z�b�g��������
        Vector3 desiredPosition = player.position + offset;

        //���݂̃J�����̈ʒu���X���[�Y�Ɉړ�������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x,smoothedPosition.y,-10);
    }
}
