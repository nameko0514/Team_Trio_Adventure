using matumoto;
using UnityEngine;

namespace Takato
{
    public class Player_AutoAttack : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 1f;

        //�L�����̌����擾�p
         private float direction = 1f;

        void Start()
        {
            InvokeRepeating(nameof(Fire), 0f, 1f / fireRate);
        }

        private void Update()
        {
            //�L�����̌����擾(�E�����Ȃ�1, �������Ȃ� - 1)
            direction = transform.localScale.x > 0 ? 1f : -1f;
        }

        void Fire()
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.None);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // firePoint �������Ă�������ɒe���΂�
            rb.linearVelocity = firePoint.right * 10f;

            // �e�̌����ڂ��������킹�����Ȃ�
            bullet.transform.right = firePoint.right;
        }
    }
}
