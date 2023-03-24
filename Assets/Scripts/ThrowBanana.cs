using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowBanana : MonoBehaviour
{
    private int currentBananas;
    [SerializeField] private TextMeshProUGUI bananaCountText;

    [SerializeField] float beginShootForce;
    private float extraShootingForce;
    [SerializeField] float increaseOverTime;
    private bool isShaking;
    private float extraDamage;
    private float originaldamage;

    [SerializeField] private float maxExtraShootingForce = 25f;
    [SerializeField] private float maxExtraDamage = 25f;
    [SerializeField] private Slider slider;

    [SerializeField] GameObject banana;
    [SerializeField] GameObject visualBanana;

    [SerializeField] Camera lookCamera;
    [SerializeField] Transform throwPosition;

    public int CurrentBanana
    {
        set { currentBananas += value ; bananaCountText.text = currentBananas.ToString(); }
    }

    private void Start()
    {
        extraDamage = originaldamage;
        currentBananas = 20;
        bananaCountText.text = currentBananas.ToString();
    }

    void Update()
    {
        if (currentBananas > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                visualBanana.SetActive(true);
                slider.gameObject.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                extraShootingForce += increaseOverTime * Time.deltaTime;
                extraShootingForce = Mathf.Clamp(extraShootingForce, 0f, maxExtraShootingForce);
                slider.value = extraShootingForce / maxExtraShootingForce;

                visualBanana.transform.position = visualBanana.transform.parent.position;
                extraDamage += increaseOverTime * Time.deltaTime;
                extraDamage = Mathf.Clamp(extraDamage, 0f, maxExtraDamage);

                if (isShaking)
                {
                    visualBanana.transform.position += Vector3.right * extraShootingForce * 0.001f;
                    isShaking = !isShaking;
                }
                else
                {
                    visualBanana.transform.position += Vector3.left * extraShootingForce * 0.001f;
                    isShaking = !isShaking;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                GameObject bananaGO = Instantiate(banana, throwPosition.position, banana.transform.rotation);
                bananaGO.GetComponent<Rigidbody>().AddForce(lookCamera.transform.forward * (extraShootingForce + beginShootForce), ForceMode.Impulse);
                bananaGO.GetComponent<Banana>().damage += extraDamage;
                extraDamage = originaldamage;
                currentBananas--;
                bananaCountText.text = currentBananas.ToString();
                extraShootingForce = 0;
                slider.value = 0;
                visualBanana.SetActive(false);
                slider.gameObject.SetActive(false);
            }
        }
    }
           
}
