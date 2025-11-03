using UnityEngine;

// Bullet.cs
// Manages the behavior of bullets fired by towers, including seeking targets and applying damage.
// Ykada_Hiroka

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    private int damage;
    HealthEnemy healthEnemy;
    Spawnpoint spawnpoint;

    public void Seek(Transform _target, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void HitTarget()
    {
        HealthEnemy enemyHealth = target.GetComponent<HealthEnemy>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);

        }

        Destroy(gameObject);
    }
}

// © 2025 YKΛDΛ_. All rights reserved.