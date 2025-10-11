using UnityEngine;

public class Raycasthealthhider : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask hideLayerMask;
    private float raycastDistance = 200f;
    private HealthEnemy healthEnemy;
    private void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, hideLayerMask))
        {
            if (hit.collider != null)
            {
                healthEnemy = hit.collider.GetComponent<HealthEnemy>();
                if (healthEnemy != null)
                {
                    healthEnemy.isHealthUIVisible = true;
                    }
            }
        }
        else
        {
            if (healthEnemy != null)
            {
                healthEnemy.isHealthUIVisible = false;
                healthEnemy = null;
            }
            }
        }
}

