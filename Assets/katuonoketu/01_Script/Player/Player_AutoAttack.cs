using matumoto;
using UnityEngine;

namespace Takato
{
    public class Player_AutoAttack : MonoBehaviour
    {
        [Tooltip("----’e‚ÌÚ×İ’è----")]
        [Header("---’e‚ğ“ü‚ê‚é‚Æ‚±‚ë---")]
        [SerializeField] private GameObject bulletPrefab;  //’e‚ÌPrefab

        [Header("---’e‚Ì”­ËêŠ“ü‚ê‚é‚Æ‚±‚ë---")]
        [SerializeField] private Transform firePoint;@@ //’e‚Ì”­ËˆÊ’u

        [Header("---˜AË‘¬“x---")]
        [SerializeField] private float fireRate;         //’e‚Ì”­ËŠÔŠu(•b)

        [Header("---UŒ‚”ÍˆÍw’è---")]
        [SerializeField] private float attackRange;      //UŒ‚”ÍˆÍ(”¼Œa)


        private float nextFireTime;

        //ƒLƒƒƒ‰‚ÌŒü‚«æ“¾—p
        private float direction = 1f;

        private void Update()
        {
            //ƒLƒƒƒ‰‚ÌŒü‚«æ“¾(‰EŒü‚«‚È‚ç1, ¶Œü‚«‚È‚ç - 1)
            direction = transform.localScale.x > 0 ? 1f : -1f;

            GameObject nearestEnemy = FindNerestEnemyWithTag("Enemy");

            //”ÍˆÍ“à‚Ì“G‚ğ’T‚·
            if (nearestEnemy != null)
            {
                float distance = Vector2.Distance(transform.position, nearestEnemy.transform.position);
                if (distance <= attackRange && Time.time >= nextFireTime)
                {
                    Fire();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }

        //’e‚Ì”­Ë‚Ìˆ—
        private void Fire()
         {
            SoundManager.Instance.PlaySE(SESoundData.SE.None);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // firePoint ‚ªŒü‚¢‚Ä‚¢‚é•ûŒü‚É’e‚ğ”ò‚Î‚·
            rb.linearVelocity = firePoint.right * 10f;

            // ’e‚ÌŒ©‚½–Ú‚àŒü‚«‡‚í‚¹‚½‚¢‚È‚ç
            bullet.transform.right = firePoint.right;
          }

        //ƒ^ƒO‚ğŒŸ’m‚·‚é‚Æ‚±‚ë
        GameObject FindNerestEnemyWithTag(string tag)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            GameObject nearest = null;
            float minDist = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                Vector2 toEnemy = enemy.transform.position - transform.position;
                float dist = toEnemy.sqrMagnitude;   // ‹——£‚Ì“ñæ‚ğŒvZ

                //ƒvƒŒƒCƒ„[‚ÌŒü‚«‚É“G‚ª‚¢‚é‚©‚Ç‚¤‚©
                float dot = Vector2.Dot(transform.right, (enemy.transform.position - transform.position).normalized);

                if (dot < 0) continue;    // ƒvƒŒƒCƒ„[‚ÌŒü‚«‚Æ‹t•ûŒü‚É“G‚ª‚¢‚éê‡‚ÍƒXƒLƒbƒv

                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = enemy;
                }
            }
            return nearest;
        }

        //Player‚ÌƒAƒ^ƒbƒN”ÍˆÍ‚Ì•\¦
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
