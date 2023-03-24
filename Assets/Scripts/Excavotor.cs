using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excavotor : Enemy
{
    void Update()
    {
        if (trees.activeTrees.Count != 0)
        {
            if (!nearestTree && trees)
            {
                FindNearestTree();
            }
            else if (nearestTree && !nearestTree.gameObject.activeInHierarchy)
            {
                FindNearestTree();
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree" && nearestTree == collision.gameObject.GetComponent<Tree>())
        {
            collision.gameObject.GetComponent<Tree>().CutDown();
            nearestTree = null;
        }
    }
}
