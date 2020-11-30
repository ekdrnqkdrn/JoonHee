using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyMGR : MonoBehaviour
{
    public Text m_MoneyTxt;
    // Update is called once per frame
    void Update()
    {
        m_MoneyTxt.text = "  $" + PlayerStats.nMoney.ToString();
    }
}
