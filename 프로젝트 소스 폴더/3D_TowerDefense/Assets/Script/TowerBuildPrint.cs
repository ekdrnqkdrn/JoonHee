using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클래스
public enum E_TOWERTYPE
{
    E_ID     ,
    E_CANNON ,
    E_LASER  ,
    E_MAGIC  ,
    E_MAX    
}

[System.Serializable] //공통 속성을 가진 변수들을 그룹지어 클래스를 만든다. [Serializable] 속성을 부여한다.
public class TowerBuildPrint
{
    public GameObject m_prefab = null;
    public GameObject m_Upgradeprefab = null;
    public int m_nCost = 0;
    public int m_nUpgrageCost = 0;
    public int m_nSellPrice = 0;
    public int m_nSellUpgradePrice = 0;
}

