using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShopMGR : MonoBehaviour
{
    public string m_TowerInfoCVSFileName = "DB_TowerInfo"; //타워 기본 가격 있는 DB
    public TowerBuildPrint m_CannonTower = null;
    public TowerBuildPrint m_MagicTower = null;
    public TowerBuildPrint m_LaserTower = null;

    private TowerBuildMGR m_builMGR;

    [Space]
    public Text m_CannonPriceTxt = null;
    public Text m_LaserPriceTxt = null;
    public Text m_MagicPriceTxt = null;

    [HideInInspector]
    public List<Dictionary<string, object>> m_TowerInfoList; //타워 리소스 읽어서 저장 하는 리스트

    [Header("대포타워 프리펩")]
    public GameObject m_Cannonprefab = null;
    public GameObject m_CannonUpgradeprefab = null;

    [Header("레이저타워 프리펩")]
    public GameObject m_Laserprefab = null;
    public GameObject m_LaserUpgradeprefab = null;

    [Header("매직타워 프리펩")]
    public GameObject m_Magicprefab = null;
    public GameObject m_MagicUpgradeprefab = null;

    private void Start()
    {
        LoadTowerInfo();
    }

    private void LoadTowerInfo()
    {
        m_builMGR = TowerBuildMGR.instance;

        m_TowerInfoList = CSVFileReader.Read(m_TowerInfoCVSFileName);
        for (int i = 0; i < m_TowerInfoList.Count; i++)
        {
            //DB에서 타워 정보 불러옴
            int nId = (int)m_TowerInfoList[i]["id"];
            
            TowerBuildPrint TempTowerBuildPrint = new TowerBuildPrint();
            TempTowerBuildPrint.m_nCost = (int)m_TowerInfoList[i]["Price_lv1"];
            TempTowerBuildPrint.m_nUpgrageCost = (int)m_TowerInfoList[i]["Price_lv2"];
            TempTowerBuildPrint.m_nSellPrice = (int)m_TowerInfoList[i]["Price_Sell_lv1"];
            TempTowerBuildPrint.m_nSellUpgradePrice = (int)m_TowerInfoList[i]["Price_Sell_lv2"];
                        
            if (nId == (int)E_TOWERTYPE.E_CANNON)
            {
                m_CannonTower = TempTowerBuildPrint;

                m_CannonTower.m_prefab = m_Cannonprefab;
                m_CannonTower.m_Upgradeprefab = m_CannonUpgradeprefab;

                m_CannonPriceTxt.text = m_CannonTower.m_nCost.ToString();
            }
            else if (nId == (int)E_TOWERTYPE.E_LASER)
            {
                m_LaserTower = TempTowerBuildPrint;

                m_LaserTower.m_prefab = m_Laserprefab;
                m_LaserTower.m_Upgradeprefab = m_LaserUpgradeprefab;

                m_LaserPriceTxt.text = m_LaserTower.m_nCost.ToString();
            }
            else if (nId == (int)E_TOWERTYPE.E_MAGIC)
            {
                m_MagicTower = TempTowerBuildPrint;

                m_MagicTower.m_prefab = m_Magicprefab;
                m_MagicTower.m_Upgradeprefab = m_MagicUpgradeprefab;

                m_MagicPriceTxt.text = m_MagicTower.m_nCost.ToString();
            }
        }
    }

    public void SelectCannonTower()
    {
        m_builMGR.SelectTowerToBuild(m_CannonTower, E_TOWERTYPE.E_CANNON);
    }

    public void SelectMagicTower()
    {
        m_builMGR.SelectTowerToBuild(m_MagicTower, E_TOWERTYPE.E_MAGIC);
    }

    public void SelectLaserTower()
    {
        m_builMGR.SelectTowerToBuild(m_LaserTower, E_TOWERTYPE.E_LASER);
    }
}
