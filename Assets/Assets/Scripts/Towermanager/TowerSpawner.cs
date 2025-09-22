using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerSpawner : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject Archer;
    [SerializeField] private GameObject Crossbow;
    [SerializeField] private GameObject Mage;
    [SerializeField] private GameObject Cannon;
    [SerializeField] private GameObject Crystal;
    [SerializeField] private GameObject Minigunner;

    [Header("Settings")]
    [SerializeField] private LayerMask placementLayer;
    [SerializeField] private int maxTowers = 25;

    [Header("UI")]
    [SerializeField] private Text moneyText;

    [SerializeField] private int money = 500;
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) TrySelectTower(Archer, 250);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) TrySelectTower(Crossbow, 500);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) TrySelectTower(Mage, 750);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) TrySelectTower(Cannon, 1500);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) TrySelectTower(Crystal, 5000);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) TrySelectTower(Minigunner, 2000);
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

            float towerHeight = currentTower.GetComponent<Collider>() != null
                ? currentTower.GetComponent<Collider>().bounds.extents.y
                : 0f;

            newPos.y += towerHeight;

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
        moneyText.text = "$ " + money.ToString();
    }
}
