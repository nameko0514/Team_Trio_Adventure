using UnityEngine;
using System.Collections;
using matumoto;

namespace Tkato
{
    public class Slime_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("---�D���ȃX�L����GameObject����ĂˁI---")]
        [SerializeField] private GameObject skillEffectPrefab;�@// �X�L���̃G�t�F�N�gPrefab

        [Header("---FirePoint������ĂˁI(���GameObject)---")]
        [SerializeField] private Transform firePoint;  // ���ˈʒu

        [Header("---�X�L���̔����Ԋu---")]
        [SerializeField] private float fireRate; // �X�L���̔����Ԋu

        [Header("---�X�L���̎�������---")]
        [SerializeField] private float skillDuration;      // �X�L���̎������ԁi�b�j
        [SerializeField] private float burstInterval;    // �e�����Ԋu�i�b�j

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
            // �v���C���[�̌����ɒǏ]�i�K�v�ɉ����āj
            Vector2 direction = transform.right; // �L�����N�^�[�̉E����(�������Ă����)�Ɍ���

            if (switchPlayer != null && switchPlayer.isTrigger)
            {
                TryUseSkill();
                switchPlayer.ResetTrigger();
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
            if (skillEffectPrefab && firePoint)
            {
                StartCoroutine(FireSkillDuringTime());
            }
        }

        private IEnumerator FireSkillDuringTime()
        {
            float elapsed = 0f;
            while (elapsed < skillDuration)
            {
                GameObject bullet = Instantiate(skillEffectPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = firePoint.right * bulletSpeed;
                }

                // �����ˎ���SE�Đ�
                SoundManager.Instance.PlaySE(SESoundData.SE.Example);

                yield return new WaitForSeconds(burstInterval);
                elapsed += burstInterval;
            }
        }
    }
}
