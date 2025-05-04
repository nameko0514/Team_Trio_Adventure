using UnityEngine;

public class HitTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
         Takato.BulletController bulletController = collision.GetComponent<Takato.BulletController>();
        if(bulletController != null)
        {
          //  currentHealth -= bulletController.GetDamage();
        }



        if (collision.CompareTag("Respawn"))
        {
            Debug.Log("THE END");
        }
        else
        {
            Debug.Log("HIT");
        }
            
    }
}
