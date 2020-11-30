using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSurviveMGR : MonoBehaviour
{
    public Text m_RoundTxt;
    void Start()
    {
        StartCoroutine(AnimateTxt());
    }

    IEnumerator AnimateTxt()
    {
        m_RoundTxt.text = "0";
        int nRound = 0;

        yield return new WaitForSeconds(0.7f);
        while (nRound < GameManager.m_nRound)
        {
            nRound++;
            m_RoundTxt.text = nRound.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }
}
