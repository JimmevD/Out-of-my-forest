using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private float duration = 1.0f;
    private float elapsedTime = 0.0f;
    private Vector3 originalRotation;
    [SerializeField] private GameObject branches;
    public Transform[] branch;
    [HideInInspector] public Trees trees;
    [SerializeField] private Light directionalLight;
    void Start()
    {
        originalRotation = transform.eulerAngles;

        directionalLight = FindObjectOfType<Light>();

        branch = new Transform[branches.transform.childCount];

        for (int i = 0; i < branches.transform.childCount; i++)
        {
           branch[i] = branches.transform.GetChild(i).gameObject.transform;
        }
    }

    public void CutDown()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        while (elapsedTime < duration)
        {
            float angle = Mathf.Lerp(0, 90, elapsedTime / duration);
            transform.eulerAngles = originalRotation + new Vector3(angle, 0, 0); 
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        trees.ChangeIntensity(true);
        gameObject.SetActive(false);
    }

    public void ResetTree()
    {
        gameObject.SetActive(true);
        trees.ChangeIntensity(false);
        transform.eulerAngles = originalRotation + new Vector3(originalRotation.x, 0, 0);
        elapsedTime = 0;
    }
}
