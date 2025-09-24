using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TowerSpawner : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject crossbow;
    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject minigunner;

    [Header("Settings")]
    [SerializeField] private LayerMask placementLayer;
    [SerializeField] private int maxTowers = 25;
    [SerializeField] private int startingMoney = 500;

    [Header("UI")]
    [SerializeField] private Text moneyText;
    [SerializeField] private Text towerErrorLog;
    [SerializeField] private GameObject errorLogsPanel;
    [SerializeField] private Text currenttowersspawnedtext;

    private int money;
    private GameObject previewTower;
    private int previewCost;

    private readonly List<PlacedTower> placedTowers = new List<PlacedTower>();

    private void Start()
    {
        money = startingMoney;
        UpdateMoneyUI();
    }

    private void Update()
    {
        HandleTowerSelection();
        HandleTowerPlacement();
        HandleTowerInteraction();
    }

    #region Tower Selection
    private void HandleTowerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) TrySelectTower(archer, 250);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) TrySelectTower(crossbow, 500);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) TrySelectTower(mage, 750);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) TrySelectTower(cannon, 1500);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) TrySelectTower(crystal, 5000);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) TrySelectTower(minigunner, 2000);
    }

    private void TrySelectTower(GameObject prefab, int cost)
    {
        if (placedTowers.Count >= maxTowers)
        {
            ShowError("Tower limit reached!");
            return;
        }

        if (money < cost)
        {
            ShowError("Insufficient funds!");
            return;
        }

        if (previewTower != null) Destroy(previewTower);
        previewTower = Instantiate(prefab);
        previewCost = cost;
    }
    #endregion

    #region Tower Placement
    private void HandleTowerPlacement()
    {
        if (previewTower == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, placementLayer))
        {
            // Snap preview directly to ground
            Vector3 newPos = hit.point;
            previewTower.transform.position = newPos;

            // Optional: ensure tower sits flat on surface
            previewTower.transform.rotation = Quaternion.Euler(0f, previewTower.transform.rotation.eulerAngles.y, 0f);

            // Rotate preview tower with R
            if (Input.GetKeyDown(KeyCode.R))
            {
                previewTower.transform.Rotate(0, 45f, 0);
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    private void PlaceTower()
    {
        if (money < previewCost)
        {
            ShowError("Not enough money!");
            Destroy(previewTower);
            previewTower = null;
            return;
        }

        money -= previewCost;
        UpdateMoneyUI();

        PlacedTower newTower = new PlacedTower(previewTower, previewCost);
        placedTowers.Add(newTower);

        previewTower = null;
    }
    #endregion

    #region Tower Interaction (Rotate / Remove)
    private void HandleTowerInteraction()
    {
        if (previewTower != null) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PlacedTower tower = placedTowers.Find(t => t.TowerObject == hit.collider.gameObject);
                if (tower != null)
                {
                    tower.TowerObject.transform.Rotate(0, 45f, 0);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Backspace pressed - attempting to remove tower");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                PlacedTower tower = placedTowers.Find(t => t.TowerObject == hit.collider.gameObject);
                if (tower != null)
                {
                    RemoveTower(tower);
                }
            }
        }
    }

    private void RemoveTower(PlacedTower tower)
    {
        int refund = tower.Cost / 2;
        money += refund;
        UpdateMoneyUI();

        placedTowers.Remove(tower);
        Destroy(tower.TowerObject);
    }
    #endregion

    #region Money & UI
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = $"$ {money}";
        currenttowersspawnedtext.text = $"Towers: {placedTowers.Count}/{maxTowers}";

    }

    private void ShowError(string message)
    {
        errorLogsPanel.SetActive(true);
        towerErrorLog.text = message;
        StartCoroutine(HideErrorAfterDelay(2.5f));
    }

    private IEnumerator HideErrorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        errorLogsPanel.SetActive(false);
    }
    #endregion

    #region Helper Class
    private class PlacedTower
    {
        public GameObject TowerObject { get; }
        public int Cost { get; }

        public PlacedTower(GameObject obj, int cost)
        {
            TowerObject = obj;
            Cost = cost;
        }
    }
    #endregion
}
