using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    HealthHouse healthHouse;
    public GameObject spawnpoint1;

    void Start()
    {
        currentHealth = maxHealth;
        spawnpoint1 = GameObject.FindWithTag("Spawnpoint");
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
        spawnpoint1.gameObject.GetComponent<Spawnpoint>().ennemydeath();
        Destroy(gameObject);
    }

    void Die()
    {
        healthHouse.BroadcastMessage("damageHouse", currentHealth);
        Destroy(gameObject);
    }
}
