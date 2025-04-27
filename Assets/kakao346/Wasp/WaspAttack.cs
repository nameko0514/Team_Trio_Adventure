using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class WaspAttack : MonoBehaviour   //�e�̏���
    {
        [SerializeField] private int damage = 1; // �e�̃_���[�W

        [SerializeField] private float lifeTime = 5f; // �e��������܂ł̎���

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)   //�v���C���[���f
        {
            // PlayerController �������Ă���΁A�v���C���[�Ɣ��f���ă_���[�W
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // �v���C���[��HP�����炷

                Destroy(gameObject); // �e������
            }

            // �n�`�ɓ��������ꍇ�������iGround���C���[�ɂ��������ǂ��炵���j
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }

}
