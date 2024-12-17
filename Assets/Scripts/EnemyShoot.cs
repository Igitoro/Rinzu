using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform gunTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingRange = 5f;
    [SerializeField] private float shootingInterval = 1f;
    [SerializeField] private float bulletSpeed = 10f;

    private Transform playerTransform;
    private float timeSinceLastShot = 0f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= shootingRange)
        {
            timeSinceLastShot += Time.deltaTime;

            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        bulletRigidbody.velocity = direction * bulletSpeed;
    }
}
