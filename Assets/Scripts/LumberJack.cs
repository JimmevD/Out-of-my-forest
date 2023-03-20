using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJack : Enemy
{
    private bool playerInRange;
    private bool followPlayer;
    private Transform nearestTree;

    private void Start()
    {
        FindNearestTree();
    }

    void Update()
    {
        if (followPlayer)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            navMeshAgent.SetDestination(nearestTree.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            followPlayer = true;
            Invoke("StillFollowPlayer", 5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void StillFollowPlayer()
    {
        if (playerInRange)
        {
            followPlayer = true;
        }
        else
        {
            followPlayer = false;
            FindNearestTree();
        }
    }

    private void FindNearestTree()
    {     
        float shortestDistance = Mathf.Infinity;

        foreach (Tree tree in trees.allTrees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position); // Calculate the distance to the object

            if (distance < shortestDistance) // Check if the distance is shorter than the current shortest distance
            {
                shortestDistance = distance;
                nearestTree = tree.gameObject.transform;
            }
        }        
    }
}
