using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBanana : MonoBehaviour
{
    private int currentBananas;
    [SerializeField] float shootForce;
    [SerializeField] GameObject banana;
    [SerializeField] Camera lookCamera;
    [SerializeField] Transform throwPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBananas <= 0)
            {
               GameObject bananaGO =  Instantiate(banana, throwPosition.position, banana.transform.rotation);
               bananaGO.GetComponent<Rigidbody>().AddForce(lookCamera.transform.forward * shootForce, ForceMode.Impulse);
            }
        }
    }
}
