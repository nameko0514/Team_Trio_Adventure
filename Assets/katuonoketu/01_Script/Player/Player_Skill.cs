using UnityEngine;

namespace Takato
{
    public class Player_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("---�D���ȃX�L����GameObject����ĂˁI---")]
        [SerializeField] private GameObject skillEffectPrefab;

        [Header("---FirePoint������ĂˁI(���GameObject)---")]
        [SerializeField] private Transform firePoint;

        void Start()
        {
            // SwitchPlayer��FindFirstObjectByType�Ŏ擾����B
            switchPlayer = Object.FindFirstObjectByType<SwitchPlayer>();
        }

        void Update()
        {
            if (switchPlayer != null && switchPlayer.isTrigger)
            {
                UseSkill();
                switchPlayer.ResetTrigger(); // �g���K�[�����Z�b�g���đ��d������h��
            }
        }

        public void UseSkill()
        {
            if (skillEffectPrefab && firePoint)
            {
                int bulletCount = 5; // �����΂�܂���
                float spreadAngle = 30f; // �S�̂ŉ��x���炢�ɂ΂炯�邩

                for (int i = 0; i < bulletCount; i++)
                {
                    // ���ˊp�x���v�Z
                    float angle = -spreadAngle / 2f + (spreadAngle / (bulletCount - 1)) * i;

                    // �e�𐶐�
                    GameObject bullet = Instantiate(skillEffectPrefab, firePoint.position, firePoint.rotation);

                    // �p�x�����炷
                    bullet.transform.Rotate(0, 0, angle);

                    // �O���ɗ͂�������i�e�� Rigidbody2D �����Ă�O��j
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = bullet.transform.right * 5f; // �e���i5�͍D���ȑ����ɂ���OK�j
                    }
                }

                Debug.Log($"{gameObject.name} �X�L�������I�i�U�e�j");
            }
        }

    }
}
