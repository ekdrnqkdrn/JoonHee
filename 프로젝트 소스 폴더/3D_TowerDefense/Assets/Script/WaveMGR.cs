using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //수정할거(2탄에서 무한으로 게임하려고 임시로 추가)

public class WaveMGR : MonoBehaviour
{
    public static int nEnemyAlive = 0;

    public Transform m_SwawnPoint = null;
    
    public Wave[] m_Waves;

    public float m_fBetweenWaveTime = 5f;
    private float m_fCountDown = 5f;

    private int m_nWaveIndex = 0;

    public GameManager m_GameManager = null;
    public Text m_WavesTxt; 

    //-->수정할거(2탄에서 무한으로 게임하려고 임시로 추가)
    [Header("무한 리스폰시 체크")]
    public bool m_bInfiniteMode = false;
    private int m_nInfiniteMode = 1;
    //<--수정할거(2탄에서 무한으로 게임하려고 임시로 추가)
    private void Start()
    {
        GameManager.m_nRound = 0;
        nEnemyAlive = 0;
        if (m_bInfiniteMode)
            m_WavesTxt.text = "  Waves :  " + GameManager.m_nRound.ToString() + " / ∞";
        else
            m_WavesTxt.text = "  Waves :  " + GameManager.m_nRound.ToString() + " / " + m_Waves.Length.ToString();

    }
    private void Update()
    {
        if (nEnemyAlive > 0)
        {
            return;
        }

        if (m_fCountDown <= 0)
        {
            StartCoroutine(SpawnWave());
            m_fCountDown = m_fBetweenWaveTime;
            return;
        }

        m_fCountDown -= Time.deltaTime;

        m_fCountDown = Mathf.Clamp(m_fCountDown, 0f, Mathf.Infinity);
        //m_tWaveCountDownTxt.text = Mathf.Round(m_fCountDown).ToString();
    }

    IEnumerator SpawnWave()
    {
        GameManager.m_nRound++;
        if (m_bInfiniteMode)
            m_WavesTxt.text = "  Waves :  " + GameManager.m_nRound.ToString() + " / ∞";
        else
            m_WavesTxt.text = "  Waves :  " + GameManager.m_nRound.ToString() + " / " + m_Waves.Length.ToString();

        Wave wave = m_Waves[m_nWaveIndex];
        nEnemyAlive = wave.nCount;

        for (int i = 0; i < wave.nCount; i++)
        {
            SpawnEnemy(wave.Enemy);
            yield return new WaitForSeconds(1f / wave.fRate);
        }
        m_nWaveIndex++;

        if (m_nWaveIndex == m_Waves.Length)
        {
            //-->수정할거(2탄에서 무한으로 게임하려고 임시로 추가)
            if (m_bInfiniteMode)
            {
                m_nInfiniteMode++;
                m_nInfiniteMode++;
                m_nWaveIndex = 0;
            }
            else
            {
            //<--수정할거(2탄에서 무한으로 게임하려고 임시로 추가)
                m_GameManager.WinLevel();
                this.enabled = false;
            }
        }
    }

    private void SpawnEnemy(GameObject EnemyPrefab)
    {
        //-->수정할거(2탄에서 무한으로 게임하려고 임시로 추가)
        GameObject EnmeyObj = Instantiate(EnemyPrefab, m_SwawnPoint.position, m_SwawnPoint.rotation) as GameObject;

        if (m_bInfiniteMode 
        && GameManager.m_nRound > m_Waves.Length)
        {
            EnemyMGR EnmeyScript = EnmeyObj.GetComponent<EnemyMGR>();

            EnemyMGR.m_bInfiniteMode = true;

            float fHP = EnemyPrefab.GetComponent<EnemyMGR>().m_fStartHealth * m_nInfiniteMode;
            float fSpeed = EnemyPrefab.GetComponent<EnemyMGR>().m_fStartSpeed + m_nInfiniteMode/4;

            //EnmeyScript.m_fHealth = fHP;
            //EnmeyScript.m_fInfiniteModeHP = fHP;

            //EnmeyScript.m_fSpeed = fSpeed;

            EnmeyScript.SetInfEnemyStats(fHP, fSpeed);
        }
        else
        {
            EnemyMGR.m_bInfiniteMode = false;
        }
        //<--수정할거(2탄에서 무한으로 게임하려고 임시로 추가)
    }
}
