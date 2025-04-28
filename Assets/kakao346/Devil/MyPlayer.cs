using UnityEngine;

using fujiiYuma;
using System.Collections;

namespace Gishi
{
    public class MyPlayer : PlayerController,IReverseControl1
    {
        private bool isReversed = false;  // ‘€ì”½“]’†‚©‚Ç‚¤‚©

        protected override void Update()
        {
            // “ü—Í‚ğæ“¾
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // ‘€ì‚ª”½“]‚µ‚Ä‚¢‚éê‡A“ü—Í•ûŒü‚ğ‹t‚É‚·‚é
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

        // ‘€ì”½“]‚ğˆê’èŠÔ“K—p‚µAŒ³‚É–ß‚·ƒRƒ‹[ƒ`ƒ“
        private IEnumerator ReverseCoroutine(float duration)
        {
            isReversed = true;  // ‘€ì‚ğ”½“]
            Debug.Log("‘€ì‚ª”½“]‚³‚ê‚Ü‚µ‚½I");

            // duration•b‘Ò‚Â
            yield return new WaitForSeconds(duration);

            isReversed = false;  // ‘€ì‚ğŒ³‚É–ß‚·
            Debug.Log("‘€ì‚ªŒ³‚É–ß‚è‚Ü‚µ‚½I");
        }

    }

}

