using UnityEngine;

namespace Takato
{
    public class BulletController : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            //‰æ–ÊŠO‚É’e‚ªo‚½‚ç’e‚ğÁ‚·
            Destroy(gameObject);
        }
    }
}

