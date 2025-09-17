using UnityEngine;

public class TowerDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float range = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 1f;

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        UpdateTarget();

        if (target == null) return;

        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ennemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        target = nearestEnemy != null ? nearestEnemy.transform : null;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target, damageAmount);
        }
    }
}
