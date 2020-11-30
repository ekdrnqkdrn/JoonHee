using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMGR : MonoBehaviour
{
    public float m_fMinY = 10f;
    public float m_fMaxY = 95f;
    public Camera m_MainCamera;
    public float m_fPanSpeed = 30f;

    float m_fOldTouchDis = 0f;
    public float m_fFieldOfView = 60;
    
    [Header("화면 이동 시 최소 터치 거리")]
    public float m_fMinTouchDistance = 20f;
    private Vector2 m_PrevPos = Vector2.zero;

    public float m_fMinX = 12.3f;
    public float m_fMaxX = 80.4f;

    public float m_fMinZ = -22f;
    public float m_fMaxZ = 22f;
    // Update is called once per frame
    void Update()
    {
        CheckTouch();
    }

    private void CheckTouch()
    {
        if (Input.touchCount == 0)
        {
            m_PrevPos = Vector2.zero;
            m_fMinTouchDistance = 0f;
        }
        else if (Input.touchCount == 1)
        {
            if(m_PrevPos == Vector2.zero)
            {
                m_PrevPos = Input.GetTouch(0).position;
                return;
            }

            m_fMinTouchDistance = Vector2.Distance(m_PrevPos, Input.GetTouch(0).position);
            if (m_fMinTouchDistance < 20f)
            {
                return;
            }

            TowerBuildMGR.instance.ResetSelectTower();

            Vector2 dir = (Input.GetTouch(0).position - m_PrevPos).normalized;
            Vector3 vVector = new Vector3(dir.x, 0, dir.y);

            m_MainCamera.transform.position -= vVector * m_fPanSpeed * Time.deltaTime;
            m_PrevPos = Input.GetTouch(0).position;
        }
        else if (Input.touchCount == 2
        && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            float fTouchDis = 0f;
            float fDis = 0f;

            TowerBuildMGR.instance.ResetSelectTower();

            fTouchDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

            fDis = (fTouchDis - m_fOldTouchDis) * 0.01f;

            m_fFieldOfView -= fDis;
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, m_fMinY, m_fMaxY);

            m_MainCamera.fieldOfView = Mathf.Lerp(m_MainCamera.fieldOfView, m_fFieldOfView, Time.deltaTime * 5);

            m_fOldTouchDis = fTouchDis;
        }
    }
}
