using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trees : MonoBehaviour
{
    public List<Tree> activeTrees = new List<Tree>();
    private List<Tree> allTrees = new List<Tree>();
    private List<GameObject> holograms = new List<GameObject>();
    private PlayerController playerController;

    [SerializeField] private GameObject hologramTree;
    [SerializeField] private Camera endCamera;
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

        playerController = FindObjectOfType<PlayerController>();
    }

    public void RemoveFromlist(Tree tree)
    {
        activeTrees.Remove(tree);

        if (activeTrees.Count == 0)
        {
            EndScreen.EndScenario(false);
        }
    }

    private void Update()
    {
        if (activeTrees.Count <= 5)
        {
            CheckForTrees();
        }

        if (activeTrees.Count == 0)
        {
            EndScreen.EndScenario(false);
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
                holograms.Add(holo);
                holo.GetComponent<HologramTree>().tree = tree.gameObject.GetComponent<Tree>();
                holo.GetComponent<HologramTree>().interactText = playerController.interactText;
            }
        }
    }

    public void RemoveHologramTrees()
    {
        for (int i = 0; i < holograms.Count; i++)
        {
            Destroy(holograms[i]);
        }

        holograms.Clear();
    }

    public void ResetAllTrees()
    {
        for (int i = 0; i < allTrees.Count; i++)
        {
            if (!allTrees[i].gameObject)
            {
                allTrees[i].ResetTree();
            }
        }
    }
}
