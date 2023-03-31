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
            GameObject.Find("Endings").transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("Endings").transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
