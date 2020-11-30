using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject m_Ui = null;
    public string m_sMainMenuName = "MainMenu";
    public SceneFader m_SceneFader = null;
    public GameObject m_OverlayCanvasTxt = null;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        m_Ui.SetActive(!m_Ui.activeSelf);
        m_OverlayCanvasTxt.SetActive(!m_OverlayCanvasTxt.activeSelf);

        if (m_Ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        m_SceneFader.Fade(SceneManager.GetActiveScene().name);
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        Toggle();
        m_SceneFader.Fade(m_sMainMenuName);
    }
}
