using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class BossController : MonoBehaviour
    {
        [Header("----�ǔ��̑��x----")]
        [SerializeField] float moveSpeed = 2f;

        [Header("----player�Ƃ̍ŏ�����----")]
        [SerializeField] private float minDistance = 0.1f;

        [Header("----�U���֘A----")]
        [SerializeField] private float attackRange = 2f; //�U������
        [SerializeField] private float attackCooldown = 4f; //�N�[���^�C��
        [SerializeField] private int damage = 7; //�U����

        [Header("----�{�X�̗̑�----")]
        [SerializeField] private int maxHealth = 20;


        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
