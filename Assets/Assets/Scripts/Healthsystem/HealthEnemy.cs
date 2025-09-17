using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    HealthHouse healthHouse;
    Spawnpoint spawnpoint;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Diebeforehome();
        }
    }

    void Diebeforehome()
    {
        spawnpoint = GameObject.FindGameObjectWithTag("Spawnpoint").GetComponent<Spawnpoint>();
        spawnpoint.ennemydeath();
        Die();
    }

    void Die()
    {
        healthHouse.BroadcastMessage("damageHouse", currentHealth);
        Destroy(gameObject);
    }
}
