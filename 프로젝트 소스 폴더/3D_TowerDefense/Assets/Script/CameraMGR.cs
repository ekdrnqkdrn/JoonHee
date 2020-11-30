using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMGR : MonoBehaviour
{
    public float m_fPanSpeed = 30f;
    public float m_fPanBorderThickness = 10;
    public float m_fScrollSpeed = 5;
    public float m_fMinY = 10f;
    public float m_fMaxY = 95f;

    public float m_fMinX = 12.3f;
    public float m_fMaxX = 80.4f;

    public float m_fMinZ = -22f;
    public float m_fMaxZ = 22f;
    //private bool m_bMovement = true; //Deleted by 2020.09.22 메뉴 패널 만드느라 삭제
    // Update is called once per frame
    void Update()
    {
        if (GameManager.bGameOver)
        {
            this.enabled = false;
            return;
        }

        //-->Delete by 2020.09.22 메뉴 패널 만드느라 삭제
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    m_bMovement = !m_bMovement;

        //if (!m_bMovement)
        //    return;
        //<--Delete by 2020.09.22 메뉴 패널 만드느라 삭제
  
        if (Input.GetKeyDown("w") || Input.mousePosition.y >= Screen.height - m_fPanBorderThickness)
        {
            if (transform.position.z >= m_fMaxZ)
                return;

            transform.Translate(Vector3.forward * m_fPanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKeyDown("s") || Input.mousePosition.y <= m_fPanBorderThickness)
        {
            if (transform.position.z <= m_fMinZ)
                return;

            transform.Translate(Vector3.back * m_fPanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKeyDown("a") || Input.mousePosition.x >= Screen.width - m_fPanBorderThickness) 
        {
            if (transform.position.x >= m_fMaxX)
                return;

            transform.Translate(Vector3.right * m_fPanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKeyDown("d") || Input.mousePosition.x <= m_fPanBorderThickness) 
        {
            if (transform.position.x <= m_fMinX)
                return;

            transform.Translate(Vector3.left * m_fPanSpeed * Time.deltaTime, Space.World);
        }

        float fScroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 vPos = transform.position;
        vPos.y -= fScroll * 1000 * m_fScrollSpeed * Time.deltaTime;
        vPos.y = Mathf.Clamp(vPos.y, m_fMinY, m_fMaxY);

        transform.position = vPos;
    }
}
