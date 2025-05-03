using UnityEngine;

namespace Takato
{
    public class Player_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("---好きなスキルのGameObject入れてね！---")]
        [SerializeField] private GameObject skillEffectPrefab;　// スキルのエフェクトPrefab

        [Header("---FirePointをいれてね！(空のGameObject)---")]
        [SerializeField] private Transform firePoint;  // 発射位置

        [Header("---弾の飛ばす数")]
        [SerializeField] private int bulletCount; // 何発ばらまくか

        [Header("---弾の飛ばす角度")]
        [SerializeField] private float Angle; // スキルを飛ばす角度

        [Header("---スキルの発動間隔---")]
        [SerializeField] private float fireRate; // スキルの発動間隔


        private float lastFireTime = -Mathf.Infinity; // 最後のスキルの発動時間

        void Start()
        {
            // SwitchPlayerをFindFirstObjectByTypeで取得する。
            switchPlayer = Object.FindFirstObjectByType<SwitchPlayer>();

            if (switchPlayer == null)
            {
                Debug.LogError("SwitchPlayerがシーン上に見つかりませんでした！");
            }
        }

        void Update()
        {
            if (switchPlayer != null && switchPlayer.isTrigger)
            {
                TryUseSkill();
                switchPlayer.ResetTrigger(); // トリガーをリセットして多重発動を防ぐ
            }
        }

        void TryUseSkill()
        {
            // スキルの発動間隔をチェック
            if (Time.time - lastFireTime >= fireRate)
            {
                lastFireTime = Time.time; // 最後の発動時間を更新
                UseSkill();
            }
        }

        public void UseSkill()
        {
            if (skillEffectPrefab && firePoint)
            {
                int bulletcount = bulletCount; // 何発ばらまくか
                float spreadAngle = Angle; // 全体で何度くらいにばらけるか

                for (int i = 0; i < bulletCount; i++)
                {
                    // 発射角度を計算
                    float angle = (bulletcount > 1)
                     ? -spreadAngle / 2f + (spreadAngle / (bulletcount - 1)) * i
                     : 0f;

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
