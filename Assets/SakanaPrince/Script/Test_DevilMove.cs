using fujiiYuma;
using UnityEngine;

namespace matumoto
{
    public class Test_DevilMove : MonoBehaviour
    {
        [SerializeField] private int damage = 1;  //�_���[�W
        [SerializeField] private float debuffTime = 3f; //�f�o�t����


        Transform player;

        Animator animator;

        float moveSpeed = 2;



        private void Awake()
        {
            animator = GetComponent<Animator>();
         
        }
        private void FixedUpdate()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            //Debug.Log(player);

            //player���ݒ肳��Ă��Ȃ��Ƃ��͂Ȃɂ����Ȃ�
            if (player == null) return;

            //player�Ƃ̋����̌v�Z
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //player�Ƃ̋������ŏ������ȏ�̏ꍇ�݈̂ړ�
            if (distanceToplayer > 0.1f)
            {
                //���݂̈ʒu����Player�̈ʒu�Ɍ�������Lerp�ňړ�
                transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            //�A�j���[�V�����J��
            SetAnime();

            //���E�̌����ڔ��]
            if(player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

        }
        private void SetAnime()
        {
            if (player.transform.position.x - transform.position.x >
                player.transform.position.y - transform.position.y )
            {
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                animator.SetBool("hori", true);
            }
            else
            {

                if (player.transform.position.y > transform.position.y)
                {
                    animator.SetBool("up", true);
                    animator.SetBool("down", false);
                    animator.SetBool("hori", false);
                }
                else
                {
                    animator.SetBool("up", false);
                    animator.SetBool("down", true);
                    animator.SetBool("hori", false);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //layerController���p�������v���C���[�ɓ���������
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Devil: �v���C���[�Ƀq�b�g�I");

                player.TakeDamage(damage); // P�̗͂����炷

                // �v���C���[�����씽�]�������Ă��邩�m�F���ĕt�^
                if (player is IReverseControl1 reverseControl)�@�@�@//IReverseControl1��Scripts�t�H���_�[�ɒǉ����Ăق����ł�
                {
                    reverseControl.ApplyReverseControl(debuffTime);  // ���]�f�o�t��^����
                }
            }
        }
    }
}