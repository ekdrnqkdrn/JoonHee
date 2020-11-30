using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonMGR : MonoBehaviour
{
    public Camera m_MainCamera = null;
    public Transform m_CannonPos = null;
    public Transform m_LaserPos = null;
    public Transform m_MaigcPos = null;
    public Text m_TowerName = null;

    public float m_fCameraSpeed = 0f;

    private Vector3 vDir = Vector3.zero;
    private bool isClick = false;

    public SceneFader m_SceneFader = null;
    public string m_sMainMenuName = "MainMenu";

    public Text m_MoneyTxt = null;

    private void Start()
    {
        m_MoneyTxt.text = "X " + PlayerPrefs.GetInt("PlayerMoney", 0).ToString();
    }

    public void UpdatePlayerMoneyTxt()
    {
        m_MoneyTxt.text = "X " + PlayerPrefs.GetInt("PlayerMoney", 0).ToString();
    }

    //수정할거(하드코딩 말고 작업 필요)
    public void RightButtonClcik()
    {
        if (isClick)
            return;

        vDir = Vector3.zero;

        if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_CANNON)
        {
            UpgradeMGR.m_eNowTower = E_TOWERTYPE.E_LASER;
            m_TowerName.text = "Laser Tower";
            vDir = m_LaserPos.position;
        }
        else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_LASER)
        {
            UpgradeMGR.m_eNowTower = E_TOWERTYPE.E_MAGIC;
            m_TowerName.text = "Maigc Tower";
            vDir = m_MaigcPos.position;
        }
        else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_MAGIC)
        {
            UpgradeMGR.m_eNowTower = E_TOWERTYPE.E_CANNON;
            m_TowerName.text = "Cannon Tower";
            vDir = m_CannonPos.position;
        }

        StartCoroutine(MoveTo(m_MainCamera.transform, vDir));
    }

    //수정할거(하드코딩 말고 작업 필요)
    public void LeftButtonClick()
    {
        if (isClick)
            return;

        vDir = Vector3.zero;

        if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_CANNON)
        {
            UpgradeMGR.m_eNowTower = E_TOWERTYPE.E_MAGIC;
            m_TowerName.text = "Maigc Tower";
            vDir = m_MaigcPos.position;
        }
        else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_LASER)
        {
            UpgradeMGR.m_eNowTower = E_TOWERTYPE.E_CANNON;
            m_TowerName.text = "Cannon Tower";
            vDir = m_CannonPos.position;
        }
        else if (UpgradeMGR.m_eNowTower == E_TOWERTYPE.E_MAGIC)
        {
            UpgradeMGR.m_eNowTower = E_TOWERTYPE.E_LASER;
            m_TowerName.text = "Laser Tower";
            vDir = m_LaserPos.position;
        }

        StartCoroutine(MoveTo(m_MainCamera.transform, vDir));
    }

    IEnumerator MoveTo(Transform FromPos, Vector3 ToPos)
    {
        isClick = true;

        float fCount = 0f;

        Vector3 wasPos = FromPos.position;

        while (true)
        {
            fCount += Time.deltaTime * m_fCameraSpeed;
            FromPos.position = Vector3.Lerp(wasPos, ToPos, fCount);

            if (fCount >= 1)
            {
                FromPos.position = ToPos;
                isClick = false;
                break;
            }
            yield return null;
        }
    }

    public void QuitBuutonClick()
    {
        m_SceneFader.Fade(m_sMainMenuName);
    }
}
