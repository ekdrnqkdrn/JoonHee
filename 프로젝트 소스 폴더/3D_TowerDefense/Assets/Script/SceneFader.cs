using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image m_img = null;
    public AnimationCurve m_AnimationCurve = null;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void Fade(string Scene)
    {
        StartCoroutine(FadeOut(Scene));
    }

    IEnumerator FadeIn()
    {
        float fData = 1f;

        while (fData > 0f)
        {
            fData -= Time.deltaTime;
            float a = m_AnimationCurve.Evaluate(fData);
            m_img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string Scene)
    {
        float fData = 0f;

        while (fData < 1f)
        {
            fData += Time.deltaTime;
            float a = m_AnimationCurve.Evaluate(fData);
            m_img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(Scene);
    }
}
