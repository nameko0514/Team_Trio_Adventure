using UnityEngine;

namespace fujiiYuma
{
    public class GhostScript : MonoBehaviour
    {
        [Header("----�ǔ��̑��x----")]
        [SerializeField] float moveSpeed = 3f;

        [Header("----player�Ƃ̍ŏ�����----")]
        [SerializeField] private float minDistance = 0.1f;

        //player��transform
        private Transform player;

        private void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            //player���ݒ肳��Ă��Ȃ��Ƃ��͂Ȃɂ����Ȃ�
            if (player == null) return;

            //player�Ƃ̋����̌v�Z
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //player�Ƃ̋������ŏ������ȏ�̏ꍇ�݈̂ړ�
            if(distanceToplayer > minDistance)
            {
                //���݂̈ʒu����Player�̈ʒu�Ɍ�������Lerp�ňړ�
                transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

        }
    }
}
