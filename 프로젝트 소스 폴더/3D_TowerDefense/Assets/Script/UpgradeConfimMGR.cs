using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //공통 속성을 가진 변수들을 그룹지어 클래스를 만든다. [Serializable] 속성을 부여한다.
public class TOWERUPGRADESTATS
{
    //업그레이드 시 데미지 증가량
    public int m_nCannonDamage = 0;
    public int m_nLaserDamage = 0;
    public int m_nMagicDamage = 0;

    //업그레이드 시 공격속도 증가량
    public float m_fCannonSpeed = 0;
    public float m_fMagicSpeed = 0;

    //업그레이드 시 슬로우 증가량
    public float m_fLaserSlow = 0f;

    //업그레이드 시 필요 금액
    public int m_nCannonDamageCost = 0;
    public int m_nCannonSpeedCost = 0;
    public int m_nLaserDamageCost = 0;
    public int m_nLaserSlowCost = 0;
    public int m_nMagicDamageCost = 0;
    public int m_nMagicSpeedCost = 0;
    
    [HideInInspector]
    public int m_nBasicCannonDamageCost = 0;
    [HideInInspector]
    public int m_nBasicCannonSpeedCost = 0;
    [HideInInspector]
    public int m_nBasicLaserDamageCost = 0;
    [HideInInspector]
    public int m_nBasicLaserSlowCost = 0;
    [HideInInspector]
    public int m_nBasicMagicDamageCost = 0;
    [HideInInspector]
    public int m_nBasicMagicSpeedCost = 0;
}

public class UpgradeConfimMGR : MonoBehaviour
{
    public Text m_NowStats = null;
    public Text m_UpgradeStats = null;
    public TOWERUPGRADESTATS m_TowerUpgradeStats;

    private UpgradeMGR.E_UPGRADEKIND m_eUpgradeKind;

    public GameObject m_CannonTowerMGR = null;
    public GameObject m_BulletMGR = null;
    public GameObject m_MagicTowerMGR = null;
    public GameObject m_MagicMGR = null;
    public GameObject m_LaserMGR = null;
    public Text m_CostTxt = null;

    private TowerMGR m_CannonTowerScript = null;
    private TowerMGR m_MagicTowerScript = null;
    private BulletMGR m_BulletScript = null;
    private TowerMGR m_LaserScript = null;
    private BulletMGR m_MaigcScript = null;

    private int m_nAddDamage = 0;
    private float m_fAddSpeed = 0f;
    private float m_fAddSlow = 0;

    [HideInInspector]
    public List<Dictionary<string, object>> m_TowerInfoList; //타워 리소스 읽어서 저장 하는 리스트
    public string m_NodeCVSFileName = "DB_TowerInfo";

    public GameObject m_NotEnoughMoneyUI = null;
    public Text m_MoneyTxt = null;
    private void Awake()
    {
        m_CannonTowerScript = m_CannonTowerMGR.GetComponent<TowerMGR>();
        m_MagicTowerScript = m_MagicTowerMGR.GetComponent<TowerMGR>();
        m_BulletScript = m_BulletMGR.GetComponent<BulletMGR>();
        m_LaserScript = m_LaserMGR.GetComponent<TowerMGR>();
        m_MaigcScript = m_MagicMGR.GetComponent<BulletMGR>();
        LoadTowerInfo();
    }

    private void LoadTowerInfo()
    {
        m_TowerInfoList = CSVFileReader.Read(m_NodeCVSFileName);
        for (int i = 0; i < m_TowerInfoList.Count; i++)
        {
            //DB에서 타워 정보 불러옴
            int nId = (int)m_TowerInfoList[i]["id"];
            int n1Row_Cost = (int)m_TowerInfoList[i]["1Row_IncreaseUpgradeCost"];
            int n2Row_Cost = (int)m_TowerInfoList[i]["2Row_IncreaseUpgradeCost"];

            if (nId == (int)E_TOWERTYPE.E_CANNON)
            {
                m_TowerUpgradeStats.m_nCannonDamageCost = n1Row_Cost * PlayerPrefs.GetInt("nCannonDamageUpgradeCount", 1);
                m_TowerUpgradeStats.m_nCannonSpeedCost = n2Row_Cost * PlayerPrefs.GetInt("nCannonSpeedUpgradeCount", 1);
                m_TowerUpgradeStats.m_nBasicCannonDamageCost = n1Row_Cost;
                m_TowerUpgradeStats.m_nBasicCannonSpeedCost= n2Row_Cost;
            }
            else if (nId == (int)E_TOWERTYPE.E_LASER)
            {
                m_TowerUpgradeStats.m_nLaserDamageCost = n1Row_Cost * PlayerPrefs.GetInt("nLaserDamageUpgradeCount", 1);
                m_TowerUpgradeStats.m_nLaserSlowCost = n2Row_Cost * PlayerPrefs.GetInt("nLaserSlowUpgradeCount", 1);
                m_TowerUpgradeStats.m_nBasicLaserDamageCost = n1Row_Cost;
                m_TowerUpgradeStats.m_nBasicLaserSlowCost = n2Row_Cost;
            }
            else if (nId == (int)E_TOWERTYPE.E_MAGIC)
            {
                m_TowerUpgradeStats.m_nMagicDamageCost = n1Row_Cost * PlayerPrefs.GetInt("nMagicDamageUpgradeCount", 1);
                m_TowerUpgradeStats.m_nMagicSpeedCost = n2Row_Cost * PlayerPrefs.GetInt("nMagicSpeedUpgradeCount", 1);
                m_TowerUpgradeStats.m_nBasicMagicDamageCost = n1Row_Cost;
                m_TowerUpgradeStats.m_nBasicMagicSpeedCost = n2Row_Cost;
            }
        }
    }

    public void TurnOnUpgradeConfirm(UpgradeMGR.E_UPGRADEKIND eKind)
    {
        m_nAddDamage = 0;
        string sUpgradeCost = "";

        if (eKind == UpgradeMGR.E_UPGRADEKIND.E_DAMAGE)
        {
            int nDamage = 0;
            int nAddDamage = 0;
            int nPlayerPrefabStats = 0;

            if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_CANNON)
            {
                nPlayerPrefabStats = PlayerPrefs.GetInt("UpgradeCannonDamage", 0);
                nDamage = m_BulletScript.m_nDamage + nPlayerPrefabStats;
                nAddDamage = m_TowerUpgradeStats.m_nCannonDamage;
                sUpgradeCost = m_TowerUpgradeStats.m_nCannonDamageCost.ToString();
            }
            else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_LASER)
            {
                nPlayerPrefabStats = PlayerPrefs.GetInt("UpgradeLaserDamage", 0);
                nDamage = m_LaserScript.m_nDamageOverTime + nPlayerPrefabStats;
                nAddDamage = m_TowerUpgradeStats.m_nLaserDamage;
                sUpgradeCost = m_TowerUpgradeStats.m_nLaserDamageCost.ToString();
            }
            else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_MAGIC)
            {
                nPlayerPrefabStats = PlayerPrefs.GetInt("UpgradeMagicDamage", 0);
                nDamage = m_MaigcScript.m_nDamage + nPlayerPrefabStats;
                nAddDamage = m_TowerUpgradeStats.m_nMagicDamage;
                sUpgradeCost = m_TowerUpgradeStats.m_nMagicDamageCost.ToString();
            }

            m_nAddDamage = nAddDamage;
            nAddDamage += nDamage;
            m_NowStats.text = "Damage : " + nDamage.ToString();
            m_UpgradeStats.text = "Damage : " + nAddDamage.ToString();
        }
        else if (eKind == UpgradeMGR.E_UPGRADEKIND.E_SPEED)
        {
            float fSpeed = 0f;
            float fAddSpeed = 0f;
            float fPlayerPrefabStats = 0f;
            
            if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_CANNON)
            {
                fPlayerPrefabStats = PlayerPrefs.GetFloat("UpgradeCannonSpeed", 0);
                fSpeed = m_CannonTowerScript.m_fFireRate + fPlayerPrefabStats;
                fAddSpeed = m_TowerUpgradeStats.m_fCannonSpeed;
                sUpgradeCost = m_TowerUpgradeStats.m_nCannonSpeedCost.ToString();
            }
            else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_MAGIC)
            {
                fPlayerPrefabStats = PlayerPrefs.GetFloat("UpgradeMagicSpeed", 0);
                fSpeed = m_MagicTowerScript.m_fFireRate + fPlayerPrefabStats;
                fAddSpeed = m_TowerUpgradeStats.m_fMagicSpeed;
                sUpgradeCost = m_TowerUpgradeStats.m_nMagicSpeedCost.ToString();
            }

            m_fAddSpeed = fAddSpeed;
            fAddSpeed += fSpeed;

            m_NowStats.text = "FireRate : " + fSpeed.ToString("F1");
            m_UpgradeStats.text = "FireRate : " + fAddSpeed.ToString("F1");
        }
        else if (eKind == UpgradeMGR.E_UPGRADEKIND.E_SLOW)
        {
            float fSlow = 0f;
            float fAddSlow = 0f;
            float fPlayerPrefabStats = 0f;

            if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_LASER)
            {
                fPlayerPrefabStats = PlayerPrefs.GetFloat("UpgradeLaserSlow", 0);
                fSlow = m_LaserScript.m_fSlowPercent + fPlayerPrefabStats;
                fAddSlow = m_TowerUpgradeStats.m_fLaserSlow;
                sUpgradeCost = m_TowerUpgradeStats.m_nLaserSlowCost.ToString();
            }

            m_fAddSlow = fAddSlow;
            fAddSlow += fSlow;

            m_NowStats.text = "SlowPercent : " + fSlow.ToString("F2");
            m_UpgradeStats.text = "SlowPercent : " + fAddSlow.ToString("F2");
        }

        m_CostTxt.text = sUpgradeCost;
        m_eUpgradeKind = eKind;
    }

    public void CancelClick()
    {
        gameObject.SetActive(false);
    }

    public void ConfirmClick()
    {
        int nTempData = 0;
        float fTempData = 0f;

        if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_CANNON)
        {
            if (UpgradeMGR.m_eUpGradeKind == UpgradeMGR.E_UPGRADEKIND.E_DAMAGE)
            {
                if (!CheckUpgrade(m_TowerUpgradeStats.m_nCannonDamageCost))
                    return;

                nTempData = PlayerPrefs.GetInt("PlayerMoney", 0);
                nTempData -= m_TowerUpgradeStats.m_nCannonDamageCost;
                PlayerPrefs.SetInt("PlayerMoney", nTempData);

                nTempData = PlayerPrefs.GetInt("UpgradeCannonDamage", 0) + m_nAddDamage;
                PlayerPrefs.SetInt("UpgradeCannonDamage", nTempData);

                nTempData = PlayerPrefs.GetInt("nCannonDamageUpgradeCount", 1) + 1;
                PlayerPrefs.SetInt("nCannonDamageUpgradeCount", nTempData);
                m_TowerUpgradeStats.m_nCannonDamageCost += m_TowerUpgradeStats.m_nBasicCannonDamageCost;
            }
            else if (UpgradeMGR.m_eUpGradeKind == UpgradeMGR.E_UPGRADEKIND.E_SPEED)
            {
                if (!CheckUpgrade(m_TowerUpgradeStats.m_nCannonSpeedCost))
                    return;

                nTempData = PlayerPrefs.GetInt("PlayerMoney", 0);
                nTempData -= m_TowerUpgradeStats.m_nCannonSpeedCost;
                PlayerPrefs.SetInt("PlayerMoney", nTempData);

                fTempData = PlayerPrefs.GetFloat("UpgradeCannonSpeed", 0) + m_fAddSpeed;
                PlayerPrefs.SetFloat("UpgradeCannonSpeed", fTempData);

                nTempData = PlayerPrefs.GetInt("nCannonSpeedUpgradeCount", 1) + 1;
                PlayerPrefs.SetInt("nCannonSpeedUpgradeCount", nTempData);
                m_TowerUpgradeStats.m_nCannonSpeedCost += m_TowerUpgradeStats.m_nBasicCannonSpeedCost;
            }
        }
        else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_LASER)
        {
            if (UpgradeMGR.m_eUpGradeKind == UpgradeMGR.E_UPGRADEKIND.E_DAMAGE)
            {
                if (!CheckUpgrade(m_TowerUpgradeStats.m_nLaserDamageCost))
                    return;

                nTempData = PlayerPrefs.GetInt("PlayerMoney", 0);
                nTempData -= m_TowerUpgradeStats.m_nLaserDamageCost;
                PlayerPrefs.SetInt("PlayerMoney", nTempData);

                nTempData = PlayerPrefs.GetInt("UpgradeLaserDamage", 0) + m_nAddDamage;
                PlayerPrefs.SetInt("UpgradeLaserDamage", nTempData);

                nTempData = PlayerPrefs.GetInt("nLaserDamageUpgradeCount", 1) + 1;
                PlayerPrefs.SetInt("nLaserDamageUpgradeCount", nTempData);
                m_TowerUpgradeStats.m_nLaserDamageCost += m_TowerUpgradeStats.m_nBasicLaserDamageCost;
            }
            else if (UpgradeMGR.m_eUpGradeKind == UpgradeMGR.E_UPGRADEKIND.E_SLOW)
            {
                if (!CheckUpgrade(m_TowerUpgradeStats.m_nLaserSlowCost))
                    return;

                nTempData = PlayerPrefs.GetInt("PlayerMoney", 0);
                nTempData -= m_TowerUpgradeStats.m_nLaserSlowCost;
                PlayerPrefs.SetInt("PlayerMoney", nTempData);

                fTempData = PlayerPrefs.GetFloat("UpgradeLaserSlow", 0) + m_fAddSlow;
                PlayerPrefs.SetFloat("UpgradeLaserSlow", fTempData);

                nTempData = PlayerPrefs.GetInt("nLaserSlowUpgradeCount", 1) + 1;
                PlayerPrefs.SetInt("nLaserSlowUpgradeCount", nTempData);
                m_TowerUpgradeStats.m_nLaserSlowCost += m_TowerUpgradeStats.m_nBasicLaserSlowCost;
            }
        }
        else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_MAGIC)
        {
            if (UpgradeMGR.m_eUpGradeKind == UpgradeMGR.E_UPGRADEKIND.E_DAMAGE)
            {
                if (!CheckUpgrade(m_TowerUpgradeStats.m_nMagicDamageCost))
                    return;

                nTempData = PlayerPrefs.GetInt("PlayerMoney", 0);
                nTempData -= m_TowerUpgradeStats.m_nMagicDamageCost;
                PlayerPrefs.SetInt("PlayerMoney", nTempData);

                nTempData = PlayerPrefs.GetInt("UpgradeMagicDamage", 0) + m_nAddDamage;
                PlayerPrefs.SetInt("UpgradeMagicDamage", nTempData);

                nTempData = PlayerPrefs.GetInt("nMagicDamageUpgradeCount", 1) + 1;
                PlayerPrefs.SetInt("nMagicDamageUpgradeCount", nTempData);
                m_TowerUpgradeStats.m_nMagicDamageCost += m_TowerUpgradeStats.m_nBasicMagicDamageCost;
            }
            else if (UpgradeMGR.m_eUpGradeKind == UpgradeMGR.E_UPGRADEKIND.E_SPEED)
            {
                if (!CheckUpgrade(m_TowerUpgradeStats.m_nMagicSpeedCost))
                    return;

                nTempData = PlayerPrefs.GetInt("PlayerMoney", 0);
                nTempData -= m_TowerUpgradeStats.m_nMagicSpeedCost;
                PlayerPrefs.SetInt("PlayerMoney", nTempData);

                fTempData = PlayerPrefs.GetFloat("UpgradeMagicSpeed", 0) + m_fAddSpeed;
                PlayerPrefs.SetFloat("UpgradeMagicSpeed", fTempData);

                nTempData = PlayerPrefs.GetInt("nMagicSpeedUpgradeCount", 1) + 1;
                PlayerPrefs.SetInt("nMagicSpeedUpgradeCount", nTempData);
                m_TowerUpgradeStats.m_nMagicSpeedCost += m_TowerUpgradeStats.m_nBasicMagicSpeedCost;
            }
        }
        m_MoneyTxt.text = "X " + PlayerPrefs.GetInt("PlayerMoney", 0).ToString();
        gameObject.SetActive(false);
    }

    private bool CheckUpgrade(int nUpgradeCost)
    {
        int nPlayerMoney = 0;
        bool bResult = false;

        nPlayerMoney = PlayerPrefs.GetInt("PlayerMoney");
        if (nPlayerMoney < nUpgradeCost)
        {
            m_NotEnoughMoneyUI.SetActive(true);
            gameObject.SetActive(false);
            bResult = false;
        }
        else
        {
            bResult = true;
        }

        return bResult;
    }
}
