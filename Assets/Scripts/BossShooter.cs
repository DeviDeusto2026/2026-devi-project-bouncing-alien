using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform target;

    [Header("Burst Settings")]
    public int bulletsPerBurst = 5;
    public float timeBetweenBullets = 0.15f;

    [Header("Cooldown Settings")]
    public float cooldownDuration = 3f;

    private int burstCounter;
    private float timer;
    private bool isResting;

    void Update()
    {
        if (bulletPrefab == null || firePoint == null || target == null)
            return;

        firePoint.LookAt(target);

        timer += Time.deltaTime;

        if (isResting)
        {
            HandleCooldown();
            return;
        }

        HandleBurstShooting();
    }

    private void HandleCooldown()
    {
        if (timer >= cooldownDuration)
        {
            isResting = false;
            burstCounter = 0;
            timer = 0f;
        }
    }

    private void HandleBurstShooting()
    {
        if (timer >= timeBetweenBullets)
        {
            Shoot();
            burstCounter++;
            timer = 0f;

            if (burstCounter >= bulletsPerBurst)
            {
                isResting = true;
                timer = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(newBullet, 5f);
    }
}