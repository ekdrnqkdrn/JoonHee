using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class NodeMGR : MonoBehaviour
{
    public Color m_hoverColor;
    public Color m_NotEnoughMoneyColor;

    public Vector3 m_vPositionOffeset = Vector3.zero;

    [HideInInspector]
    public GameObject m_Tower = null;
    [HideInInspector]
    public TowerBuildPrint m_TowerBuildPrintMGR = null;
    [HideInInspector]
    public bool m_bUpgarade = false;

    private Renderer m_Rend = null;
    private Color m_StartColor;
    TowerBuildMGR m_TowerBuildMGR;
    //타워 설치 가능 여부 관련 변수
    public int[] m_CanBuildTowerList; //수정할거(hide로 숨김)
    public int m_index; //수정할거(삭제)

    //CSV 파일 읽을때 컬럼명값
    [Header("CSV_File_Colum_name")]
    public string m_ID = "id";
    public string m_Cannon = "Cannon";
    public string m_Laser = "Laser";
    public string m_Magic = "Magic";
    private void Start()
    {
        m_Rend = GetComponent<Renderer>();
        m_StartColor = m_Rend.material.color;
        m_TowerBuildMGR = TowerBuildMGR.instance;
    }

    public void SetCanBuildTower(int nIndex)
    {
        m_CanBuildTowerList = new int[(int)E_TOWERTYPE.E_MAX];

        m_CanBuildTowerList[(int)E_TOWERTYPE.E_ID] = (int)TowerBuildMGR.instance.m_NodesCanBuildList[nIndex]["id"];
        m_CanBuildTowerList[(int)E_TOWERTYPE.E_CANNON] = (int)TowerBuildMGR.instance.m_NodesCanBuildList[nIndex]["Cannon"];
        m_CanBuildTowerList[(int)E_TOWERTYPE.E_LASER] = (int)TowerBuildMGR.instance.m_NodesCanBuildList[nIndex]["Laser"];
        m_CanBuildTowerList[(int)E_TOWERTYPE.E_MAGIC] = (int)TowerBuildMGR.instance.m_NodesCanBuildList[nIndex]["Magic"];

        m_index = nIndex;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (m_Tower != null)
        {
            m_TowerBuildMGR.SelectNode(this);
            return;
        }

        if (!m_TowerBuildMGR.CanBuild)
            return;

        if (!CanBuildTower(m_TowerBuildMGR.m_eTowerType))
            return;


        BuildTower(m_TowerBuildMGR.GetTowerToBuild());
    }

    private bool CanBuildTower(E_TOWERTYPE eTowerType)
    {
        bool bResult = false;

        if (eTowerType == E_TOWERTYPE.E_CANNON
        && m_CanBuildTowerList[(int)E_TOWERTYPE.E_CANNON] == 1)
        {
            bResult = false;
        }
        else if (eTowerType == E_TOWERTYPE.E_LASER
        && m_CanBuildTowerList[(int)E_TOWERTYPE.E_LASER] == 1)
        {
            bResult = false;
        }
        else if (eTowerType == E_TOWERTYPE.E_MAGIC
        && m_CanBuildTowerList[(int)E_TOWERTYPE.E_MAGIC] == 1)
        {
            bResult = false;
        }
        else
        {
            bResult = true;
        }

        return bResult;
    }

    void BuildTower(TowerBuildPrint TowerBuildPrint)
    {
        if (PlayerStats.nMoney < TowerBuildPrint.m_nCost)
        {
            return;
        }
        PlayerStats.nMoney -= TowerBuildPrint.m_nCost;
        
        GameObject effect = Instantiate(m_TowerBuildMGR.m_buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        //GameObject Tower = Instantiate(m_TowerBuildPrintMGR.m_prefab, node.GetBuildPosition(), Quaternion.identity);
        GameObject Tower = Instantiate(TowerBuildPrint.m_prefab, GetBuildPosition(), TowerBuildPrint.m_prefab.transform.rotation);
        m_Tower = Tower;
        m_TowerBuildPrintMGR = TowerBuildPrint;
        m_TowerBuildMGR.ResetSelectTower();
    }

    public void UpGrageTower()
    {
        if (PlayerStats.nMoney < m_TowerBuildPrintMGR.m_nUpgrageCost)
        {
            return;
        }
        PlayerStats.nMoney -= m_TowerBuildPrintMGR.m_nUpgrageCost;
        
        //이전 타워 삭제
        Destroy(m_Tower);


        //업그레이드 된 타워 생성
        //GameObject Tower = Instantiate(m_TowerBuildPrintMGR.m_prefab, node.GetBuildPosition(), Quaternion.identity);
        GameObject Tower = Instantiate(m_TowerBuildPrintMGR.m_Upgradeprefab, GetBuildPosition(), m_TowerBuildPrintMGR.m_Upgradeprefab.transform.rotation);

        GameObject effect = Instantiate(m_TowerBuildMGR.m_buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        m_bUpgarade = true;
        m_Tower = Tower;
    }
    public void SellTower()
    {
        if (m_bUpgarade)
            PlayerStats.nMoney += m_TowerBuildPrintMGR.m_nSellUpgradePrice;
        else
            PlayerStats.nMoney += m_TowerBuildPrintMGR.m_nSellPrice;


        GameObject effect = Instantiate(m_TowerBuildMGR.m_SellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        Destroy(m_Tower);
        m_TowerBuildPrintMGR = null;
        m_bUpgarade = false;
    }
    //private void OnMouseEnter()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return;

    //    if (!m_TowerBuildMGR.CanBuild)
    //        return;

    //    if (m_TowerBuildMGR.HasMoney)
    //    {
    //        m_Rend.material.color = m_hoverColor;
    //    }
    //    else
    //    {
    //        m_Rend.material.color = m_NotEnoughMoneyColor;
    //    }
    //}

    //private void OnMouseExit()
    //{
    //    m_Rend.material.color = m_StartColor;
    //}

    public Vector3 GetBuildPosition()
    {
        return transform.position + m_vPositionOffeset;
    }

    public void TowerRangeDisplay(bool bData)
    {
        if (m_Tower == null)
            return;

        m_Tower.GetComponent<TowerMGR>().DisplayRange(bData);
    }
    
    public void SetNodeColor(E_TOWERTYPE eTowerType)
    {
        if (eTowerType == E_TOWERTYPE.E_CANNON
        && m_CanBuildTowerList[(int)E_TOWERTYPE.E_CANNON] == 1)
        {
            m_Rend.material.color = m_NotEnoughMoneyColor;
        }
        else if (eTowerType == E_TOWERTYPE.E_LASER
        && m_CanBuildTowerList[(int)E_TOWERTYPE.E_LASER] == 1)
        {
            m_Rend.material.color = m_NotEnoughMoneyColor;
        }
        else if (eTowerType == E_TOWERTYPE.E_MAGIC
        && m_CanBuildTowerList[(int)E_TOWERTYPE.E_MAGIC] == 1)
        {
            m_Rend.material.color = m_NotEnoughMoneyColor;
        }
        else if (eTowerType == E_TOWERTYPE.E_MAX)
        {
            m_Rend.material.color = m_StartColor;
        }
        else
        {
            m_Rend.material.color = m_hoverColor;
        }
    }
}
