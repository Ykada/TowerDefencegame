using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HouseHealth : MonoBehaviour
{
    [SerializeField] private int currenthealth = 100;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text healthText;
    [SerializeField] private Spawnpoint spawnpoint1;

    void Start()
    {
    }
    void Update()
    {
        healthBar.value = currenthealth;
        healthText.text = currenthealth.ToString() + " HP";
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        Debug.Log("House took " + damage + " damage. Current health: " + currenthealth);
        if (currenthealth <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("StartingScene");
        }
        spawnpoint1.gameObject.GetComponent<Spawnpoint>().EnemyDeathByHouse();
    }

}
