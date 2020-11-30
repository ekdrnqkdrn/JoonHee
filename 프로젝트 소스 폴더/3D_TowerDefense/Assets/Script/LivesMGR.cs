using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesMGR : MonoBehaviour
{
    public Text m_LivesTxt;
    // Update is called once per frame
    void Update()
    {
        m_LivesTxt.text = "Lives : " + PlayerStats.nLives.ToString();
    }
}
