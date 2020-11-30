using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적 움직이는 행동과 상태를 구분 하기위해 소스 나눔
[RequireComponent(typeof(EnemyMGR))] //Enemy 오브젝트의 움직임에 대한 스크립트이기 때문에 EnemyMGR이 null인경우 에러나게 만듬
public class EnemyMovementMGR : MonoBehaviour
{
    private Transform m_Target = null;
    private int m_nWavepointIndex = 0;

    private EnemyMGR m_EnemyMGR = null;
    private void Start()
    {
        m_Target = WayPointMGR.WayPointList[0];
        m_EnemyMGR = gameObject.GetComponent<EnemyMGR>();
    }
    private void Update()
    {
        Vector3 Dir = m_Target.position - transform.position;
        transform.Translate(Dir.normalized * m_EnemyMGR.m_fSpeed * Time.deltaTime, Space.World); //normalized : 일정한 속도로 가기 위해서
        if (Vector3.Distance(transform.position, m_Target.position) <= 0.5f)
        {
            NextWayPoint();
        }

        if (EnemyMGR.m_bInfiniteMode)
        {
            m_EnemyMGR.m_fSpeed = m_EnemyMGR.m_fInfiniteModeSpeed;
        }
        else
        {
            m_EnemyMGR.m_fSpeed = m_EnemyMGR.m_fStartSpeed;
        }
    }

    private void NextWayPoint()
    {
        m_nWavepointIndex++;

        if (m_nWavepointIndex >= WayPointMGR.WayPointList.Length)
        {
            EndPath();
            return;
        }

        m_Target = WayPointMGR.WayPointList[m_nWavepointIndex];
    }
    void EndPath()
    {
        PlayerStats.nLives--;
        WaveMGR.nEnemyAlive--;
        GameObject.Destroy(gameObject);
    }
}
