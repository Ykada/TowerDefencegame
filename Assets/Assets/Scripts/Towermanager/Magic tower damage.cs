using UnityEngine;

public class Magictowerdamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject impactEffectPrefab;

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        if (target == null || !IsInRange(target))
        {
            UpdateTarget();
        }

        if (target == null) return;

        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    bool IsInRange(Transform enemy)
    {
        return Vector2.Distance(transform.position, enemy.position) <= range;
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
        if (target == null) return;

        Transform head = target.Find("Head");
        Vector3 effectPosition = head != null ? head.position : target.position;

        if (impactEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(impactEffectPrefab, effectPosition, Quaternion.identity);
            if (head != null)
            {
                effectInstance.transform.SetParent(head);
            }

            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(effectInstance, ps.main.duration);
            }
            else
            {
                Destroy(effectInstance, 2f);
            }
        }

        HealthEnemy health = target.GetComponent<HealthEnemy>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
