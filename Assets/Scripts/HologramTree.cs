using UnityEngine;
using TMPro;

public class HologramTree : MonoBehaviour
{
    public Tree tree;
    private GameObject player;
    public TextMeshProUGUI interactText;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player)
        {
            tree.ResetTree();
            player = null;
            interactText.enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            interactText.text = "Press 'E' to plant Tree";
            interactText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player = null;
            interactText.enabled = false;
        }
    }
}
