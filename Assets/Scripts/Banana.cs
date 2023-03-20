using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private bool hittedObject;
    [SerializeField] private float damage;
    void Start()
    {
        Destroy(gameObject, 10);
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
        if (!hittedObject && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }

        hittedObject = true;
    }
}
