using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeUIMGR : MonoBehaviour
{
    public GameObject Ui = null;
    private NodeMGR m_TargetNode = null;
    public Text m_UpgradCostTxt;
    public Button m_UpgradeButton = null;
    public Button m_SellButoon = null;
    public Text m_SellCostTxt = null;
    public void SetTarget(NodeMGR Node)
    {
        m_TargetNode = Node;
        transform.position = Node.GetBuildPosition();

        if (!Node.m_bUpgarade)
        {
            m_UpgradCostTxt.text = "$" + Node.m_TowerBuildPrintMGR.m_nUpgrageCost.ToString();
            m_SellCostTxt.text = "$" + Node.m_TowerBuildPrintMGR.m_nSellPrice;
            m_UpgradeButton.interactable = true;
        }
        else
        {
            m_UpgradCostTxt.text = "MAX";
            m_SellCostTxt.text = "$" + Node.m_TowerBuildPrintMGR.m_nSellUpgradePrice;
            m_UpgradeButton.interactable = false;
        }
        Ui.SetActive(true);
    }

    public void Hide()
    {
        Ui.SetActive(false);
    }

    public void Upgrade()
    {
        m_TargetNode.UpGrageTower();
        TowerBuildMGR.instance.DeSelectNode();
    }

    public void Sell()
    {
        m_TargetNode.SellTower();
        TowerBuildMGR.instance.DeSelectNode();
    }
}
