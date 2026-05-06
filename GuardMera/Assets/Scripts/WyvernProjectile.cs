using UnityEngine;

public class WyvernProjectile : MonoBehaviour
{
    public float speed = 15f; // Fast, short-lived projectiles
    public float lifeSpan = 0.5f; // "Pop" quickly, limited range
    public int damage = 20;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemyScript = collision.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }
            
            // Destroy immediately on hit
            Destroy(gameObject);
        }
    }
}
