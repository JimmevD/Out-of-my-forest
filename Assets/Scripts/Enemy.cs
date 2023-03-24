using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float speed = 3.5f;

    protected GameObject healthBar;
    protected Slider slider;
    protected NavMeshAgent navMeshAgent;
    protected Transform playerTransform;
    [HideInInspector] public Trees trees;
    [HideInInspector] public WaveManager waveManager;
    [HideInInspector] public Tree nearestTree;

    public void Awake()
    {
        healthBar = GetComponentInChildren<Canvas>().gameObject;
        slider = GetComponentInChildren<Slider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
        navMeshAgent.speed = speed;
        healthBar.SetActive(false);
    }

    private void LateUpdate()
    {
        healthBar.transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        slider.value = currentHealth / maxHealth;

        if (currentHealth < maxHealth && !healthBar.activeInHierarchy)
        {
            healthBar.SetActive(true);
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        waveManager.KilledEnemy();
        Destroy(this.gameObject);
    }

    public void FindNearestTree()
    {
        float shortestDistance = Mathf.Infinity;

        foreach (Tree tree in trees.activeTrees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position); // Calculate the distance to the object

            if (distance < shortestDistance) // Check if the distance is shorter than the current shortest distance
            {
                shortestDistance = distance;
                nearestTree = tree;
                navMeshAgent.SetDestination(nearestTree.transform.position);
            }
        }

        trees.activeTrees.Remove(nearestTree);
    }
}
