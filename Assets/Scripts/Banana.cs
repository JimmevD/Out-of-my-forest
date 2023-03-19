using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private bool hittedObject;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (!hittedObject)
        {
            transform.Rotate(transform.forward * 1000 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hittedObject = true;
    }
}
