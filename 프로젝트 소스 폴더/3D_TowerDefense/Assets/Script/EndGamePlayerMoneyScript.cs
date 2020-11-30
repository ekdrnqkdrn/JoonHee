using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePlayerMoneyScript : MonoBehaviour
{
    public Text m_PlayerTxt;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerTxt.text = "PlayerMoney : $" + PlayerPrefs.GetInt("PlayerMoney", 0).ToString();
    }
}
