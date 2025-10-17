using UnityEngine;
using UnityEngine.AI;

public class Enemymovement : MonoBehaviour
{
    public GameObject Player;
    public float speed = 3f;
    public NavMeshAgent agent;
    



    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().speed = speed;
    }
    private void Update()
    {
        if (agent != null)
            agent.SetDestination(Player.transform.position);
    }
}

