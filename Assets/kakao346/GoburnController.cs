using UnityEngine;

namespace Gishi {
    public class GoburnContolor : PlayerController
    {
        private Transform player;

        public float Speed = 2f;               // 敵の移動速度

        public float attackRange = 1.5f;           // 攻撃範囲

        public float attackCooldowntime = 1f;          // 攻撃のクールダウン時間

        public int Damage = 10;              // 攻撃力

        private float lastAttackTime = 0f;         // 最後に攻撃した時間
        protected override void Update()
        {
            base.Update();
            if (player == null) return;

            //float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            //if (distanceToPlayer > attackRange)
            //{
            //    //Vector2 direction = (player.position - transform.position).normalized;
            //    //transform.position += (Vector3)direction * Speed * Time.deltaTime;
            //}

            //else if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldowntime)
            //{
            //    Attack();
            //    lastAttackTime = Time.time;
            //}
        }

        void Attack()
        {
            if (player != null)
            {
                //player.GetComponent<>().TakeDamage(Damage);
                Debug.Log("プレイヤーに攻撃！ ダメージ: " + Damage);
            }
        }
    }
}
