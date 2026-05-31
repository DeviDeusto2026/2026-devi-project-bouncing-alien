using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Ráfaga")]
    public int bulletsPerBurst = 5;
    public float timeBetweenBullets = 0.15f;

    [Header("Pausa")]
    public float timeBetweenBursts = 3f;

    private int bulletsShot;
    private float timer;
    private bool resting;

    void Start()
    {
        bulletsShot = 0;
        timer = 0f;
        resting = false;
    }

    void Update()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

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
            Shoot();
            bulletsShot++;
            timer = 0f;

            if (bulletsShot >= bulletsPerBurst)
            {
                resting = true;
                timer = 0f;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}