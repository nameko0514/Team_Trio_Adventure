using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class DevilController : MonoBehaviour
    {
        [SerializeField] private int damage = 1;  //�_���[�W
        [SerializeField] private float debuffTime = 3f; //�f�o�t����

        private void OnTriggerEnter2D(Collider2D other)
        {
            //layerController���p�������v���C���[�ɓ���������
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Devil: �v���C���[�Ƀq�b�g�I");

                player.TakeDamage(damage); // P�̗͂����炷

                //// �v���C���[�����씽�]�������Ă��邩�m�F���ĕt�^����
                //if (player is IReverseControl reverseControl)�@�@�@//IReverseControl��Scripts�t�H���_�[�ɒǉ����Ăق����ł�
                //{
                //    reverseControl.ApplyReverseControl(debuffTime);  // ���]�f�o�t��^����
                //}
            }
        }
       
    }

}

