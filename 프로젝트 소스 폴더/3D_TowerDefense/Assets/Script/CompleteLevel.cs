using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader m_SceneFader = null;

    public string m_sMainMenuName = "MainMenu";
    public string m_sNextLevel = "Level_2";
    public int m_nLevelToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("LevelReach", m_nLevelToUnlock);
        m_SceneFader.Fade(m_sNextLevel);
    }

    public void Menu()
    {
        m_SceneFader.Fade(m_sMainMenuName);
    }
}
