using TMPro;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public GameObject healthHouse;
    public GameObject spawnpoint1;
    [SerializeField] private TextMeshProUGUI healthText;


    void Start()
    {
        currentHealth = maxHealth;
        healthHouse = GameObject.FindWithTag("Player");
        spawnpoint1 = GameObject.FindWithTag("Spawnpoint");
    }
    void Update()
    {
        healthText.text = currentHealth.ToString();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (healthHouse != null)
            {
                healthHouse.GetComponent<HouseHealth>().TakeDamage(currentHealth);
            }
            Destroy(gameObject);
        }
    }
}
