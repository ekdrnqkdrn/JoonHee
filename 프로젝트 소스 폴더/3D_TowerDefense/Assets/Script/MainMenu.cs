using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string m_sLoadString = "Level_1";
    public string m_sLoadUpgrade = "Upgrade";

    public SceneFader m_SceneFader = null;
    public void Play()
    {
        m_SceneFader.Fade(m_sLoadString);
    }

    public void Upgrade()
    {
        m_SceneFader.Fade(m_sLoadUpgrade);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
