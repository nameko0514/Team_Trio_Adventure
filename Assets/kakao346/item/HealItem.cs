using UnityEngine;
using fujiiYuma;


namespace Gishi
{
    public class HealItem : MonoBehaviour
    {
        [SerializeField] private int heal = 1; //�񕜗�


        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                //player.Heal(heal);        //�񕜏���
                Destroy(gameObject);      //�A�C�e������
            }
        }
    }

}
