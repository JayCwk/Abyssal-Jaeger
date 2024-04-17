using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    private BulletPool bulletPool;
    private float bulletSpeed = 10f;

    private void Start()
    {
        bulletPool = GetComponent<BulletPool>();
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(0.3f); // Adjust the delay between shots
        }
    }

    private void Shoot()
    {
        GameObject bullet = bulletPool.GetBullet();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * bulletSpeed; // Set velocity to move upward
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true); // Activate the bullet after setting its initial position and rotation
    }
}
