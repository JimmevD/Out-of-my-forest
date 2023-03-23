using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public List<Tree> activeTrees = new List<Tree>();
    private List<Tree> allTrees = new List<Tree>();

    [SerializeField] private GameObject hologramTree;
    void Start()
    {        
        Tree[] currentTree = FindObjectsOfType<Tree>();

        foreach (Tree tree in currentTree)
        {
            if (tree.gameObject.activeInHierarchy)
            {
                activeTrees.Add(tree);
                allTrees.Add(tree);
            }
        }
    }

    public void RemoveFromlist(Tree tree)
    {
        activeTrees.Remove(tree);

        if (activeTrees.Count == 0)
        {
            Debug.Log("Game over");
        }
    }

    private void Update()
    {
        if (activeTrees.Count <= 5)
        {
            CheckForTrees();
        }
    }

    private void CheckForTrees()
    {
        foreach (Tree tree in allTrees)
        {
            if (tree.gameObject.activeInHierarchy)
            {
                if (!activeTrees.Contains(tree))
                {
                    activeTrees.Add(tree);
                }
            }
        }
    }

    public void SpawnHoloGram()
    {
        foreach (Tree tree in allTrees)
        {
            if (!tree.gameObject.activeInHierarchy)
            {
                GameObject holo = Instantiate(hologramTree, tree.transform.position, Quaternion.identity);
                holo.GetComponent<HologramTree>().tree = tree.gameObject.GetComponent<Tree>();
            }
        }
    }
}
