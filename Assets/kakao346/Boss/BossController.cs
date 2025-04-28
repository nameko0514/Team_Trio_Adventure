using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class BossController : MonoBehaviour
    {
        [Header("----追尾の速度----")]
        [SerializeField] float moveSpeed = 2f;

        [Header("----playerとの最小距離----")]
        [SerializeField] private float minDistance = 0.1f;

        [Header("----攻撃関連----")]
        [SerializeField] private float attackRange = 2f; //攻撃距離
        [SerializeField] private float attackCooldown = 4f; //クールタイム
        [SerializeField] private int damage = 7; //攻撃力

        [Header("----ボスの体力----")]
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
