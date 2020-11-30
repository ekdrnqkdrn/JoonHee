using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int nMoney;
    public static int nLives;

    [HideInInspector]
    public List<Dictionary<string, object>> m_PlayerStatsList; //DB에서 스텟 받는 리스트
    public string m_PlayerCVSFileName = "DB_PlayerStats"; 

    private void Start()
    {
        m_PlayerStatsList = CSVFileReader.Read(m_PlayerCVSFileName + "_" + SceneManager.GetActiveScene().name);
        
        nMoney = (int)m_PlayerStatsList[0]["Value"];
        nLives = (int)m_PlayerStatsList[1]["Value"];
    }
}
