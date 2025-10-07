using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class MagicBulletwizard : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    private int damage;
    HealthEnemy healthEnemy;
    Spawnpoint spawnpoint;
    [SerializeField] private GameObject impactEffect;

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
        StartCoroutine(playanimationtime(0.3f));
    }
    private IEnumerator playanimationtime(float time)
    {
        impactEffect.SetActive(true);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
