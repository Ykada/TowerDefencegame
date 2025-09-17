using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab1;
    [SerializeField] private GameObject towerPrefab2;
    [SerializeField] private GameObject towerPrefab3;

    [SerializeField] private int money = 500;
    [SerializeField] private Text moneytext;
    [SerializeField] private int maxtowers = 25;






    private void Start()
    {
        moneytext.text = "Money: " + money.ToString();
    }

    void showingplacementtower(int towerNumber)
    {
        Vector3 MousePos = Input.mousePosition;
        MousePos.z = 10f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(MousePos);
        transform.position = worldPosition;
        if (towerNumber == 1 && Input.GetMouseButtonDown(0))
        {
            placetower(towerPrefab1, 100);
        }
        else if (towerNumber == 2 && Input.GetMouseButtonDown(0))
        {
            placetower(towerPrefab2, 200);
        }
        else if (towerNumber == 3 && Input.GetMouseButtonDown(0))
        {
            placetower(towerPrefab3, 300);
        }
    }

        void placetower(GameObject towerPrefab, int cost)
    {
        if (money >= cost)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            money -= cost;
            moneytext.text = "Money: " + money.ToString();
        }
        else
        {
            Debug.Log("Not enough money to place tower!");
        }
    }
}
