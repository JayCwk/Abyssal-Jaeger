using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 20;
    private List<GameObject> bulletPool;
    private float bulletSpeed = 10f;

    private void Start()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (bullet != null && !bullet.activeInHierarchy)
            {
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.up * bulletSpeed; // Set velocity to move upward
                bullet.SetActive(true);
                return bullet;
            }
        }

        // If no inactive bullet is found or some bullets are destroyed, expand the pool by instantiating a new bullet
        GameObject newBullet = Instantiate(bulletPrefab);
        Rigidbody2D newRb = newBullet.GetComponent<Rigidbody2D>();
        newRb.velocity = Vector2.up * bulletSpeed; // Set velocity to move upward
        newBullet.SetActive(true);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
