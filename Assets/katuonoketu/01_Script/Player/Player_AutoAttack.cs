using matumoto;
using UnityEngine;

namespace Takato
{
    public class Player_AutoAttack : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 1f;

        //キャラの向き取得用
         private float direction = 1f;

        void Start()
        {
            InvokeRepeating(nameof(Fire), 0f, 1f / fireRate);
        }

        private void Update()
        {
            //キャラの向き取得(右向きなら1, 左向きなら - 1)
            direction = transform.localScale.x > 0 ? 1f : -1f;
        }

        void Fire()
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.None);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // firePoint が向いている方向に弾を飛ばす
            rb.linearVelocity = firePoint.right * 10f;

            // 弾の見た目も向き合わせたいなら
            bullet.transform.right = firePoint.right;
        }
    }
}
