using UnityEngine;

namespace fujiiYuma
{
    public class GhostScript : MonoBehaviour
    {
        [Header("----追尾の速度----")]
        [SerializeField] float moveSpeed = 3f;

        [Header("----playerとの最小距離----")]
        [SerializeField] private float minDistance = 0.1f;

        //playerのtransform
        private Transform player;

        private void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            //playerが設定されていないときはなにもしない
            if (player == null) return;

            //playerとの距離の計算
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //playerとの距離が最小距離以上の場合のみ移動
            if(distanceToplayer > minDistance)
            {
                //現在の位置からPlayerの位置に向かってLerpで移動
                transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

        }
    }
}
