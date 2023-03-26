using UnityEngine;
using TMPro;

public static class EndScreen
{
    public static void EndScenario(bool goodEnding)
    {
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("EndCamera").GetComponent<Camera>().enabled = true;
        
        if (goodEnding)
        {
            GameObject.Find("GoodText").GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            GameObject.Find("BadText").GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
