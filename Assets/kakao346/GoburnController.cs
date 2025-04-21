using fujiiYuma;
using UnityEngine;

namespace Gishi {
    public class GoburnContolor : MonoBehaviour
    {
        [Header("----�ǔ��̑��x----")]
        [SerializeField] float moveSpeed = 3f;

        [Header("----player�Ƃ̍ŏ�����----")]
        [SerializeField] private float minDistance = 0.1f;

        //player��transform
        private Transform player;

        [Header("----Ghost�̗L/���̐؂�ւ�----")]
        [SerializeField] private float waitTime = 10f;

        [Header("----Ghost�̏�����/�o�ꂷ��speed----")]
        [SerializeField] private float switchSpeed = 1f;

        private float time = 0f;

        private bool isChangeFlag = false;


        public float Speed = 2f;               // �G�̈ړ����x

        public float attackRange = 1.5f;           // �U���͈�

        public float attackCooldowntime = 1f;          // �U���̃N�[���_�E������

        public int Damage = 10;              // �U����

        private float lastAttackTime = 0f;         // �Ō�ɍU����������
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

            //player���ݒ肳��Ă��Ȃ��Ƃ��͂Ȃɂ����Ȃ�
            if (player == null) return;

            //player�Ƃ̋����̌v�Z
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //player�Ƃ̋������ŏ������ȏ�̏ꍇ�݈̂ړ�
            if (distanceToplayer > minDistance)
            {
                //���݂̈ʒu����Player�̈ʒu�Ɍ�������Lerp�ňړ�
                transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

        
    }

        void Attack()
        {
            if (player != null)
            {
                //player.GetComponent<PlayerController>(Takk).;
                Debug.Log("�v���C���[�ɍU���I �_���[�W: " + Damage);
            }
        }
    }
}
