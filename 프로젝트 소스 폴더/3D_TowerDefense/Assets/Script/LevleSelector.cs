using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevleSelector : MonoBehaviour
{
    public SceneFader m_SceneFacder = null;
    public Button[] m_arrLevelButtons;

    private void Start()
    {
        int LevelReach = PlayerPrefs.GetInt("LevelReach", 1);

        for (int i = 0; i < m_arrLevelButtons.Length; i++)
        {
            if (i+1 > LevelReach)
                m_arrLevelButtons[i].interactable = false;
        }
    }

    public void LevelSelect(string sLevelName)
    {
        m_SceneFacder.Fade(sLevelName);
    }
}
