using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public List<Tree> allTrees = new List<Tree>();
    void Start()
    {
        Tree[] currentTree = FindObjectsOfType<Tree>();

        foreach (Tree tree in currentTree)
        {
            allTrees.Add(tree); 
        }
    }

    
    void Update()
    {
        
    }
}
