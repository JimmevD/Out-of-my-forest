using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJack : Enemy
{
    private bool playerInRange;
    private bool followPlayer;

    void Update()
    {
        if (trees.activeTrees.Count != 0)
        {
            if (!nearestTree && trees && !followPlayer)
            {
                FindNearestTree();
            }
            else if (nearestTree && !nearestTree.gameObject.activeInHierarchy)
            {
                FindNearestTree();
            }

            if (followPlayer)
            {
                navMeshAgent.SetDestination(playerTransform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree" && nearestTree == collision.gameObject.GetComponent<Tree>())
        {
            StartCoroutine(CuttingDownTree(collision.gameObject.GetComponent<Tree>()));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FoundPlayer();
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

    private IEnumerator CuttingDownTree(Tree currentTree)
    {
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(3);

        currentTree.CutDown();
        nearestTree = null;

        navMeshAgent.isStopped = false;
    }

    private void FoundPlayer()
    {
        StopAllCoroutines();
        if (nearestTree != null)
        {
            trees.activeTrees.Add(nearestTree);
        }
        nearestTree = null;
        playerInRange = true;
        followPlayer = true;
        navMeshAgent.isStopped = false;
        Invoke("StillFollowPlayer", 5);
    }
}
