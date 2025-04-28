using UnityEngine;

namespace Takato
{
    public class Player_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("---好きなスキルのGameObject入れてね！---")]
        [SerializeField] private GameObject skillEffectPrefab;

        [Header("---FirePointをいれてね！(空のGameObject)---")]
        [SerializeField] private Transform firePoint;

        void Start()
        {
            // SwitchPlayerをFindFirstObjectByTypeで取得する。
            switchPlayer = Object.FindFirstObjectByType<SwitchPlayer>();
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
                int bulletCount = 5; // 何発ばらまくか
                float spreadAngle = 30f; // 全体で何度くらいにばらけるか

                for (int i = 0; i < bulletCount; i++)
                {
                    // 発射角度を計算
                    float angle = -spreadAngle / 2f + (spreadAngle / (bulletCount - 1)) * i;

                    // 弾を生成
                    GameObject bullet = Instantiate(skillEffectPrefab, firePoint.position, firePoint.rotation);

                    // 角度をずらす
                    bullet.transform.Rotate(0, 0, angle);

                    // 前方に力を加える（弾に Rigidbody2D がついてる前提）
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = bullet.transform.right * 5f; // 弾速（5は好きな速さにしてOK）
                    }
                }

                Debug.Log($"{gameObject.name} スキル発動！（散弾）");
            }
        }

    }
}
