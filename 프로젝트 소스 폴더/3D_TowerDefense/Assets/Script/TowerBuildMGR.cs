using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildMGR : MonoBehaviour
{
    public static TowerBuildMGR instance;
    public GameObject m_buildEffect = null;
    public GameObject m_SellEffect = null;
     
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("TowerBuildMGR 하나 이상 생성 시도 오류");
            return;
        }
        instance = this;
    }

    private TowerBuildPrint m_TowerBuildPrintMGR = null;
    private NodeMGR m_SelectNode = null;
    public NodeUIMGR m_NodeUiMGR = null;

    public E_TOWERTYPE m_eTowerType; //선택된 타워 타입
    public string m_NodeCVSFileName = "DB_mapinfo"; //노드에 타워 설치 가능여부 저장된 DB
    [HideInInspector]
    public List<Dictionary<string, object>> m_NodesCanBuildList; //전체노드에 타워 건설여부 저장 배열

    public GameObject m_NodesListMGR = null;
    private void Start()
    {
        m_NodesCanBuildList = CSVFileReader.Read(m_NodeCVSFileName);
        for (int i = 0; i < m_NodesCanBuildList.Count; i++)
        {
            m_NodesListMGR.transform.GetChild(i).GetComponent<NodeMGR>().SetCanBuildTower(i);
            //Debug.Log("ID : " + m_NodesCanBuildList[i][sData] + "   lv1 : " + m_NodesCanBuildList[i]["lv_1"] + "   lv2 : " + m_NodesCanBuildList[i]["lv_2"]);
        }
    }

    public bool CanBuild { get { return m_TowerBuildPrintMGR != null; } }
    public bool HasMoney { get { return PlayerStats.nMoney >= m_TowerBuildPrintMGR.m_nCost; } }
    public void SelectTowerToBuild (TowerBuildPrint Tower, E_TOWERTYPE eTowerType)
    {
        m_eTowerType = eTowerType;
        m_TowerBuildPrintMGR = Tower;

        if (HasMoney)
        {
            for (int i = 0; i < m_NodesCanBuildList.Count; i++)
            {
                m_NodesListMGR.transform.GetChild(i).GetComponent<NodeMGR>().SetNodeColor(eTowerType);
            }
        }

        DeSelectNode();
    }

    public void SelectNode(NodeMGR Node)
    {
        if (m_SelectNode == Node)
        {
            DeSelectNode();
            return;
        }

        if (m_SelectNode != null)
        {
            NodeMGR BeforeTempNodeScript = m_SelectNode.GetComponent<NodeMGR>();
            BeforeTempNodeScript.TowerRangeDisplay(false);
        }

        m_SelectNode = Node;
        m_TowerBuildPrintMGR = null;
        m_NodeUiMGR.SetTarget(m_SelectNode);

        NodeMGR TempNodeScript = m_SelectNode.GetComponent<NodeMGR>();
        TempNodeScript.TowerRangeDisplay(true);
    }

    public void DeSelectNode()
    {
        if (m_SelectNode != null)
        {
            NodeMGR BeforeTempNodeScript = m_SelectNode.GetComponent<NodeMGR>();
            BeforeTempNodeScript.TowerRangeDisplay(false);
        }

        m_SelectNode = null;
        m_NodeUiMGR.Hide();
    }

    public TowerBuildPrint GetTowerToBuild()
    {
        return m_TowerBuildPrintMGR;
    }

    public void ResetSelectTower()
    {
        m_TowerBuildPrintMGR = null;

        for (int i = 0; i < m_NodesCanBuildList.Count; i++)
        {
            m_NodesListMGR.transform.GetChild(i).GetComponent<NodeMGR>().SetNodeColor(E_TOWERTYPE.E_MAX);
        }
    }
}
