using fujiiYuma;
using UnityEngine;

namespace Gishi {
    public class GoburnContolor : MonoBehaviour
    {
        [Header("----追尾の速度----")]
        [SerializeField] float moveSpeed = 3f;

        [Header("----playerとの最小距離----")]
        [SerializeField] private float minDistance = 0.1f;

        //playerのtransform
        private Transform player;

        [Header("----Ghostの有/無の切り替え----")]
        [SerializeField] private float waitTime = 10f;

        [Header("----Ghostの消える/登場するspeed----")]
        [SerializeField] private float switchSpeed = 1f;

        private float time = 0f;

        private bool isChangeFlag = false;


        public float Speed = 2f;               // 敵の移動速度

        public float attackRange = 1.5f;           // 攻撃範囲

        public float attackCooldowntime = 1f;          // 攻撃のクールダウン時間

        public int Damage = 10;              // 攻撃力

        private float lastAttackTime = 0f;         // 最後に攻撃した時間
        private void Update()
        {
            if (player == null) return;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * Speed * Time.deltaTime;
            }

            else if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldowntime)
            {
                Attack();
                lastAttackTime = Time.time;
            }


            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            Debug.Log(player);

            //playerが設定されていないときはなにもしない
            if (player == null) return;

            //playerとの距離の計算
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //playerとの距離が最小距離以上の場合のみ移動
            if (distanceToplayer > minDistance)
            {
                //現在の位置からPlayerの位置に向かってLerpで移動
                transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

        
    }

        void Attack()
        {
            if (player != null)
            {
                //player.GetComponent<PlayerController>(Takk).;
                Debug.Log("プレイヤーに攻撃！ ダメージ: " + Damage);
            }
        }
    }
}
