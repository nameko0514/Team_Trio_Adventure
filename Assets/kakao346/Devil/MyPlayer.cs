using UnityEngine;

using fujiiYuma;
using System.Collections;

namespace Gishi
{
    public class MyPlayer : PlayerController,IReverseControl1
    {
        private bool isReversed = false;  // ���씽�]�����ǂ���

        protected override void Update()
        {
            // ���͂��擾
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // ���삪���]���Ă���ꍇ�A���͕������t�ɂ���
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

        // ���씽�]����莞�ԓK�p���A���ɖ߂��R���[�`��
        private IEnumerator ReverseCoroutine(float duration)
        {
            isReversed = true;  // ����𔽓]
            Debug.Log("���삪���]����܂����I");

            // duration�b�҂�
            yield return new WaitForSeconds(duration);

            isReversed = false;  // ��������ɖ߂�
            Debug.Log("���삪���ɖ߂�܂����I");
        }

    }

}

