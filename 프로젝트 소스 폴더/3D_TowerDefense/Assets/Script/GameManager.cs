using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool bGameOver = false;

    public GameObject m_GameOverUI = null;
    public static int m_nRound = 0;
    public GameObject m_CompleteUI = null;
    public GameObject m_OverlayCanvasTxt = null;
    public int m_nClearMoney = 0;

    private void Start()
    {
        bGameOver = false;
        m_nRound = 0;
    }
    void Update()
    {
        if (bGameOver)
            return;

        if (PlayerStats.nLives <= 0)
        {
            EndGame();    
        }
    }

    void EndGame()
    {
        bGameOver = true;
        m_OverlayCanvasTxt.SetActive(false);
        m_GameOverUI.SetActive(true);

        int nTempMoney = PlayerPrefs.GetInt("PlayerMoney", 0);

        if (m_nRound > 30)
            m_nClearMoney = m_nRound * 30;
        else
            m_nClearMoney = m_nRound * 10;

        nTempMoney += m_nClearMoney;
        m_nClearMoney = nTempMoney;
        PlayerPrefs.SetInt("PlayerMoney", nTempMoney);
    }

    public void WinLevel ()
    {
        bGameOver = true;
        m_OverlayCanvasTxt.SetActive(false);
        m_CompleteUI.SetActive(true);

        int nTempMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        nTempMoney += m_nClearMoney;
        PlayerPrefs.SetInt("PlayerMoney", nTempMoney);
    }
}
