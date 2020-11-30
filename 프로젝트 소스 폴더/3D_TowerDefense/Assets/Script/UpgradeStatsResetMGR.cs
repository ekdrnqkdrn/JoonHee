using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatsResetMGR : MonoBehaviour
{
    public GameObject m_ResetConfirmUI = null;

    public void UpgradeStatsResetClick()
    {
        m_ResetConfirmUI.SetActive(true);
    }

    public void CancelClick()
    {
        m_ResetConfirmUI.SetActive(false);
    }

    public void UpgradeStatsConfirm()
    {
        PlayerPrefs.SetInt("UpgradeCannonDamage", 0);
        PlayerPrefs.SetFloat("UpgradeCannonSpeed", 0);

        PlayerPrefs.SetInt("UpgradeLaserDamage", 0);
        PlayerPrefs.SetFloat("UpgradeLaserSlow", 0);

        PlayerPrefs.SetInt("UpgradeMagicDamage", 0);
        PlayerPrefs.SetFloat("UpgradeMagicSpeed", 0);

        PlayerPrefs.SetInt("nCannonDamageUpgradeCount", 1);
        PlayerPrefs.SetInt("nCannonSpeedUpgradeCount", 1);
        PlayerPrefs.SetInt("nLaserDamageUpgradeCount", 1);
        PlayerPrefs.SetInt("nLaserSlowUpgradeCount", 1);
        PlayerPrefs.SetInt("nMagicDamageUpgradeCount", 1);
        PlayerPrefs.SetInt("nMagicSpeedUpgradeCount", 1);

        PlayerPrefs.SetInt("PlayerMoney", 0);
        PlayerPrefs.SetInt("PlayerMoney", 1000);

        m_ResetConfirmUI.SetActive(false);
    }
}
