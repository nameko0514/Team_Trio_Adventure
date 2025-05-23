using UnityEngine;

using fujiiYuma;
using System.Collections;

namespace Gishi
{
    public class MyPlayer : PlayerController,IReverseControl1
    {
        private bool isReversed = false;  // 操作反転中かどうか

        protected override void Update()
        {
            // 入力を取得
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 操作が反転している場合、入力方向を逆にする
            if (isReversed)
            {
                horizontal *= -1;
                vertical *= -1;
            }

            moveInput.x = horizontal;
            moveInput.y = vertical;
        }

        public void ApplyReverseControl(float duration)
        {
            StartCoroutine(ReverseCoroutine(duration));
        }

        // 操作反転を一定時間適用し、元に戻すコルーチン
        private IEnumerator ReverseCoroutine(float duration)
        {
            isReversed = true;  // 操作を反転
            Debug.Log("操作が反転されました！");

            // duration秒待つ
            yield return new WaitForSeconds(duration);

            isReversed = false;  // 操作を元に戻す
            Debug.Log("操作が元に戻りました！");
        }

    }

}

