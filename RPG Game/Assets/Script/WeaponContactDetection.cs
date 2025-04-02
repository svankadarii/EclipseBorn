using UnityEngine;

public class WeaponContactDetection : MonoBehaviour
{
    public int damage = 10; // Adjust damage as needed

    private void OnTriggerEnter(Collider other)
    {
            SpiderHealth enemyHealth = other.GetComponent<SpiderHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
    }
}
