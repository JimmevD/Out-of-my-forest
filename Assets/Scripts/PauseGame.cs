using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject normalUI, pausedUI;
    private PlayerController playerController;
    private ThrowBanana throwBanana;
    private PlayerHealth playerHealth;
    private MouseLook mouseLook;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        throwBanana = FindObjectOfType<ThrowBanana>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        mouseLook = FindObjectOfType<MouseLook>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Cursor.lockState = CursorLockMode.None;        
            Time.timeScale = 0;
            SwitchUI();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Resume();
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SwitchUI();
    }

    private void SwitchUI()
    {
        normalUI.SetActive(!normalUI.activeInHierarchy);
        pausedUI.SetActive(!pausedUI.activeInHierarchy);
        isPaused = !isPaused;
        playerController.enabled = !playerController.enabled;
        throwBanana.enabled = !throwBanana.enabled;
        playerHealth.enabled = !playerHealth.enabled;
        mouseLook.enabled = !mouseLook.enabled;
    }
}
