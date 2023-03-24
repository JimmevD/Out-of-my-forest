using UnityEngine;
using TMPro;

public class BananaPack : MonoBehaviour
{
    [SerializeField] private int amountOfBanans = 3;
    private GameObject player;
    private TextMeshProUGUI interactText;
    private bool pickedUp;
    private float respawnTimer;
   
    void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.E))
        {
            PickedUpBananaPack();
        }

        if (pickedUp)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0)
            {
                pickedUp = false;
                gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
           
            if (!interactText)
            {
                interactText = player.GetComponent<PlayerController>().interactText;
            }

            interactText.enabled = true;
            interactText.text = "Press 'E' To pick up Bananas";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            interactText.enabled = false;
            player = null;
        }
    }

    private void PickedUpBananaPack()
    {
        player.GetComponent<ThrowBanana>().CurrentBanana = amountOfBanans;
        pickedUp = true;
        respawnTimer = 20;
        interactText.enabled = false;
        player = null;
        gameObject.SetActive(false);
    }
}
