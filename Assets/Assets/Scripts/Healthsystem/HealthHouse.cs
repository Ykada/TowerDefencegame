using UnityEngine;
using UnityEngine.UI;

public class HealthHouse : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currenthousehealth = 100;
    [SerializeField] private Slider healthBar;

    void Start()
    {
        currenthousehealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currenthousehealth;
    }
    void damageHouse(int damage)
    {
        currenthousehealth -= damage;
        healthBar.value = currenthousehealth;
        if (currenthousehealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }

}
