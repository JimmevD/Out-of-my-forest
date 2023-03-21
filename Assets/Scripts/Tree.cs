using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private float duration = 1.0f;
    private float elapsedTime = 0.0f;
    private Vector3 originalRotation;
    [SerializeField] Transform anchor;
    private Trees trees;
    void Start()
    {
        originalRotation = anchor.transform.eulerAngles;
        trees = FindObjectOfType<Trees>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CutDown()
    {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        while (elapsedTime < duration)
        {
            float angle = Mathf.Lerp(0, 90, elapsedTime / duration);
            anchor.transform.eulerAngles = originalRotation + new Vector3(angle, 0, 0); 
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        
        trees.allTrees.Remove(this);
        gameObject.SetActive(false);
    }
}
