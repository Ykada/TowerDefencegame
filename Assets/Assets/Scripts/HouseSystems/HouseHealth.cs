using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// HouseHealth.cs
// Manages the health of the house, updates UI elements, and handles game over state.
// Ykada_Hiroka
public class HouseHealth : MonoBehaviour
{
    [SerializeField] private int currenthealth = 100;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text healthText;
    [SerializeField] private Spawnpoint spawnpoint1;
    [SerializeField] private GameObject gameovermenu;
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
            Cursor.lockState = CursorLockMode.None;
            gameovermenu.SetActive(true);
        }
        spawnpoint1.gameObject.GetComponent<Spawnpoint>().EnemyDeathByHouse();
    }

}

// © 2025 YKΛDΛ_. All rights reserved.