using fujiiYuma;
using System.Collections;
using Takato;
using UnityEngine;

namespace matumoto
{

[RequireComponent(typeof(Rigidbody2D))]
    public class Test_DevilMove : MonoBehaviour
    {
        [Header("�X�e�[�^�X")]
        [SerializeField] private int maxHealth = 3;          // HP
        [SerializeField] private float moveSpeed = 2f;       // �X�s�[�h
        [SerializeField] private int damage = 1;  //�_���[�W
        [SerializeField] private float debuffTime = 3f; //�f�o�t����

        private int currentHealth;                           // ���݂�HP

        private bool stoperOfDobuleDead = false;

        [SerializeField] bool testInstaZeroLife = false;

        [SerializeField] private Camera mainCamera;

        private Transform player;

        private Rigidbody2D rb;

        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();

            rb = GetComponent<Rigidbody2D>();

            if (mainCamera == null) mainCamera = Camera.main;

        }

        private void Start()
        {
            currentHealth = maxHealth;

        }

        private void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            
        }

        private void FixedUpdate()
        {
            if (!IsObjectInCameraView())
            {
                return;
            }

            if (player == null)
            {
                return;
            }

            // �v���C���[��ǔ�
            if (player != null)
            {
                if(stoperOfDobuleDead == false)
                {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

                }
                else
                {
                    moveSpeed = 0;
                }
            }

            //�A�j���[�V�����J��
            SetAnime();

            //���E�̌����ڔ��]
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (testInstaZeroLife)
            {
                Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //layerController���p�������v���C���[�ɓ���������
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null  && currentHealth > 0)
            {
                Debug.Log("Devil: �v���C���[�Ƀq�b�g�I");

                player.TakeDamage(damage); // P�̗͂����炷

                // �v���C���[�����씽�]�������Ă��邩�m�F���ĕt�^
                if (player is IReverseControl1 reverseControl)�@�@�@//IReverseControl1��Scripts�t�H���_�[�ɒǉ����Ăق����ł�
                {
                    reverseControl.ApplyReverseControl(debuffTime);  // ���]�f�o�t��^����
                }
            }

            //if (other.CompareTag("PlayerAttack"))
            //{
            //    if (other.TryGetComponent<BUkketTest>(out var bullet))
            //    {
            //        if (bullet != null)
            //        {
            //            currentHealth -= (int)bullet.GetDamage();
            //        }
            //    }

            //}

            Takato.BulletController bulletController = other.GetComponent<Takato.BulletController>();
            if (bulletController != null)
            {
              
                currentHealth = Mathf.Max(currentHealth - (int)bulletController.GetDamage(), 0);
                //Debug.Log("SKEED");

                if (currentHealth == 0)
                {
                    Die(); //���S����
                }
            }

            //BUkketTest bUkketTest = other.GetComponent<BUkketTest>();
            //if(bUkketTest != null)
            //{
            //    currentHealth = Mathf.Max(currentHealth - bUkketTest.GetDamage(), 0); 
            //    Debug.Log("SKEED");

            //    if (currentHealth == 0)
            //    {
            //        Die(); //���S����
            //    }
            //}

        }

  

        private void Die()
        {
            Debug.Log("Devil: �|���ꂽ�I");

            if (stoperOfDobuleDead == false)
            {
            SoundManager.Instance.PlaySE(SESoundData.SE.Damage);
                stoperOfDobuleDead = true;
            StartCoroutine(KnockAnime());
            }
         
        }

        IEnumerator KnockAnime()
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.EnemyKnockOut);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("hori", false);
            animator.SetBool("knock", true);
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private bool IsObjectInCameraView()
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

            return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        }

        private void SetAnime()
        {
            if (player.transform.position.x - transform.position.x >
                player.transform.position.y - transform.position.y)
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
    }

    //[SerializeField] private int damage = 1;  //�_���[�W
    //    [SerializeField] private float debuffTime = 3f; //�f�o�t����


    //    Transform player;

    //    Animator animator;

    //    float moveSpeed = 2;



      
        //private void FixedUpdate()
        //{
        //    player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //    //Debug.Log(player);

        //    //player���ݒ肳��Ă��Ȃ��Ƃ��͂Ȃɂ����Ȃ�
        //    if (player == null) return;

        //    //player�Ƃ̋����̌v�Z
        //    float distanceToplayer = Vector2.Distance(transform.position, player.position);

        //    //player�Ƃ̋������ŏ������ȏ�̏ꍇ�݈̂ړ�
        //    if (distanceToplayer > 0.1f)
        //    {
        //        //���݂̈ʒu����Player�̈ʒu�Ɍ�������Lerp�ňړ�
        //        transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
        //    }

    }
