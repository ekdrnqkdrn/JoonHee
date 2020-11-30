using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMGR : MonoBehaviour
{
    public SceneFader m_SceneFader = null;
    public string m_sMainMenuName = "MainMenu";
    // Start is called before the first frame update

    public void Retry()
    {
        m_SceneFader.Fade(SceneManager.GetActiveScene().name);
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //현재씬을 재시작 하기위함으로 현재씬 인덱스 다시 로드
    }

    public void Menu()
    {
        m_SceneFader.Fade(m_sMainMenuName);
    }
}

