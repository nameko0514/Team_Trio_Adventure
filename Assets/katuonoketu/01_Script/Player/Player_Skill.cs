using UnityEngine;

namespace Takato
{
    public class Player_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("�D���ȃX�L����GameObject����ĂˁI")]
        [SerializeField] private GameObject skillEffectPrefab;

        [Header("FirePoint������ĂˁI(���GameObject)")]
        [SerializeField] private Transform firePoint;

        void Start()
        {
            // SwitchPlayer��GetConponent����B
            switchPlayer = GetComponent<SwitchPlayer>();

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
                Instantiate(skillEffectPrefab, firePoint.position, firePoint.rotation);
                Debug.Log($"{gameObject.name} �X�L�������I");
            }
        }
    }
}
