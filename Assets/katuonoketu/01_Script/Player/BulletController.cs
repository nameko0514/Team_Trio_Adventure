using UnityEngine;

namespace Takato
{
    public class BulletController : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            //��ʊO�ɒe���o����e������
            Destroy(gameObject);
        }
    }
}

