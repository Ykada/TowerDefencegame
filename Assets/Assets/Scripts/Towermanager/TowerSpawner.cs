using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject Archer;
    [SerializeField] private GameObject Crossbow;
    [SerializeField] private GameObject Mage;
    [SerializeField] private GameObject Cannon;
    [SerializeField] private GameObject Minnigunner;
    [SerializeField] private GameObject Gunner;

    [Header("Settings")]
    [SerializeField] private LayerMask placementLayer;
    [SerializeField] private int maxTowers = 25;

    [Header("UI")]
    [SerializeField] private Text moneyText;

    private int money = 500;
    private GameObject currentTower;
    private int currentCost;

    private void Start()
    {
        UpdateMoneyUI();
    }

    private void Update()
    {
        HandleTowerSelection();
        HandleTowerPlacement();
    }

    public void ennemydeath1(int money2)
    {
        money += money2;
        UpdateMoneyUI();
    }
    #region towerlist
    private void HandleTowerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TrySelectTower(Archer, 250);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TrySelectTower(Crossbow, 500);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TrySelectTower(Mage, 750);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TrySelectTower(Cannon, 1500);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TrySelectTower(Minnigunner, 3500);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TrySelectTower(Gunner, 500);
        }
    }
    #endregion

    private void TrySelectTower(GameObject prefab, int cost)
    {
        if (money >= cost)
        {
            if (currentTower != null) Destroy(currentTower);
            currentTower = Instantiate(prefab);
            currentCost = cost;
        }
        else
        {
            return;
            Debug.Log("Not enough money to select tower!");
        }
    }

    private void HandleTowerPlacement()
    {
        if (currentTower == null) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, placementLayer))
        {
            Vector3 newPos = hit.point;
            newPos.y = 0;
            currentTower.transform.position = newPos;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    private void PlaceTower()
    {
        if (money >= currentCost)
        {
            money -= currentCost;
            UpdateMoneyUI();

            currentTower = null;
        }
        else
        {
            Debug.Log("Not enough money to place tower!");
            Destroy(currentTower);
            currentTower = null;
        }
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = "$  " + money.ToString();
    }
}
