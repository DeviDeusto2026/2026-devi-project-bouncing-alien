using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform target;

    [Header("Ráfaga")]
    public int bulletsPerBurst = 5;
    public float timeBetweenBullets = 0.15f;

    [Header("Pausa")]
    public float timeBetweenBursts = 3f;

    private int bulletsShot;
    private float timer;
    private bool resting;

    void Update()
    {
        if (bulletPrefab == null || firePoint == null || target == null)
            return;

        firePoint.LookAt(target);

        timer += Time.deltaTime;

        if (resting)
        {
            if (timer >= timeBetweenBursts)
            {
                resting = false;
                bulletsShot = 0;
                timer = 0f;
            }

            return;
        }

        if (timer >= timeBetweenBullets)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            bulletsShot++;
            timer = 0f;

            if (bulletsShot >= bulletsPerBurst)
            {
                resting = true;
                timer = 0f;
            }
        }
    }
}
