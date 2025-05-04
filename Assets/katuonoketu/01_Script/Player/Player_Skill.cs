using matumoto;
using UnityEngine;
using static matumoto.SESoundData;

namespace Takato
{
    public class Player_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("---�D���ȃX�L����GameObject����ĂˁI---")]
        [SerializeField] private GameObject skillEffectPrefab;�@// �X�L���̃G�t�F�N�gPrefab

        [Header("---FirePoint������ĂˁI(���GameObject)---")]
        [SerializeField] private Transform firePoint;  // ���ˈʒu

        [Header("---�e�̔�΂���")]
        [SerializeField] private int bulletCount; // �����΂�܂���

        [Header("---�e�̔�΂��p�x")]
        [SerializeField] private float Angle; // �X�L�����΂��p�x

        [Header("---�X�L���̔����Ԋu---")]
        [SerializeField] private float fireRate; // �X�L���̔����Ԋu

        [Header("---�e�̃X�s�[�h---")]
        [SerializeField] private float bulletSpeed; // �e��

        private float lastFireTime = -Mathf.Infinity; // �Ō�̃X�L���̔�������

        void Start()
        {
            // SwitchPlayer��FindFirstObjectByType�Ŏ擾����B
            switchPlayer = Object.FindFirstObjectByType<SwitchPlayer>();

            if (switchPlayer == null)
            {
                Debug.LogError("SwitchPlayer���V�[����Ɍ�����܂���ł����I");
            }
        }

        void Update()
        {
            Vector2 direction = transform.right; // �L�����N�^�[�̉E����(�������Ă����)�Ɍ���

            if (switchPlayer != null && switchPlayer.isTrigger)
            {
                TryUseSkill();
                switchPlayer.ResetTrigger(); // �g���K�[�����Z�b�g���đ��d������h��
            }
        }

        void TryUseSkill()
        {
            // �X�L���̔����Ԋu���`�F�b�N
            if (Time.time - lastFireTime >= fireRate)
            {
                lastFireTime = Time.time; // �Ō�̔������Ԃ��X�V
                UseSkill();
            }
        }

        public void UseSkill()
        {
            // �����ˎ���SE�Đ�
            SoundManager.Instance.PlaySE(SESoundData.SE.None);

            if (skillEffectPrefab && firePoint)
            {
                int bulletcount = bulletCount; // �����΂�܂���
                float spreadAngle = Angle; // �S�̂ŉ��x���炢�ɂ΂炯�邩

                for (int i = 0; i < bulletCount; i++)
                {
                     // ���ˊp�x���v�Z
                     float angle = (bulletcount > 1)
                     ? -spreadAngle / 2f + (spreadAngle / (bulletcount - 1)) * i
                     : 0f;

                    // �e�𐶐�
                    GameObject bullet = Instantiate(skillEffectPrefab, firePoint.position, firePoint.rotation);

                    // �p�x�����炷
                    bullet.transform.Rotate(0, 0, angle);

                    // �O���ɗ͂�������i�e�� Rigidbody2D �����Ă�O��j
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = bullet.transform.right * bulletSpeed; // �e��
                    }
                }

                Debug.Log($"{gameObject.name} �X�L�������I�i�U�e�j");
            }
        }

    }
}
