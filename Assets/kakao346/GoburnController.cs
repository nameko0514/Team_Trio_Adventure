using UnityEngine;

namespace Gishi {
    public class GoburnContolor : PlayerController
    {
        private Transform player;

        public float Speed = 2f;               // �G�̈ړ����x

        public float attackRange = 1.5f;           // �U���͈�

        public float attackCooldowntime = 1f;          // �U���̃N�[���_�E������

        public int Damage = 10;              // �U����

        private float lastAttackTime = 0f;         // �Ō�ɍU����������
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
                Debug.Log("�v���C���[�ɍU���I �_���[�W: " + Damage);
            }
        }
    }
}
