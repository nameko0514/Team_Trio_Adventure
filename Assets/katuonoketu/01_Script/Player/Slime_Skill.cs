using UnityEngine;
using System.Collections;
using matumoto;

namespace Tkato
{
    public class Slime_Skill : MonoBehaviour
    {
        private SwitchPlayer switchPlayer;

        [Header("---好きなスキルのGameObject入れてね！---")]
        [SerializeField] private GameObject skillEffectPrefab;　// スキルのエフェクトPrefab

        [Header("---FirePointをいれてね！(空のGameObject)---")]
        [SerializeField] private Transform firePoint;  // 発射位置

        [Header("---スキルの発動間隔---")]
        [SerializeField] private float fireRate; // スキルの発動間隔

        [Header("---スキルの持続時間---")]
        [SerializeField] private float skillDuration;      // スキルの持続時間（秒）
        [SerializeField] private float burstInterval;    // 弾を撃つ間隔（秒）

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
            // プレイヤーの向きに追従（必要に応じて）
            Vector2 direction = transform.right; // キャラクターの右方向(＝向いてる方向)に撃つ

            if (switchPlayer != null && switchPlayer.isTrigger)
            {
                TryUseSkill();
                switchPlayer.ResetTrigger();
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

                // 毎発射時にSE再生
                SoundManager.Instance.PlaySE(SESoundData.SE.Example);

                yield return new WaitForSeconds(burstInterval);
                elapsed += burstInterval;
            }
        }
    }
}
