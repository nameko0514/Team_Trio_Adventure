using matumoto;
using UnityEngine;
using static matumoto.SESoundData;

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

        [Header("---弾のスピード---")]
        [SerializeField] private float bulletSpeed; // 弾速

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
            Vector2 direction = transform.right; // キャラクターの右方向(＝向いてる方向)に撃つ

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
            // 毎発射時にSE再生
            SoundManager.Instance.PlaySE(SESoundData.SE.None);

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
                        rb.linearVelocity = bullet.transform.right * bulletSpeed; // 弾速
                    }
                }

                Debug.Log($"{gameObject.name} スキル発動！（散弾）");
            }
        }

    }
}
