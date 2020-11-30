using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMGR : MonoBehaviour
{
    public float m_fStartSpeed = 10;
    //[HideInInspector] //시작 스피드가 있는데 현재 스피드 변수가 있으면 헷깔려서 추가 (인스펙터에 안보이게함)
    public float m_fSpeed = 0f;
    public float m_fStartHealth = 100f;
    //[HideInInspector] 
    public float m_fHealth = 0f;
    public int m_nMoney = 20;

    public GameObject m_DieEffect = null;
    [Header("Unity Stuff")]
    public Image m_HealthBar = null;

    private bool m_bDie = false;

    [HideInInspector]
    public float m_fInfiniteModeHP;
    [HideInInspector]
    public static bool m_bInfiniteMode = false;
    [HideInInspector]
    public float m_fInfiniteModeSpeed;

    private void Start()
    {
        if (!m_bInfiniteMode)
        {
            m_fHealth = m_fStartHealth;
            m_fSpeed = m_fStartHealth;
        }
    }

    public void SetInfEnemyStats(float fHP, float fSpeed)
    {
        m_fHealth = fHP;
        m_fInfiniteModeHP = fHP;
        m_fSpeed = fSpeed;
        m_fInfiniteModeSpeed = fSpeed;
    }

    public void TakeDamage (float fAmount)
    {
        m_fHealth -= fAmount;

        //-->수정할거(2탄에서 무한으로 게임하려고 임시로 추가)
        if (m_bInfiniteMode)
        {
            m_HealthBar.fillAmount = m_fHealth / m_fInfiniteModeHP;
        }
        else
        {
            m_HealthBar.fillAmount = m_fHealth / m_fStartHealth;
        }
        //<--수정할거(2탄에서 무한으로 게임하려고 임시로 추가)

        if (m_fHealth <= 0 && !m_bDie )
        {
            Die();
        }
    }
    
    void Die()
    {
        m_bDie = true;
        PlayerStats.nMoney += m_nMoney;
        GameObject TempObj = (GameObject)Instantiate(m_DieEffect, transform.position, Quaternion.identity);
        WaveMGR.nEnemyAlive--;
        Destroy(TempObj, 1.5f);
        Destroy(gameObject);
    }

    public void Slow(float fSlowPerCent)
    {
        m_fSpeed = m_fSpeed * (1f - fSlowPerCent);
    }
}
