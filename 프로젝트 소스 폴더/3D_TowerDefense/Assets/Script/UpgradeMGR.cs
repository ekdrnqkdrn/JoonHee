using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMGR : MonoBehaviour
{
    public static E_TOWERTYPE m_eNowTower;
    public static E_UPGRADEKIND m_eUpGradeKind;
    public enum E_UPGRADEKIND
    {
        E_DAMAGE,
        E_SPEED,
        E_SLOW
    }

    public GameObject m_UpgradeConfirmUI = null;
    private UpgradeConfimMGR m_UpgradeConfirmScript = null;
    public Text m_MoneyTxt = null;

    // Start is called before the first frame update
    void Start()
    {
        m_eNowTower = E_TOWERTYPE.E_CANNON;
        m_UpgradeConfirmScript = m_UpgradeConfirmUI.GetComponent<UpgradeConfimMGR>();
        m_MoneyTxt.text = "X " + PlayerPrefs.GetInt("PlayerMoney", 0);
    }

    void UpgradeConfirm(E_UPGRADEKIND eKind)
    {
        m_UpgradeConfirmUI.SetActive(true);
        m_UpgradeConfirmScript.TurnOnUpgradeConfirm(eKind);
    }

    public void DamageClick()
    {
        m_eUpGradeKind = E_UPGRADEKIND.E_DAMAGE;
        UpgradeConfirm(E_UPGRADEKIND.E_DAMAGE);
    }

    public void SpeedClick()
    {
        m_eUpGradeKind = E_UPGRADEKIND.E_SPEED;
        UpgradeConfirm(E_UPGRADEKIND.E_SPEED);
    }

    public void SlowClick()
    {
        m_eUpGradeKind = E_UPGRADEKIND.E_SLOW;
        UpgradeConfirm(E_UPGRADEKIND.E_SLOW);
    }
}
