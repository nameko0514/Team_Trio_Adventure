using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class WaspAttack : MonoBehaviour   //弾の処理
    {
        [SerializeField] private int damage = 1; // 弾のダメージ

        [SerializeField] private float lifeTime = 5f; // 弾が消えるまでの時間

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)   //プレイヤー判断
        {
            // PlayerController を持っていれば、プレイヤーと判断してダメージ
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // プレイヤーのHPを減らす

                Destroy(gameObject); // 弾も消す
            }

            // 地形に当たった場合も消す（Groundレイヤーにした方が良いらしい）
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }

}
