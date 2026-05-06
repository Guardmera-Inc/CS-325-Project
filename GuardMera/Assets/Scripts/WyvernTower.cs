using UnityEngine;
using System.Collections;

public class WyvernTower : MonoBehaviour
{
    [Header("Figure Eight Movement")]
    // The node it was spawned on is the "anchor" center
    public Vector3 centerPoint; 
    
    // How wide is the eight?
    public float radiusX = 3f;  
    // How tall is the eight?
    public float radiusY = 1.5f; 
    // How fast does it fly the loop?
    public float flySpeed = 2f; 
    public float turnSpeed = 360f;

    [Header("Nova Burst Firing")]
    // The "bullet" or "wing flap" projectile
    public GameObject burstProjectilePrefab; 
    
    // Number of shots in the 360-degree circle
    public int projectilesPerBurst = 8; 
    
    // Time between bursts
    public float burstInterval = 1f; 

    // Used to track fly time smoothly
    private float timer = 0f;
    void Start()
    {
        centerPoint = transform.position;
        StartCoroutine(FireNovaBurstLoop());
        
    }

    void Update()
    {
        timer += Time.deltaTime * flySpeed;

        float currentX = Mathf.Cos(timer) * radiusX;
        float currentY = Mathf.Sin(timer * 2f) * radiusY;
        Vector3 currentPosition = centerPoint + new Vector3(currentX, currentY, 0f);

        // 3. Calculate NEXT position (just a tiny step ahead in time)
        float nextTimer = timer + 0.05f; 
        float nextX = Mathf.Cos(nextTimer) * radiusX;
        float nextY = Mathf.Sin(nextTimer * 2f) * radiusY;
        Vector3 nextPosition = centerPoint + new Vector3(nextX, nextY, 0f);

        // 4. Move to the current position
        transform.position = currentPosition;

        // 5. Smoothly rotate to look at the next position
        Vector3 moveDirection = nextPosition - currentPosition;
        if (moveDirection != Vector3.zero)
        {
            // Calculate the target angle
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            
            // Subtracting 90 degrees assumes your sprite naturally points "Up". 
            // Adjust this offset (-90f, 0f, 90f, 180f) depending on your sprite's default orientation!
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle); 

            // Smoothly rotate toward the target angle over time
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        
    }

    IEnumerator FireNovaBurstLoop()
    {
        while (true)
        {
            // Calculate how many degrees separate each shot
            float angleStep = 360f / projectilesPerBurst;
            float currentAngle = 0f;

            // Spawn each projectile in the circular burst
            for (int i = 0; i < projectilesPerBurst; i++)
            {
                // Create the rotation for this specific projectile
                Quaternion projRotation = Quaternion.Euler(0, 0, currentAngle);

                // Spawn the bullet right where the Wyvern is right now
                Instantiate(burstProjectilePrefab, transform.position, projRotation);

                // Move to the next angle spot
                currentAngle += angleStep;
            }

            // Wait until the next flap/burst
            yield return new WaitForSeconds(burstInterval);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(centerPoint, new Vector3(radiusX * 2, radiusY * 2, 0.1f));
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(centerPoint, 0.2f);
    }




}
