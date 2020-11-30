using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughMoneyUIMGR : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("TurnOffUI");
    }

    public void OKClick()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    IEnumerator TurnOffUI()
    {
        yield return new WaitForSeconds(5f);

        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
 
