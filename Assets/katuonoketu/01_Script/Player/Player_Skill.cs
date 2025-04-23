using UnityEngine;

namespace Takato
{
    public class Player_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("好きなスキルのGameObject入れてね！")]
        [SerializeField] private GameObject skillEffectPrefab;

        [Header("FirePointをいれてね！(空のGameObject)")]
        [SerializeField] private Transform firePoint;

        void Start()
        {
            // SwitchPlayerをGetConponentする。
            switchPlayer = GetComponent<SwitchPlayer>();

        }

        void Update()
        {
            if (switchPlayer != null && switchPlayer.isTrigger)
            {
                UseSkill();
                switchPlayer.ResetTrigger(); // トリガーをリセットして多重発動を防ぐ
            }
        }

        public void UseSkill()
        {
            if (skillEffectPrefab && firePoint)
            {
                Instantiate(skillEffectPrefab, firePoint.position, firePoint.rotation);
                Debug.Log($"{gameObject.name} スキル発動！");
            }
        }
    }
}
